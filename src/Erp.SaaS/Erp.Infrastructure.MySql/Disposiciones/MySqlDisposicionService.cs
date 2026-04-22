using Erp.Application.Auditing;
using Erp.Application.Companies;
using Erp.Application.Contexts;
using Erp.Application.Disposiciones;
using Erp.Application.Stock;
using Erp.Domain.Common;
using Erp.Infrastructure.MySql.Database;
using Erp.Infrastructure.MySql.Support;
using MySqlConnector;

namespace Erp.Infrastructure.MySql.Disposiciones;

public sealed class MySqlDisposicionService : IDisposicionQueries, IDisposicionService
{
    private readonly record struct StockLineSnapshot(
        int LineNumber,
        string FabricCode,
        string Description,
        decimal TotalKilograms,
        decimal Yield,
        bool IsServed,
        bool IsDisposed);

    private readonly MySqlConnectionFactory _connectionFactory;
    private readonly IAuditLogService _auditLogService;
    private readonly ICompanyAccessService _companyAccessService;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly ITenantContext _tenantContext;
    private readonly IActiveCompanyContext _activeCompanyContext;

    public MySqlDisposicionService(
        MySqlConnectionFactory connectionFactory,
        IAuditLogService auditLogService,
        ICompanyAccessService companyAccessService,
        ICurrentUserContext currentUserContext,
        ITenantContext tenantContext,
        IActiveCompanyContext activeCompanyContext)
    {
        _connectionFactory = connectionFactory;
        _auditLogService = auditLogService;
        _companyAccessService = companyAccessService;
        _currentUserContext = currentUserContext;
        _tenantContext = tenantContext;
        _activeCompanyContext = activeCompanyContext;
    }

