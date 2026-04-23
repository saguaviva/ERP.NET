using Erp.Application.Auditing;
using Erp.Application.Auth;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Numbering;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Numbering;

public sealed class MySqlDocumentNumberingService : IDocumentNumberingQueries, IDocumentNumberingService
{
    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;

    public MySqlDocumentNumberingService(
        MySqlConnectionFactory connectionFactory,
        IAuditLogService auditLogService,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext)
    {
        _connectionFactory = connectionFactory;
        _auditLogService = auditLogService;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
    }

    public async Task<DocumentNumberingSetupDto> GetSetupAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return BuildEmptySetup();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        var storedSequences = await DocumentNumberingSqlHelper.LoadSequencesAsync(connection, transaction: null, tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var sequenceItems = new List<DocumentNumberingSequenceDto>(DocumentNumberingKeys.OrderedKeys.Count);

        foreach (var key in DocumentNumberingKeys.OrderedKeys)
        {
            var stored = storedSequences.TryGetValue(key, out var state) ? state : null;
            var suggested = await DocumentNumberingSqlHelper.GetSuggestedNextNumberAsync(connection, transaction: null, tenantId, companyId, key, cancellationToken);
            sequenceItems.Add(new DocumentNumberingSequenceDto
            {
                SequenceKey = key,
                Series = stored?.Series ?? string.Empty,
                NextNumber = stored?.NextNumber ?? suggested,
                SuggestedNextNumber = suggested,
                LastNumber = stored?.LastNumber ?? Math.Max(0, suggested - 1),
                IsActive = stored?.IsActive ?? true,
                Notes = stored?.Notes ?? string.Empty
            });
        }

        var disposition = await DocumentNumberingSqlHelper.LoadDispositionSetupAsync(connection, transaction: null, tenantId, companyId, cancellationToken);
        var suggestedDispositionYear = disposition?.Year ?? DateTime.Today.Year.ToString();
        var suggestedDispositionNextNumber = await DocumentNumberingSqlHelper.GetSuggestedDispositionNextNumberAsync(
            connection,
            transaction: null,
            centerCode,
            suggestedDispositionYear,
            cancellationToken);

        return new DocumentNumberingSetupDto
        {
            Sequences = sequenceItems,
            DispositionYear = disposition?.Year ?? suggestedDispositionYear,
            SuggestedDispositionYear = suggestedDispositionYear,
            DispositionNextNumber = disposition?.NextNumber ?? suggestedDispositionNextNumber,
            SuggestedDispositionNextNumber = suggestedDispositionNextNumber
        };
    }

    public async Task SaveSetupAsync(SaveDocumentNumberingSetupCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();

        var safeSequences = command.Sequences
            .Where(item => DocumentNumberingKeys.OrderedKeys.Contains(item.SequenceKey, StringComparer.OrdinalIgnoreCase))
            .GroupBy(item => item.SequenceKey, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .ToArray();

        if (safeSequences.Length == 0)
        {
            throw new InvalidOperationException("No hay secuencias válidas para guardar.");
        }

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        foreach (var sequence in safeSequences)
        {
            await DocumentNumberingSqlHelper.UpsertSequenceAsync(
                connection,
                transaction,
                command.TenantId,
                command.CompanyId,
                sequence.SequenceKey,
                sequence.Series,
                Math.Max(1, sequence.NextNumber),
                Math.Max(0, sequence.NextNumber - 1),
                sequence.IsActive,
                sequence.Notes,
                cancellationToken);
        }

        await DocumentNumberingSqlHelper.UpsertDispositionSetupAsync(
            connection,
            transaction,
            command.TenantId,
            command.CompanyId,
            command.DispositionYear,
            Math.Max(1, command.DispositionNextNumber),
            cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = command.TenantId,
            CompanyId = command.CompanyId,
            UserId = _currentUserContext.UserId,
            Action = "DocumentNumberingUpdated",
            EntityName = "DocumentNumbering",
            EntityId = command.CompanyId.ToString(),
            Details = $"Secuencias={safeSequences.Length}; Disposición={DocumentNumberingSqlHelper.NormalizeDispositionYear(command.DispositionYear)}-{Math.Max(1, command.DispositionNextNumber)}"
        }, cancellationToken);
    }

    private static DocumentNumberingSetupDto BuildEmptySetup() =>
        new()
        {
            Sequences = DocumentNumberingKeys.OrderedKeys.Select(key => new DocumentNumberingSequenceDto
            {
                SequenceKey = key,
                NextNumber = 1,
                SuggestedNextNumber = 1,
                IsActive = true
            }).ToArray(),
            DispositionYear = DateTime.Today.Year.ToString(),
            SuggestedDispositionYear = DateTime.Today.Year.ToString(),
            DispositionNextNumber = 1,
            SuggestedDispositionNextNumber = 1
        };

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        var companies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId!.Value, tenantId, cancellationToken);
        var company = companies.FirstOrDefault(item => item.CompanyId == companyId);
        if (company is null || string.IsNullOrWhiteSpace(company.LegacyCenterCode))
        {
            throw new InvalidOperationException("La empresa activa no tiene centro legacy configurado.");
        }

        return company.LegacyCenterCode.Trim().ToUpperInvariant();
    }

    private void EnsureTenantWriteAccess()
    {
        if (_currentUserContext.IsPlatformAdmin)
        {
            return;
        }

        if (_currentUserContext.Roles.Contains(PlatformRoles.TenantAdmin, StringComparer.OrdinalIgnoreCase))
        {
            return;
        }

        throw new InvalidOperationException("No tienes permisos para editar numeraciones en este tenant.");
    }

    private async Task EnsureCompanyAccessAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        if (!_currentUserContext.IsAuthenticated || !_currentUserContext.UserId.HasValue)
        {
            throw new InvalidOperationException("Debes iniciar sesión para acceder a esta empresa.");
        }

        if (!_tenantContext.TenantId.HasValue || _tenantContext.TenantId.Value != tenantId)
        {
            throw new InvalidOperationException("El tenant solicitado no coincide con tu sesión activa.");
        }

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        if (!allowedCompanies.Any(item => item.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa solicitada.");
        }
    }
}