    public async Task<DisposicionSearchResultDto> SearchAsync(Guid tenantId, Guid companyId, DisposicionFilter filter, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return new DisposicionSearchResultDto();
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);
        var pageSize = Math.Clamp(filter.PageSize, 10, 200);
        var page = Math.Max(filter.Page, 1);
        var offset = (page - 1) * pageSize;
        var search = filter.Search?.Trim() ?? string.Empty;
        var likeSearch = $"%{search}%";
        var receivedMode = filter.ReceivedMode?.Trim() ?? string.Empty;

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);

        await using var countCommand = connection.CreateCommand();
        countCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM dispos d
            LEFT JOIN clients client
                ON client.CENTRO = d.CENTRO
               AND client.CODI = d.CLIENT
               AND client.is_deleted = 0
            LEFT JOIN tallers finisher
                ON finisher.CENTRO = d.CENTRO
               AND finisher.CODI = d.ACABADOR
               AND finisher.is_deleted = 0
            WHERE d.CENTRO = @centerCode
              AND d.is_deleted = 0
              AND (@includeCancelled = 1 OR d.ANULADA = 0)
              AND (
                    @receivedMode = ''
                    OR (@receivedMode = 'received' AND d.RECIBIDO = 1)
                    OR (@receivedMode = 'pending' AND d.RECIBIDO = 0)
                  )
              AND (
                    @search = ''
                    OR CAST(d.CODI AS CHAR) LIKE @likeSearch
                    OR CAST(d.IDDISPOS AS CHAR) LIKE @likeSearch
                    OR d.ANY LIKE @likeSearch
                    OR CAST(d.CLIENT AS CHAR) LIKE @likeSearch
                    OR COALESCE(client.NOM, '') LIKE @likeSearch
                    OR CAST(d.ACABADOR AS CHAR) LIKE @likeSearch
                    OR COALESCE(finisher.NOM, '') LIKE @likeSearch
                    OR COALESCE(d.CODICLIENT, '') LIKE @likeSearch
                    OR COALESCE(d.COMANDA, '') LIKE @likeSearch
                    OR COALESCE(d.COLOR, '') LIKE @likeSearch
                    OR COALESCE(d.COLORCLIENTE, '') LIKE @likeSearch
                    OR COALESCE(d.OBSERV, '') LIKE @likeSearch
                  );
            """;
        FillSearchParameters(countCommand, centerCode, filter.IncludeCancelled, receivedMode, search, likeSearch);
        var totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync(cancellationToken));
        if (totalCount == 0)
        {
            return new DisposicionSearchResultDto();
        }

        var items = new List<DisposicionListItemDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            $"""
            SELECT d.CODI,
                   d.CENTRO,
                   d.ANY,
                   d.IDDISPOS,
                   d.FECHA,
                   d.DRECEPCION,
                   d.CLIENT,
                   COALESCE(client.NOM, '') AS NOMCLIENT,
                   d.ACABADOR,
                   COALESCE(finisher.NOM, '') AS NOMACABADOR,
                   d.CODICLIENT,
                   d.COLORCLIENTE,
                   d.COLOR,
                   d.COMANDA,
                   d.TOTALPIEZAS,
                   d.TOTALKG,
                   d.RECIBIDO,
                   d.ANULADA,
                   d.origin,
                   (
                       SELECT COUNT(*)
                       FROM ddispos detail_rows
                       WHERE detail_rows.CENTRO = d.CENTRO
                         AND detail_rows.DISPOS = d.CODI
                   ) AS LINEAS
            FROM dispos d
            LEFT JOIN clients client
                ON client.CENTRO = d.CENTRO
               AND client.CODI = d.CLIENT
               AND client.is_deleted = 0
            LEFT JOIN tallers finisher
                ON finisher.CENTRO = d.CENTRO
               AND finisher.CODI = d.ACABADOR
               AND finisher.is_deleted = 0
            WHERE d.CENTRO = @centerCode
              AND d.is_deleted = 0
              AND (@includeCancelled = 1 OR d.ANULADA = 0)
              AND (
                    @receivedMode = ''
                    OR (@receivedMode = 'received' AND d.RECIBIDO = 1)
                    OR (@receivedMode = 'pending' AND d.RECIBIDO = 0)
                  )
              AND (
                    @search = ''
                    OR CAST(d.CODI AS CHAR) LIKE @likeSearch
                    OR CAST(d.IDDISPOS AS CHAR) LIKE @likeSearch
                    OR d.ANY LIKE @likeSearch
                    OR CAST(d.CLIENT AS CHAR) LIKE @likeSearch
                    OR COALESCE(client.NOM, '') LIKE @likeSearch
                    OR CAST(d.ACABADOR AS CHAR) LIKE @likeSearch
                    OR COALESCE(finisher.NOM, '') LIKE @likeSearch
                    OR COALESCE(d.CODICLIENT, '') LIKE @likeSearch
                    OR COALESCE(d.COMANDA, '') LIKE @likeSearch
                    OR COALESCE(d.COLOR, '') LIKE @likeSearch
                    OR COALESCE(d.COLORCLIENTE, '') LIKE @likeSearch
                    OR COALESCE(d.OBSERV, '') LIKE @likeSearch
                  )
            {BuildSearchOrderByClause(filter)}
            LIMIT @limit OFFSET @offset;
            """;
        FillSearchParameters(command, centerCode, filter.IncludeCancelled, receivedMode, search, likeSearch);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new DisposicionListItemDto
            {
                Code = reader.GetInt32OrDefault("CODI"),
                CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
                Year = reader.GetStringOrEmpty("ANY"),
                Number = reader.GetInt32OrDefault("IDDISPOS"),
                Date = GetNullableDateTime(reader, "FECHA"),
                ReceptionDate = GetNullableDateTime(reader, "DRECEPCION"),
                ClientCode = reader.GetInt32OrDefault("CLIENT"),
                ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
                FinisherCode = reader.GetInt32OrDefault("ACABADOR"),
                FinisherName = reader.GetStringOrEmpty("NOMACABADOR"),
                ClientReferenceCode = reader.GetStringOrEmpty("CODICLIENT"),
                ClientColor = reader.GetStringOrEmpty("COLORCLIENTE"),
                Color = reader.GetStringOrEmpty("COLOR"),
                OrderReference = reader.GetStringOrEmpty("COMANDA"),
                TotalPieces = reader.GetDecimalOrDefault("TOTALPIEZAS"),
                TotalKilograms = reader.GetDecimalOrDefault("TOTALKG"),
                LinesCount = reader.GetInt32OrDefault("LINEAS"),
                IsReceived = reader.GetBooleanValue("RECIBIDO"),
                IsCancelled = reader.GetBooleanValue("ANULADA"),
                Origin = reader.GetStringOrEmpty("origin")
            });
        }

        return new DisposicionSearchResultDto
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<DisposicionDetailDto?> GetByCodeAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return null;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT d.CODI,
                   d.CENTRO,
                   d.ANY,
                   d.IDDISPOS,
                   d.FECHA,
                   d.DRECEPCION,
                   d.ACABADOR,
                   COALESCE(finisher.NOM, '') AS NOMACABADOR,
                   d.CLIENT,
                   COALESCE(client.NOM, '') AS NOMCLIENT,
                   d.CODICLIENT,
                   d.OBSERV,
                   d.COLORCLIENTE,
                   d.COLOR,
                   d.TOTALPIEZAS,
                   d.TOTALKG,
                   d.RECIBIDO,
                   d.ANULADA,
                   d.COMANDA,
                   d.origin
            FROM dispos d
            LEFT JOIN tallers finisher
                ON finisher.CENTRO = d.CENTRO
               AND finisher.CODI = d.ACABADOR
               AND finisher.is_deleted = 0
            LEFT JOIN clients client
                ON client.CENTRO = d.CENTRO
               AND client.CODI = d.CLIENT
               AND client.is_deleted = 0
            WHERE d.CENTRO = @centerCode
              AND d.CODI = @code
              AND d.is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        var detail = new DisposicionDetailDto
        {
            Code = reader.GetInt32OrDefault("CODI"),
            CompanyCenterCode = reader.GetStringOrEmpty("CENTRO"),
            Year = reader.GetStringOrEmpty("ANY"),
            Number = reader.GetInt32OrDefault("IDDISPOS"),
            Date = GetNullableDateTime(reader, "FECHA"),
            ReceptionDate = GetNullableDateTime(reader, "DRECEPCION"),
            FinisherCode = reader.GetInt32OrDefault("ACABADOR"),
            FinisherName = reader.GetStringOrEmpty("NOMACABADOR"),
            ClientCode = reader.GetInt32OrDefault("CLIENT"),
            ClientName = reader.GetStringOrEmpty("NOMCLIENT"),
            ClientReferenceCode = reader.GetStringOrEmpty("CODICLIENT"),
            Notes = reader.GetStringOrEmpty("OBSERV"),
            ClientColor = reader.GetStringOrEmpty("COLORCLIENTE"),
            Color = reader.GetStringOrEmpty("COLOR"),
            TotalPieces = reader.GetDecimalOrDefault("TOTALPIEZAS"),
            TotalKilograms = reader.GetDecimalOrDefault("TOTALKG"),
            IsReceived = reader.GetBooleanValue("RECIBIDO"),
            IsCancelled = reader.GetBooleanValue("ANULADA"),
            OrderReference = reader.GetStringOrEmpty("COMANDA"),
            Origin = reader.GetStringOrEmpty("origin")
        };

        await reader.CloseAsync();
        detail.Lines = await LoadLinesAsync(connection, centerCode, detail.Code, cancellationToken);
        return detail;
    }

    public async Task<int> SaveAsync(SaveDisposicionCommand command, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return 0;
        }

        await EnsureCompanyAccessAsync(command.TenantId, command.CompanyId, cancellationToken);
        EnsureTenantWriteAccess();
        NormalizeAndValidate(command);
        DisposicionDetailDto? previous = null;
        if (command.Code.HasValue && command.Code.Value > 0)
        {
            previous = await GetByCodeAsync(command.TenantId, command.CompanyId, command.Code.Value, cancellationToken);
            if (previous is null)
            {
                throw new InvalidOperationException("No se ha encontrado la disposición que intentas modificar.");
            }
        }

        ApplyHeaderCheckboxRules(command);
        ValidateStockManagedLines(command);
        ValidateProtectedStateChanges(previous, command);

        var centerCode = await ResolveCompanyCenterCodeAsync(command.TenantId, command.CompanyId, cancellationToken);
        var calculatedTotals = CalculateTotals(command.Lines);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var code = command.Code.GetValueOrDefault();
        if (code <= 0)
        {
            code = await GenerateNextCodeAsync(connection, transaction, centerCode, cancellationToken);
        }

        if (command.Number <= 0)
        {
            command.Number = await GenerateNextDisplayNumberAsync(connection, transaction, centerCode, command.Year, cancellationToken);
        }

        await ApplyStockTransitionsAsync(connection, transaction, centerCode, code, previous, command, cancellationToken);

        if (previous is not null)
        {
            await using var updateCommand = connection.CreateCommand();
            updateCommand.Transaction = transaction;
            updateCommand.CommandText =
                """
                UPDATE dispos
                SET CODICLIENT = @clientReferenceCode,
                    ANY = @year,
                    IDDISPOS = @number,
                    FECHA = @date,
                    DRECEPCION = @receptionDate,
                    ACABADOR = @finisherCode,
                    ANULADA = @isCancelled,
                    CLIENT = @clientCode,
                    OBSERV = @notes,
                    COLORCLIENTE = @clientColor,
                    TOTALPIEZAS = @totalPieces,
                    TOTALKG = @totalKilograms,
                    COLOR = @color,
                    RECIBIDO = @isReceived,
                    COMANDA = @orderReference,
                    origin = 'local',
                    is_deleted = 0,
                    synced_utc = NULL
                WHERE CENTRO = @centerCode
                  AND CODI = @code;
                """;
            FillHeaderParameters(updateCommand, centerCode, code, command, calculatedTotals);
            var affected = await updateCommand.ExecuteNonQueryAsync(cancellationToken);
            if (affected == 0)
            {
                throw new InvalidOperationException("No se ha podido actualizar la disposición.");
            }

            await ReplaceLinesAsync(connection, transaction, centerCode, code, command.Lines, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            await WriteAuditAsync(
                command.TenantId,
                command.CompanyId,
                code,
                "DisposicionUpdated",
                $"Disposición {FormatDisposicionLabel(command.Year, command.Number)} actualizada; líneas={command.Lines.Count}; origen local.",
                cancellationToken);

            return code;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO dispos (
                CODI,
                CODICLIENT,
                CENTRO,
                ANY,
                IDDISPOS,
                FECHA,
                DRECEPCION,
                ACABADOR,
                ANULADA,
                CLIENT,
                OBSERV,
                COLORCLIENTE,
                TOTALPIEZAS,
                TOTALKG,
                COLOR,
                RECIBIDO,
                COMANDA,
                origin,
                is_deleted,
                synced_utc)
            VALUES (
                @code,
                @clientReferenceCode,
                @centerCode,
                @year,
                @number,
                @date,
                @receptionDate,
                @finisherCode,
                @isCancelled,
                @clientCode,
                @notes,
                @clientColor,
                @totalPieces,
                @totalKilograms,
                @color,
                @isReceived,
                @orderReference,
                'local',
                0,
                NULL);
            """;
        FillHeaderParameters(insertCommand, centerCode, code, command, calculatedTotals);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        await ReplaceLinesAsync(connection, transaction, centerCode, code, command.Lines, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        await WriteAuditAsync(
            command.TenantId,
            command.CompanyId,
            code,
            "DisposicionCreated",
            $"Disposición {FormatDisposicionLabel(command.Year, command.Number)} creada; líneas={command.Lines.Count}; origen local.",
            cancellationToken);

        return code;
    }

    public async Task DeleteAsync(Guid tenantId, Guid companyId, int code, CancellationToken cancellationToken = default)
    {
        if (!_connectionFactory.IsConfigured)
        {
            return;
        }

        await EnsureCompanyAccessAsync(tenantId, companyId, cancellationToken);
        EnsureTenantWriteAccess();
        var centerCode = await ResolveCompanyCenterCodeAsync(tenantId, companyId, cancellationToken);

        await using var connection = await _connectionFactory.OpenConnectionAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await using (var deleteLinesCommand = connection.CreateCommand())
        {
            deleteLinesCommand.Transaction = transaction;
            deleteLinesCommand.CommandText =
                """
                DELETE FROM ddispos
                WHERE CENTRO = @centerCode
                  AND DISPOS = @code;
                """;
            deleteLinesCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteLinesCommand.Parameters.AddWithValue("@code", code);
            await deleteLinesCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        await using var deleteCommand = connection.CreateCommand();
        deleteCommand.Transaction = transaction;
        deleteCommand.CommandText =
            """
            UPDATE dispos
            SET origin = 'local',
                is_deleted = 1,
                synced_utc = NULL
            WHERE CENTRO = @centerCode
              AND CODI = @code;
            """;
        deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
        deleteCommand.Parameters.AddWithValue("@code", code);
        var affected = await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException("No se ha encontrado la disposición a eliminar.");
        }

        await transaction.CommitAsync(cancellationToken);

        await WriteAuditAsync(
            tenantId,
            companyId,
            code,
            "DisposicionDeleted",
            $"Disposición {code} eliminada en local.",
            cancellationToken);
    }

    private static void FillSearchParameters(
        MySqlCommand command,
        string centerCode,
        bool includeCancelled,
        string receivedMode,
        string search,
        string likeSearch)
    {
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@includeCancelled", includeCancelled);
        command.Parameters.AddWithValue("@receivedMode", receivedMode);
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@likeSearch", likeSearch);
    }

    private static void FillHeaderParameters(
        MySqlCommand command,
        string centerCode,
        int code,
        SaveDisposicionCommand model,
        (decimal TotalPieces, decimal TotalKilograms) totals)
    {
        command.Parameters.AddWithValue("@code", code);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@clientReferenceCode", DbValue(model.ClientReferenceCode));
        command.Parameters.AddWithValue("@year", model.Year);
        command.Parameters.AddWithValue("@number", model.Number);
        command.Parameters.AddWithValue("@date", model.Date ?? DateTime.Today);
        command.Parameters.AddWithValue("@receptionDate", model.ReceptionDate.HasValue ? model.ReceptionDate.Value : DBNull.Value);
        command.Parameters.AddWithValue("@finisherCode", model.FinisherCode);
        command.Parameters.AddWithValue("@isCancelled", model.IsCancelled);
        command.Parameters.AddWithValue("@clientCode", model.ClientCode);
        command.Parameters.AddWithValue("@notes", DbValue(model.Notes));
        command.Parameters.AddWithValue("@clientColor", DbValue(model.ClientColor));
        command.Parameters.AddWithValue("@totalPieces", totals.TotalPieces);
        command.Parameters.AddWithValue("@totalKilograms", totals.TotalKilograms);
        command.Parameters.AddWithValue("@color", DbValue(model.Color));
        command.Parameters.AddWithValue("@isReceived", model.IsReceived);
        command.Parameters.AddWithValue("@orderReference", DbValue(model.OrderReference));
    }

    private static async Task<List<DisposicionLineDto>> LoadLinesAsync(
        MySqlConnection connection,
        string centerCode,
        int code,
        CancellationToken cancellationToken)
    {
        var items = new List<DisposicionLineDto>();
        await using var command = connection.CreateCommand();
        command.CommandText =
            """
            SELECT detail_rows.LINEA,
                   detail_rows.DESCRIPCIO,
                   detail_rows.TEJEDOR,
                   COALESCE(weaver.NOM, '') AS NOMTEJEDOR,
                   detail_rows.NALBARAN,
                   detail_rows.TEJIDO,
                   detail_rows.COMPOS,
                   detail_rows.NPIEZAS,
                   detail_rows.TOTALPIEZAS,
                   detail_rows.TOTALKG,
                   detail_rows.ACABADO,
                   detail_rows.ANCHO,
                   detail_rows.GRAMAJE,
                   detail_rows.RENDIMIENTO,
                   detail_rows.SERVIDO,
                   detail_rows.DISPUESTO
            FROM ddispos detail_rows
            LEFT JOIN tallers weaver
                ON weaver.CENTRO = detail_rows.CENTRO
               AND weaver.CODI = detail_rows.TEJEDOR
               AND weaver.is_deleted = 0
            WHERE detail_rows.CENTRO = @centerCode
              AND detail_rows.DISPOS = @code
            ORDER BY detail_rows.LINEA;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@code", code);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            items.Add(new DisposicionLineDto
            {
                LineNumber = reader.GetInt32OrDefault("LINEA"),
                Description = reader.GetStringOrEmpty("DESCRIPCIO"),
                WeaverCode = reader.GetInt32OrDefault("TEJEDOR"),
                WeaverName = reader.GetStringOrEmpty("NOMTEJEDOR"),
                DeliveryNoteNumber = reader.GetStringOrEmpty("NALBARAN"),
                FabricCode = reader.GetStringOrEmpty("TEJIDO"),
                CompositionText = reader.GetStringOrEmpty("COMPOS"),
                PiecesText = reader.GetStringOrEmpty("NPIEZAS"),
                TotalPieces = reader.GetDecimalOrDefault("TOTALPIEZAS"),
                TotalKilograms = reader.GetDecimalOrDefault("TOTALKG"),
                FinishText = reader.GetStringOrEmpty("ACABADO"),
                WidthText = reader.GetStringOrEmpty("ANCHO"),
                GramWeight = reader.GetDecimalOrDefault("GRAMAJE"),
                Yield = reader.GetDecimalOrDefault("RENDIMIENTO"),
                IsServed = reader.GetBooleanValue("SERVIDO"),
                IsDisposed = reader.GetBooleanValue("DISPUESTO")
            });
        }

        return items;
    }

    private static async Task ReplaceLinesAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        int code,
        IReadOnlyList<SaveDisposicionLineInput> lines,
        CancellationToken cancellationToken)
    {
        await using (var deleteCommand = connection.CreateCommand())
        {
            deleteCommand.Transaction = transaction;
            deleteCommand.CommandText =
                """
                DELETE FROM ddispos
                WHERE CENTRO = @centerCode
                  AND DISPOS = @code;
                """;
            deleteCommand.Parameters.AddWithValue("@centerCode", centerCode);
            deleteCommand.Parameters.AddWithValue("@code", code);
            await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
        }

        if (lines.Count == 0)
        {
            return;
        }

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO ddispos (
                CENTRO,
                DESCRIPCIO,
                LINEA,
                DISPOS,
                TEJEDOR,
                NALBARAN,
                TEJIDO,
                COMPOS,
                NPIEZAS,
                TOTALPIEZAS,
                TOTALKG,
                ACABADO,
                ANCHO,
                GRAMAJE,
                RENDIMIENTO,
                SERVIDO,
                DISPUESTO)
            VALUES (
                @centerCode,
                @description,
                @lineNumber,
                @code,
                @weaverCode,
                @deliveryNoteNumber,
                @fabricCode,
                @compositionText,
                @piecesText,
                @totalPieces,
                @totalKilograms,
                @finishText,
                @widthText,
                @gramWeight,
                @yield,
                @isServed,
                @isDisposed);
            """;
        insertCommand.Parameters.Add("@centerCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@description", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@lineNumber", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@code", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@weaverCode", MySqlDbType.Int32);
        insertCommand.Parameters.Add("@deliveryNoteNumber", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@fabricCode", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@compositionText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@piecesText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@totalPieces", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@totalKilograms", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@finishText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@widthText", MySqlDbType.VarChar);
        insertCommand.Parameters.Add("@gramWeight", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@yield", MySqlDbType.Decimal);
        insertCommand.Parameters.Add("@isServed", MySqlDbType.Bit);
        insertCommand.Parameters.Add("@isDisposed", MySqlDbType.Bit);

        var orderedLines = lines.OrderBy(line => line.LineNumber).ToList();
        for (var index = 0; index < orderedLines.Count; index++)
        {
            var line = orderedLines[index];
            insertCommand.Parameters["@centerCode"].Value = centerCode;
            insertCommand.Parameters["@description"].Value = DbValue(line.Description);
            insertCommand.Parameters["@lineNumber"].Value = line.LineNumber <= 0 ? index + 1 : line.LineNumber;
            insertCommand.Parameters["@code"].Value = code;
            insertCommand.Parameters["@weaverCode"].Value = line.WeaverCode;
            insertCommand.Parameters["@deliveryNoteNumber"].Value = DbValue(line.DeliveryNoteNumber);
            insertCommand.Parameters["@fabricCode"].Value = DbValue(line.FabricCode);
            insertCommand.Parameters["@compositionText"].Value = DbValue(line.CompositionText);
            insertCommand.Parameters["@piecesText"].Value = DbValue(line.PiecesText);
            insertCommand.Parameters["@totalPieces"].Value = line.TotalPieces;
            insertCommand.Parameters["@totalKilograms"].Value = line.TotalKilograms;
            insertCommand.Parameters["@finishText"].Value = DbValue(line.FinishText);
            insertCommand.Parameters["@widthText"].Value = DbValue(line.WidthText);
            insertCommand.Parameters["@gramWeight"].Value = line.GramWeight;
            insertCommand.Parameters["@yield"].Value = line.Yield;
            insertCommand.Parameters["@isServed"].Value = line.IsServed;
            insertCommand.Parameters["@isDisposed"].Value = line.IsDisposed;
            await insertCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private static async Task<int> GenerateNextCodeAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(MAX(CODI), 0) + 1
            FROM dispos
            WHERE CENTRO = @centerCode;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static async Task<int> GenerateNextDisplayNumberAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string year,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            SELECT COALESCE(MAX(IDDISPOS), 0) + 1
            FROM dispos
            WHERE CENTRO = @centerCode
              AND ANY = @year;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@year", year);
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
    }

    private static (decimal TotalPieces, decimal TotalKilograms) CalculateTotals(IEnumerable<SaveDisposicionLineInput> lines)
    {
        var totalPieces = 0m;
        var totalKilograms = 0m;

        foreach (var line in lines)
        {
            totalPieces += line.TotalPieces;
            totalKilograms += line.TotalKilograms;
        }

        return (totalPieces, totalKilograms);
    }

    private async Task<string> ResolveCompanyCenterCodeAsync(Guid tenantId, Guid companyId, CancellationToken cancellationToken)
    {
        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId!.Value, tenantId, cancellationToken);
        var company = allowedCompanies.FirstOrDefault(item => item.CompanyId == companyId);
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

        throw new InvalidOperationException("No tienes permisos para editar disposiciones en este tenant.");
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

        if (!_activeCompanyContext.CompanyId.HasValue || _activeCompanyContext.CompanyId.Value != companyId)
        {
            throw new InvalidOperationException("La empresa activa no coincide con la empresa solicitada.");
        }

        var allowedCompanies = await _companyAccessService.GetAllowedCompaniesAsync(_currentUserContext.UserId.Value, tenantId, cancellationToken);
        if (!allowedCompanies.Any(company => company.CompanyId == companyId))
        {
            throw new InvalidOperationException("No tienes acceso a la empresa activa.");
        }
    }

    private async Task WriteAuditAsync(Guid tenantId, Guid companyId, int code, string action, string details, CancellationToken cancellationToken)
    {
        await _auditLogService.WriteAsync(new WriteAuditLogCommand
        {
            TenantId = tenantId,
            CompanyId = companyId,
            UserId = _currentUserContext.UserId,
            Action = action,
            EntityName = "Disposicion",
            EntityId = code.ToString(),
            Details = details
        }, cancellationToken);
    }

    private static void NormalizeAndValidate(SaveDisposicionCommand command)
    {
        command.Year = string.IsNullOrWhiteSpace(command.Year)
            ? DateTime.Today.Year.ToString()
            : command.Year.Trim();
        command.ClientReferenceCode = command.ClientReferenceCode.Trim();
        command.Notes = command.Notes.Trim();
        command.ClientColor = command.ClientColor.Trim();
        command.Color = command.Color.Trim();
        command.OrderReference = command.OrderReference.Trim();

        if (command.Year.Length > 10)
        {
            throw new InvalidOperationException("El año/serie de la disposición no puede superar 10 caracteres.");
        }

        if (command.Number < 0)
        {
            throw new InvalidOperationException("El número de disposición no puede ser negativo.");
        }

        if (command.FinisherCode < 0 || command.ClientCode < 0)
        {
            throw new InvalidOperationException("Cliente y acabador no pueden ser negativos.");
        }

        for (var index = 0; index < command.Lines.Count; index++)
        {
            var line = command.Lines[index];
            line.Description = line.Description.Trim();
            line.DeliveryNoteNumber = line.DeliveryNoteNumber.Trim();
            line.FabricCode = line.FabricCode.Trim();
            line.CompositionText = line.CompositionText.Trim();
            line.PiecesText = line.PiecesText.Trim();
            line.FinishText = line.FinishText.Trim();
            line.WidthText = line.WidthText.Trim();

            if (line.WeaverCode < 0 ||
                line.TotalPieces < 0 ||
                line.TotalKilograms < 0 ||
                line.GramWeight < 0 ||
                line.Yield < 0)
            {
                throw new InvalidOperationException($"La línea {index + 1} de la disposición no admite valores negativos.");
            }
        }
    }

    private static void ApplyHeaderCheckboxRules(SaveDisposicionCommand command)
    {
        if (command.IsCancelled)
        {
            command.IsReceived = false;
            command.ReceptionDate = null;
            return;
        }

        if (command.IsReceived)
        {
            if (command.Lines.Any(line => !line.IsServed))
            {
                throw new InvalidOperationException("Para marcar la disposición como recibida, todas las líneas deben estar servidas.");
            }

            command.ReceptionDate ??= command.Date ?? DateTime.Today;
            return;
        }

        command.ReceptionDate = null;
    }

    private static void ValidateStockManagedLines(SaveDisposicionCommand command)
    {
        foreach (var line in command.Lines)
        {
            if (line.IsServed && !line.IsDisposed)
            {
                throw new InvalidOperationException($"La línea {line.LineNumber} no puede marcarse como servida sin estar antes dispuesta.");
            }

            if ((line.IsDisposed || line.IsServed) && string.IsNullOrWhiteSpace(line.FabricCode))
            {
                throw new InvalidOperationException($"La línea {line.LineNumber} debe indicar un tejido antes de mover stock.");
            }
        }
    }

    private static void ValidateProtectedStateChanges(DisposicionDetailDto? previous, SaveDisposicionCommand command)
    {
        if (previous is null)
        {
            return;
        }

        if (!string.Equals(previous.Color, command.Color, StringComparison.OrdinalIgnoreCase) &&
            previous.Lines.Any(line => line.IsServed))
        {
            throw new InvalidOperationException("No puedes cambiar el color de la disposición mientras existan líneas ya servidas.");
        }

        var currentByLine = command.Lines.ToDictionary(line => line.LineNumber);
        foreach (var previousLine in previous.Lines)
        {
            if (!currentByLine.TryGetValue(previousLine.LineNumber, out var currentLine))
            {
                continue;
            }

            if (!previousLine.IsDisposed && !previousLine.IsServed)
            {
                continue;
            }

            if (!string.Equals(previousLine.FabricCode, currentLine.FabricCode, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"La línea {previousLine.LineNumber} ya tiene movimientos de stock y no puede cambiar de tejido.");
            }

            if (previousLine.TotalKilograms != currentLine.TotalKilograms || previousLine.Yield != currentLine.Yield)
            {
                throw new InvalidOperationException($"La línea {previousLine.LineNumber} ya tiene movimientos de stock y no puede cambiar kilos o rendimiento.");
            }
        }
    }

    private static async Task ApplyStockTransitionsAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        int dispositionCode,
        DisposicionDetailDto? previous,
        SaveDisposicionCommand current,
        CancellationToken cancellationToken)
    {
        var previousByLine = previous?.Lines.ToDictionary(line => line.LineNumber, ToStockLineSnapshot)
            ?? new Dictionary<int, StockLineSnapshot>();
        var currentByLine = current.Lines.ToDictionary(line => line.LineNumber, ToStockLineSnapshot);

        foreach (var previousLine in previousByLine.Values.Where(line => !currentByLine.ContainsKey(line.LineNumber)).OrderBy(line => line.LineNumber))
        {
            if (previousLine.IsServed)
            {
                await ApplyServedTransitionAsync(
                    connection,
                    transaction,
                    current.TenantId,
                    current.CompanyId,
                    centerCode,
                    dispositionCode,
                    current.Year,
                    current.Number,
                    previous!.Color,
                    current.Date,
                    previousLine,
                    false,
                    cancellationToken);
            }

            if (previousLine.IsDisposed)
            {
                await ApplyDisposedTransitionAsync(
                    connection,
                    transaction,
                    current.TenantId,
                    current.CompanyId,
                    centerCode,
                    dispositionCode,
                    current.Year,
                    current.Number,
                    previous!.Color,
                    current.Date,
                    previousLine,
                    false,
                    cancellationToken);
            }
        }

        foreach (var currentLine in currentByLine.Values.OrderBy(line => line.LineNumber))
        {
            if (!previousByLine.TryGetValue(currentLine.LineNumber, out var previousLine))
            {
                if (currentLine.IsDisposed)
                {
                    await ApplyDisposedTransitionAsync(
                        connection,
                        transaction,
                        current.TenantId,
                        current.CompanyId,
                        centerCode,
                        dispositionCode,
                        current.Year,
                        current.Number,
                        current.Color,
                        current.Date,
                        currentLine,
                        true,
                        cancellationToken);
                }

                if (currentLine.IsServed)
                {
                    await ApplyServedTransitionAsync(
                        connection,
                        transaction,
                        current.TenantId,
                        current.CompanyId,
                        centerCode,
                        dispositionCode,
                        current.Year,
                        current.Number,
                        current.Color,
                        current.Date,
                        currentLine,
                        true,
                        cancellationToken);
                }

                continue;
            }

            if (previousLine.IsServed && !currentLine.IsServed)
            {
                await ApplyServedTransitionAsync(
                    connection,
                    transaction,
                    current.TenantId,
                    current.CompanyId,
                    centerCode,
                    dispositionCode,
                    current.Year,
                    current.Number,
                    previous!.Color,
                    current.Date,
                    previousLine,
                    false,
                    cancellationToken);
            }

            if (previousLine.IsDisposed && !currentLine.IsDisposed)
            {
                await ApplyDisposedTransitionAsync(
                    connection,
                    transaction,
                    current.TenantId,
                    current.CompanyId,
                    centerCode,
                    dispositionCode,
                    current.Year,
                    current.Number,
                    previous!.Color,
                    current.Date,
                    previousLine,
                    false,
                    cancellationToken);
            }

            if (!previousLine.IsDisposed && currentLine.IsDisposed)
            {
                await ApplyDisposedTransitionAsync(
                    connection,
                    transaction,
                    current.TenantId,
                    current.CompanyId,
                    centerCode,
                    dispositionCode,
                    current.Year,
                    current.Number,
                    current.Color,
                    current.Date,
                    currentLine,
                    true,
                    cancellationToken);
            }

            if (!previousLine.IsServed && currentLine.IsServed)
            {
                await ApplyServedTransitionAsync(
                    connection,
                    transaction,
                    current.TenantId,
                    current.CompanyId,
                    centerCode,
                    dispositionCode,
                    current.Year,
                    current.Number,
                    current.Color,
                    current.Date,
                    currentLine,
                    true,
                    cancellationToken);
            }
        }
    }

    private static StockLineSnapshot ToStockLineSnapshot(DisposicionLineDto line) =>
        new(
            line.LineNumber,
            line.FabricCode.Trim().ToUpperInvariant(),
            line.Description.Trim(),
            line.TotalKilograms,
            line.Yield,
            line.IsServed,
            line.IsDisposed);

    private static StockLineSnapshot ToStockLineSnapshot(SaveDisposicionLineInput line) =>
        new(
            line.LineNumber,
            line.FabricCode.Trim().ToUpperInvariant(),
            line.Description.Trim(),
            line.TotalKilograms,
            line.Yield,
            line.IsServed,
            line.IsDisposed);

    private static async Task ApplyDisposedTransitionAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        int dispositionCode,
        string dispositionYear,
        int dispositionNumber,
        string dispositionColor,
        DateTime? dispositionDate,
        StockLineSnapshot line,
        bool setDisposed,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(line.FabricCode) || line.TotalKilograms <= 0)
        {
            return;
        }

        var meters = CalculateMeters(line.TotalKilograms, line.Yield);
        if (setDisposed)
        {
            await EnsureEnoughFabricStockAsync(connection, transaction, centerCode, line.FabricCode, "STCRUK", line.TotalKilograms, "No hay suficiente stock crudo del tejido para marcar la línea como dispuesta.", cancellationToken);
            await UpdateFabricStockAsync(connection, transaction, centerCode, line.FabricCode, -line.TotalKilograms, -meters, line.TotalKilograms, meters, cancellationToken);
            await InsertStockMovementAsync(
                connection,
                transaction,
                tenantId,
                companyId,
                StockMovementTypes.OutboundTextileDispositionRaw,
                dispositionDate,
                TextileWarehouses.Raw,
                line.FabricCode,
                line.Description,
                dispositionColor,
                line.TotalKilograms,
                "kg",
                dispositionCode,
                dispositionYear,
                dispositionNumber,
                line.LineNumber,
                $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: crudo -> dispuesto (salida de crudo).",
                cancellationToken);
            await InsertStockMovementAsync(
                connection,
                transaction,
                tenantId,
                companyId,
                StockMovementTypes.InboundTextileDispositionDisposed,
                dispositionDate,
                TextileWarehouses.Disposed,
                line.FabricCode,
                line.Description,
                dispositionColor,
                line.TotalKilograms,
                "kg",
                dispositionCode,
                dispositionYear,
                dispositionNumber,
                line.LineNumber,
                $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: crudo -> dispuesto (entrada a dispuesto).",
                cancellationToken);
            return;
        }

        await EnsureEnoughFabricStockAsync(connection, transaction, centerCode, line.FabricCode, "STDISPK", line.TotalKilograms, "No hay suficiente stock dispuesto del tejido para desmarcar la línea como dispuesta.", cancellationToken);
        await UpdateFabricStockAsync(connection, transaction, centerCode, line.FabricCode, line.TotalKilograms, meters, -line.TotalKilograms, -meters, cancellationToken);
        await InsertStockMovementAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            StockMovementTypes.OutboundTextileDispositionDisposed,
            dispositionDate,
            TextileWarehouses.Disposed,
            line.FabricCode,
            line.Description,
            dispositionColor,
            line.TotalKilograms,
            "kg",
            dispositionCode,
            dispositionYear,
            dispositionNumber,
            line.LineNumber,
            $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: revertir dispuesto -> crudo (salida de dispuesto).",
            cancellationToken);
        await InsertStockMovementAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            StockMovementTypes.InboundTextileDispositionRaw,
            dispositionDate,
            TextileWarehouses.Raw,
            line.FabricCode,
            line.Description,
            dispositionColor,
            line.TotalKilograms,
            "kg",
            dispositionCode,
            dispositionYear,
            dispositionNumber,
            line.LineNumber,
            $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: revertir dispuesto -> crudo (entrada a crudo).",
            cancellationToken);
    }

    private static async Task ApplyServedTransitionAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string centerCode,
        int dispositionCode,
        string dispositionYear,
        int dispositionNumber,
        string dispositionColor,
        DateTime? dispositionDate,
        StockLineSnapshot line,
        bool setServed,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(line.FabricCode) || line.TotalKilograms <= 0)
        {
            return;
        }

        var meters = CalculateMeters(line.TotalKilograms, line.Yield);
        var color = dispositionColor.Trim();
        if (setServed)
        {
            await EnsureColorDetailAsync(connection, transaction, centerCode, line.FabricCode, color, cancellationToken);
            await UpdateColorActualAsync(connection, transaction, centerCode, line.FabricCode, color, meters, cancellationToken);
            await UpdateFabricStockAsync(connection, transaction, centerCode, line.FabricCode, 0, 0, -line.TotalKilograms, -meters, cancellationToken);
            await InsertStockMovementAsync(
                connection,
                transaction,
                tenantId,
                companyId,
                StockMovementTypes.OutboundTextileDispositionDisposed,
                dispositionDate,
                TextileWarehouses.Disposed,
                line.FabricCode,
                line.Description,
                color,
                line.TotalKilograms,
                "kg",
                dispositionCode,
                dispositionYear,
                dispositionNumber,
                line.LineNumber,
                $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: dispuesto -> color (salida de dispuesto).",
                cancellationToken);
            await InsertStockMovementAsync(
                connection,
                transaction,
                tenantId,
                companyId,
                StockMovementTypes.InboundTextileDispositionColor,
                dispositionDate,
                TextileWarehouses.Colored,
                line.FabricCode,
                line.Description,
                color,
                meters,
                "m",
                dispositionCode,
                dispositionYear,
                dispositionNumber,
                line.LineNumber,
                $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: dispuesto -> color (entrada a color).",
                cancellationToken);
            return;
        }

        await UpdateColorActualAsync(connection, transaction, centerCode, line.FabricCode, color, -meters, cancellationToken);
        await UpdateFabricStockAsync(connection, transaction, centerCode, line.FabricCode, 0, 0, line.TotalKilograms, meters, cancellationToken);
        await InsertStockMovementAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            StockMovementTypes.OutboundTextileDispositionColor,
            dispositionDate,
            TextileWarehouses.Colored,
            line.FabricCode,
            line.Description,
            color,
            meters,
            "m",
            dispositionCode,
            dispositionYear,
            dispositionNumber,
            line.LineNumber,
            $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: revertir color -> dispuesto (salida de color).",
            cancellationToken);
        await InsertStockMovementAsync(
            connection,
            transaction,
            tenantId,
            companyId,
            StockMovementTypes.InboundTextileDispositionDisposed,
            dispositionDate,
            TextileWarehouses.Disposed,
            line.FabricCode,
            line.Description,
            color,
            line.TotalKilograms,
            "kg",
            dispositionCode,
            dispositionYear,
            dispositionNumber,
            line.LineNumber,
            $"Disposición {FormatDisposicionLabel(dispositionYear, dispositionNumber)}: revertir color -> dispuesto (entrada a dispuesto).",
            cancellationToken);
    }

    private static async Task InsertStockMovementAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        Guid tenantId,
        Guid companyId,
        string movementType,
        DateTime? movementDate,
        string warehouse,
        string itemCode,
        string itemDescription,
        string color,
        decimal quantity,
        string unitOfMeasure,
        int dispositionCode,
        string dispositionYear,
        int dispositionNumber,
        int lineNumber,
        string notes,
        CancellationToken cancellationToken)
    {
        if (quantity <= 0)
        {
            return;
        }

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            INSERT INTO inventory_movements (
                movement_id,
                tenant_id,
                company_id,
                movement_type,
                movement_date,
                warehouse,
                item_code,
                item_description,
                color,
                quantity,
                unit_of_measure,
                source_document_type,
                source_document_id,
                source_document_number,
                source_line_number,
                notes,
                created_utc)
            VALUES (
                @movementId,
                @tenantId,
                @companyId,
                @movementType,
                @movementDate,
                @warehouse,
                @itemCode,
                @itemDescription,
                @color,
                @quantity,
                @unitOfMeasure,
                @sourceDocumentType,
                @sourceDocumentId,
                @sourceDocumentNumber,
                @sourceLineNumber,
                @notes,
                @createdUtc);
            """;
        command.Parameters.AddWithValue("@movementId", Guid.NewGuid().ToString());
        command.Parameters.AddWithValue("@tenantId", tenantId.ToString());
        command.Parameters.AddWithValue("@companyId", companyId.ToString());
        command.Parameters.AddWithValue("@movementType", movementType);
        command.Parameters.AddWithValue("@movementDate", (movementDate ?? DateTime.Today).Date);
        command.Parameters.AddWithValue("@warehouse", DbValue(warehouse));
        command.Parameters.AddWithValue("@itemCode", DbValue(itemCode));
        command.Parameters.AddWithValue("@itemDescription", string.IsNullOrWhiteSpace(itemDescription) ? itemCode : itemDescription);
        command.Parameters.AddWithValue("@color", DbValue(color));
        command.Parameters.AddWithValue("@quantity", quantity);
        command.Parameters.AddWithValue("@unitOfMeasure", DbValue(unitOfMeasure));
        command.Parameters.AddWithValue("@sourceDocumentType", "TextileDisposition");
        command.Parameters.AddWithValue("@sourceDocumentId", Guid.NewGuid().ToString());
        command.Parameters.AddWithValue("@sourceDocumentNumber", dispositionNumber);
        command.Parameters.AddWithValue("@sourceLineNumber", lineNumber);
        command.Parameters.AddWithValue("@notes", DbValue($"{notes} Código interno={dispositionCode}; Año={dispositionYear}."));
        command.Parameters.AddWithValue("@createdUtc", DateTime.UtcNow);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task EnsureEnoughFabricStockAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string fabricCode,
        string stockColumn,
        decimal needed,
        string errorMessage,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            $"""
            SELECT COALESCE({stockColumn}, 0)
            FROM teixits
            WHERE CENTRO = @centerCode
              AND CODI = @fabricCode
              AND is_deleted = 0
            LIMIT 1;
            """;
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@fabricCode", fabricCode);
        var value = Convert.ToDecimal(await command.ExecuteScalarAsync(cancellationToken) ?? 0m);
        if (value < needed)
        {
            throw new InvalidOperationException(errorMessage);
        }
    }

    private static async Task UpdateFabricStockAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string fabricCode,
        decimal rawKilogramsDelta,
        decimal rawMetersDelta,
        decimal disposedKilogramsDelta,
        decimal disposedMetersDelta,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE teixits
            SET STCRUK = STCRUK + @rawKilogramsDelta,
                STCRUM = STCRUM + @rawMetersDelta,
                STDISPK = STDISPK + @disposedKilogramsDelta,
                STDISPM = STDISPM + @disposedMetersDelta
            WHERE CENTRO = @centerCode
              AND CODI = @fabricCode
              AND is_deleted = 0;
            """;
        command.Parameters.AddWithValue("@rawKilogramsDelta", rawKilogramsDelta);
        command.Parameters.AddWithValue("@rawMetersDelta", rawMetersDelta);
        command.Parameters.AddWithValue("@disposedKilogramsDelta", disposedKilogramsDelta);
        command.Parameters.AddWithValue("@disposedMetersDelta", disposedMetersDelta);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@fabricCode", fabricCode);
        var affected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
        {
            throw new InvalidOperationException($"No se ha encontrado el tejido {fabricCode} para actualizar stock.");
        }
    }

    private static async Task EnsureColorDetailAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string fabricCode,
        string color,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(color))
        {
            return;
        }

        await using var existsCommand = connection.CreateCommand();
        existsCommand.Transaction = transaction;
        existsCommand.CommandText =
            """
            SELECT COUNT(*)
            FROM teixits_color_detail
            WHERE CENTRO = @centerCode
              AND TEIXIT_CODI = @fabricCode
              AND COALESCE(COLOR, '') = @color;
            """;
        existsCommand.Parameters.AddWithValue("@centerCode", centerCode);
        existsCommand.Parameters.AddWithValue("@fabricCode", fabricCode);
        existsCommand.Parameters.AddWithValue("@color", color);
        var exists = Convert.ToInt32(await existsCommand.ExecuteScalarAsync(cancellationToken)) > 0;
        if (exists)
        {
            return;
        }

        await using var lineCommand = connection.CreateCommand();
        lineCommand.Transaction = transaction;
        lineCommand.CommandText =
            """
            SELECT COALESCE(MAX(LINE_NUMBER), 0) + 1
            FROM teixits_color_detail
            WHERE CENTRO = @centerCode
              AND TEIXIT_CODI = @fabricCode;
            """;
        lineCommand.Parameters.AddWithValue("@centerCode", centerCode);
        lineCommand.Parameters.AddWithValue("@fabricCode", fabricCode);
        var nextLineNumber = Convert.ToInt32(await lineCommand.ExecuteScalarAsync(cancellationToken));

        await using var insertCommand = connection.CreateCommand();
        insertCommand.Transaction = transaction;
        insertCommand.CommandText =
            """
            INSERT INTO teixits_color_detail (
                CENTRO,
                TEIXIT_CODI,
                LINE_NUMBER,
                PROVE,
                COLOR,
                ACTUAL,
                MINIM,
                TINTAR,
                PREU,
                METRES,
                KG,
                OBSERV)
            VALUES (
                @centerCode,
                @fabricCode,
                @lineNumber,
                0,
                @color,
                0,
                0,
                0,
                0,
                0,
                0,
                NULL);
            """;
        insertCommand.Parameters.AddWithValue("@centerCode", centerCode);
        insertCommand.Parameters.AddWithValue("@fabricCode", fabricCode);
        insertCommand.Parameters.AddWithValue("@lineNumber", nextLineNumber);
        insertCommand.Parameters.AddWithValue("@color", color);
        await insertCommand.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task UpdateColorActualAsync(
        MySqlConnection connection,
        MySqlTransaction transaction,
        string centerCode,
        string fabricCode,
        string color,
        decimal deltaMeters,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(color) || deltaMeters == 0)
        {
            return;
        }

        await using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText =
            """
            UPDATE teixits_color_detail
            SET ACTUAL = ACTUAL + @deltaMeters
            WHERE CENTRO = @centerCode
              AND TEIXIT_CODI = @fabricCode
              AND COALESCE(COLOR, '') = @color;
            """;
        command.Parameters.AddWithValue("@deltaMeters", deltaMeters);
        command.Parameters.AddWithValue("@centerCode", centerCode);
        command.Parameters.AddWithValue("@fabricCode", fabricCode);
        command.Parameters.AddWithValue("@color", color);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static decimal CalculateMeters(decimal kilograms, decimal yield)
    {
        var effectiveYield = yield == 0 ? 1m : yield;
        return Math.Round(kilograms * effectiveYield, 2, MidpointRounding.AwayFromZero);
    }

    private static class TextileWarehouses
    {
        public const string Raw = "TEJIDO-CRUDO";
        public const string Disposed = "TEJIDO-DISPUESTO";
        public const string Colored = "TEJIDO-COLOR";
    }

    private static string BuildSearchOrderByClause(DisposicionFilter filter)
    {
        var column = filter.SortColumn switch
        {
            nameof(DisposicionListItemDto.Code) => "d.CODI",
            nameof(DisposicionListItemDto.Number) => "d.IDDISPOS",
            nameof(DisposicionListItemDto.Date) => "d.FECHA",
            nameof(DisposicionListItemDto.ReceptionDate) => "d.DRECEPCION",
            nameof(DisposicionListItemDto.ClientName) => "NOMCLIENT",
            nameof(DisposicionListItemDto.FinisherName) => "NOMACABADOR",
            nameof(DisposicionListItemDto.Color) => "d.COLOR",
            nameof(DisposicionListItemDto.TotalPieces) => "d.TOTALPIEZAS",
            nameof(DisposicionListItemDto.TotalKilograms) => "d.TOTALKG",
            nameof(DisposicionListItemDto.Origin) => "d.origin",
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(column))
        {
            return "ORDER BY d.FECHA DESC, d.ANY DESC, d.IDDISPOS DESC, d.CODI DESC";
        }

        var direction = filter.SortDescending ? "DESC" : "ASC";
        return $"ORDER BY {column} {direction}, d.ANY DESC, d.IDDISPOS DESC, d.CODI DESC";
    }

    private static DateTime? GetNullableDateTime(MySqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal))
        {
            return null;
        }

        var value = reader.GetValue(ordinal);
        return value switch
        {
            DateTime dateTime => dateTime,
            string stringValue when DateTime.TryParse(stringValue, out var parsed) => parsed,
            _ => Convert.ToDateTime(value)
        };
    }

    private static object DbValue(string value) =>
        string.IsNullOrWhiteSpace(value) ? DBNull.Value : value;

    private static string FormatDisposicionLabel(string year, int number) =>
        string.IsNullOrWhiteSpace(year) ? number.ToString() : $"{number}-{year}";
}
