using Erp.Application.Acabados;
using Erp.Application.BaseData;
using Erp.Application.Clients;
using Erp.Application.Disposiciones;
using Erp.Application.Fornituras;
using Erp.Application.Hilos;
using Erp.Application.Mailing;
using Erp.Application.Models;
using Erp.Application.Muestras;
using Erp.Application.Purchases;
using Erp.Application.Representatives;
using Erp.Application.Sales;
using Erp.Application.Stock;
using Erp.Application.Suppliers;
using Erp.Application.Talleres;
using Erp.Application.Tejidos;
using Erp.Application.Transportistas;

namespace Erp.Application.Search;

public sealed class GlobalSearchService(
    IClienteQueries clienteQueries,
    IProveedorQueries proveedorQueries,
    IRepresentativeQueries representativeQueries,
    ITransportistaQueries transportistaQueries,
    ITallerQueries tallerQueries,
    IFornituraQueries fornituraQueries,
    IHiloQueries hiloQueries,
    ITejidoQueries tejidoQueries,
    IModeloQueries modeloQueries,
    IMuestraQueries muestraQueries,
    IDisposicionQueries disposicionQueries,
    ISalesOrderQueries salesOrderQueries,
    ISalesRemittanceQueries salesRemittanceQueries,
    IPurchaseOrderQueries purchaseOrderQueries,
    IPurchaseInvoiceQueries purchaseInvoiceQueries,
    IParteAcabadoQueries parteAcabadoQueries,
    IStockQueries stockQueries,
    IBaseCatalogQueries baseCatalogQueries,
    IMailingCampaignQueries mailingCampaignQueries) : IGlobalSearchService
{
    public async Task<GlobalSearchResultDto> SearchAsync(
        Guid tenantId,
        Guid companyId,
        GlobalSearchFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = filter.Search.Trim();
        if (query.Length < 2)
        {
            return GlobalSearchResultDto.Empty(query);
        }

        var displayLimit = Math.Clamp(filter.MaxResultsPerGroup, 1, 25);
        var queryLimit = Math.Max(10, displayLimit);
        var groups = new List<GlobalSearchGroupDto>();

        async Task TryAddAsync(Func<Task> addAsync)
        {
            try
            {
                await addAsync();
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch
            {
                // A broken legacy-backed module should not make the whole global search unusable.
            }
        }

        await TryAddAsync(async () =>
        {
            var clients = await clienteQueries.SearchAsync(
                tenantId,
                companyId,
                new ClienteFilter
                {
                    Search = query,
                    IncludeBlocked = true,
                    Page = 1,
                    PageSize = queryLimit,
                    SortColumn = nameof(ClienteListItemDto.Name)
                },
                cancellationToken);

            AddGroup(
                groups,
                "clients",
                "Clientes",
                clients.TotalCount,
                clients.Items
                    .Take(displayLimit)
                    .Select(client => new GlobalSearchItemDto
                    {
                        Kind = "client",
                        Badge = "Cliente",
                        Title = string.IsNullOrWhiteSpace(client.Name) ? $"Cliente {client.Code}" : client.Name,
                        Subtitle = BuildClientSubtitle(client),
                        Href = $"/crm/clientes/editar/{client.Code}",
                        Score = ScoreClient(query, client)
                    }));
        });

        await TryAddAsync(async () =>
        {
            var suppliers = await proveedorQueries.SearchAsync(
                tenantId,
                companyId,
                new ProveedorFilter
                {
                    Search = query,
                    Page = 1,
                    PageSize = queryLimit,
                    SortColumn = nameof(ProveedorListItemDto.Name)
                },
                cancellationToken);

            AddGroup(
                groups,
                "suppliers",
                "Proveedores",
                suppliers.TotalCount,
                suppliers.Items
                    .Take(displayLimit)
                    .Select(supplier => new GlobalSearchItemDto
                    {
                        Kind = "supplier",
                        Badge = "Proveedor",
                        Title = string.IsNullOrWhiteSpace(supplier.Name) ? $"Proveedor {supplier.Code}" : supplier.Name,
                        Subtitle = BuildEntitySubtitle(supplier.Code, supplier.TaxId, supplier.City, supplier.Email, supplier.Phone),
                        Href = $"/crm/proveedores/editar/{supplier.Code}",
                        Score = ScoreText(query, supplier.Code.ToString(), supplier.Name, supplier.TaxId, supplier.Email, supplier.City)
                    }));
        });

        await TryAddAsync(async () =>
        {
            var representatives = await representativeQueries.SearchAsync(
                tenantId,
                companyId,
                new RepresentativeFilter
                {
                    Search = query,
                    Page = 1,
                    PageSize = queryLimit,
                    SortColumn = nameof(RepresentativeListItemDto.Name)
                },
                cancellationToken);

            AddGroup(
                groups,
                "representatives",
                "Representantes",
                representatives.TotalCount,
                representatives.Items
                    .Take(displayLimit)
                    .Select(representative => new GlobalSearchItemDto
                    {
                        Kind = "representative",
                        Badge = "Representante",
                        Title = string.IsNullOrWhiteSpace(representative.Name) ? $"Representante {representative.Code}" : representative.Name,
                        Subtitle = BuildEntitySubtitle(representative.Code, representative.TaxId, representative.City, representative.Email, $"{representative.CommissionPercent:N2}% comisión"),
                        Href = $"/crm/representantes/editar/{representative.Code}",
                        Score = ScoreText(query, representative.Code.ToString(), representative.Name, representative.TaxId, representative.City, representative.Email)
                    }));
        });

        await TryAddAsync(async () =>
        {
            var carriers = await transportistaQueries.SearchAsync(
                tenantId,
                companyId,
                new TransportistaFilter
                {
                    Search = query,
                    Page = 1,
                    PageSize = queryLimit,
                    SortColumn = nameof(TransportistaListItemDto.Name)
                },
                cancellationToken);

            AddGroup(
                groups,
                "carriers",
                "Transportistas",
                carriers.TotalCount,
                carriers.Items
                    .Take(displayLimit)
                    .Select(carrier => new GlobalSearchItemDto
                    {
                        Kind = "carrier",
                        Badge = "Transportista",
                        Title = string.IsNullOrWhiteSpace(carrier.Name) ? $"Transportista {carrier.Code}" : carrier.Name,
                        Subtitle = BuildEntitySubtitle(carrier.Code, carrier.TaxId, carrier.City, carrier.Email, carrier.Phone),
                        Href = $"/crm/transportistas/editar/{carrier.Code}",
                        Score = ScoreText(query, carrier.Code.ToString(), carrier.Name, carrier.TaxId, carrier.City, carrier.Email)
                    }));
        });

        await TryAddAsync(async () =>
        {
            var workshops = await tallerQueries.SearchAsync(
                tenantId,
                companyId,
                new TallerFilter
                {
                    Search = query,
                    Page = 1,
                    PageSize = queryLimit,
                    SortColumn = nameof(TallerListItemDto.Name)
                },
                cancellationToken);

            AddGroup(
                groups,
                "workshops",
                "Talleres",
                workshops.TotalCount,
                workshops.Items
                    .Take(displayLimit)
                    .Select(workshop => new GlobalSearchItemDto
                    {
                        Kind = "workshop",
                        Badge = "Taller",
                        Title = string.IsNullOrWhiteSpace(workshop.Name) ? $"Taller {workshop.Code}" : workshop.Name,
                        Subtitle = BuildEntitySubtitle(workshop.Code, workshop.TaxId, workshop.City, workshop.PrimaryEmail, workshop.Phone),
                        Href = $"/crm/talleres/editar/{workshop.Code}",
                        Score = ScoreText(query, workshop.Code.ToString(), workshop.Name, workshop.TaxId, workshop.City, workshop.PrimaryEmail)
                    }));
        });

        await AddSalesGroupsAsync();
        await AddArticleGroupsAsync();
        await AddPurchaseGroupsAsync();
        await AddProductionGroupsAsync();
        await AddStockGroupsAsync();
        await AddBaseGroupsAsync();
        await AddMailingGroupsAsync();

        return new GlobalSearchResultDto
        {
            Query = query,
            Groups = groups
        };

        async Task AddSalesGroupsAsync()
        {
            await TryAddAsync(async () =>
            {
                var orders = await salesOrderQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new SalesOrderFilter
                    {
                        Search = query,
                        IncludeClosed = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(SalesOrderListItemDto.DocumentDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "sales-orders",
                    "Pedidos",
                    orders.TotalCount,
                    orders.Items
                        .OrderByDescending(order => order.DocumentDate)
                        .ThenByDescending(order => order.OrderNumber)
                        .Take(displayLimit)
                        .Select(order => new GlobalSearchItemDto
                        {
                            Kind = "sales-order",
                            Badge = "Pedido",
                            Title = $"Pedido {order.OrderNumber}",
                            Subtitle = BuildDocumentSubtitle(order.ClientName, order.Status, order.LineCount),
                            Href = $"/ventas/pedidos/editar/{order.OrderNumber}",
                            Date = order.DocumentDate,
                            Amount = order.TotalAmount,
                            Score = ScoreDocument(query, order.OrderNumber.ToString(), order.ClientName)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var shipments = await salesOrderQueries.SearchShipmentsAsync(
                    tenantId,
                    companyId,
                    new SalesOrderFilter
                    {
                        Search = query,
                        IncludeClosed = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(SalesOrderShipmentDto.ShipmentDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "sales-shipments",
                    "Albaranes",
                    shipments.TotalCount,
                    shipments.Items
                        .OrderByDescending(shipment => shipment.ShipmentDate)
                        .ThenByDescending(shipment => shipment.ShipmentNumber)
                        .Take(displayLimit)
                        .Select(shipment => new GlobalSearchItemDto
                        {
                            Kind = "sales-shipment",
                            Badge = "Albarán",
                            Title = $"Albarán {shipment.DisplayNumber}",
                            Subtitle = BuildDocumentSubtitle(shipment.ClientName, shipment.InvoiceStatus, shipment.Lines.Count),
                            Href = $"/ventas/albaranes/{shipment.ShipmentNumber}",
                            Date = shipment.ShipmentDate,
                            Amount = null,
                            Score = ScoreDocument(query, shipment.ShipmentNumber.ToString(), shipment.ClientName)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var drafts = await salesOrderQueries.SearchInvoiceDraftsAsync(
                    tenantId,
                    companyId,
                    new SalesPreInvoiceFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(SalesInvoiceDraftListItemDto.IssueDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "sales-drafts",
                    "Borradores",
                    drafts.TotalCount,
                    drafts.Items
                        .OrderByDescending(draft => draft.IssueDate)
                        .ThenByDescending(draft => draft.DraftNumber)
                        .Take(displayLimit)
                        .Select(draft => new GlobalSearchItemDto
                        {
                            Kind = "sales-draft",
                            Badge = "Borrador",
                            Title = $"Borrador {draft.DisplayNumber}",
                            Subtitle = BuildDocumentSubtitle(draft.ClientName, draft.Status, draft.ShipmentCount),
                            Href = $"/ventas/pre-facturacion/borradores/{draft.DraftNumber}",
                            Date = draft.IssueDate,
                            Amount = draft.TotalAmount,
                            Score = ScoreDocument(query, draft.DraftNumber.ToString(), draft.ClientName)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var invoices = await salesOrderQueries.SearchInvoicesAsync(
                    tenantId,
                    companyId,
                    new SalesPreInvoiceFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(SalesInvoiceListItemDto.IssueDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "sales-invoices",
                    "Facturas",
                    invoices.TotalCount,
                    invoices.Items
                        .OrderByDescending(invoice => invoice.IssueDate)
                        .ThenByDescending(invoice => invoice.InvoiceNumber)
                        .Take(displayLimit)
                        .Select(invoice => new GlobalSearchItemDto
                        {
                            Kind = "sales-invoice",
                            Badge = "Factura",
                            Title = $"Factura {invoice.DisplayNumber}",
                            Subtitle = BuildInvoiceSubtitle(invoice),
                            Href = $"/ventas/facturas/{invoice.InvoiceNumber}",
                            Date = invoice.IssueDate,
                            Amount = invoice.TotalAmount,
                            Score = ScoreDocument(query, invoice.InvoiceNumber.ToString(), invoice.ClientName)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var remittances = await salesRemittanceQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new SalesRemittanceFilter
                    {
                        Search = query,
                        IncludeClosed = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(SalesRemittanceListItemDto.RemittanceDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "sales-remittances",
                    "Remesas",
                    remittances.TotalCount,
                    remittances.Items
                        .OrderByDescending(remittance => remittance.RemittanceDate)
                        .ThenByDescending(remittance => remittance.RemittanceNumber)
                        .Take(displayLimit)
                        .Select(remittance => new GlobalSearchItemDto
                        {
                            Kind = "sales-remittance",
                            Badge = "Remesa",
                            Title = $"Remesa {remittance.DisplayNumber}",
                            Subtitle = BuildRemittanceSubtitle(remittance),
                            Href = $"/ventas/remesas/editar/{remittance.RemittanceNumber}",
                            Date = remittance.RemittanceDate,
                            Amount = remittance.TotalAmount,
                            Score = ScoreDocument(query, remittance.RemittanceNumber.ToString(), remittance.BankName)
                        }));
            });
        }

        async Task AddArticleGroupsAsync()
        {
            await TryAddAsync(async () =>
            {
                var fornituras = await fornituraQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new FornituraFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(FornituraListItemDto.Code)
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "fornituras",
                    "Fornituras",
                    fornituras.TotalCount,
                    fornituras.Items
                        .Take(displayLimit)
                        .Select(fornitura => new GlobalSearchItemDto
                        {
                            Kind = "fornitura",
                            Badge = "Fornitura",
                            Title = string.IsNullOrWhiteSpace(fornitura.Description) ? fornitura.Code : $"{fornitura.Code} · {fornitura.Description}",
                            Subtitle = BuildEntitySubtitle(fornitura.Code, $"Proveedor {fornitura.SupplierCode}", fornitura.Model, fornitura.Series, fornitura.Season),
                            Href = $"/articulos/fornituras/editar/{Encode(fornitura.Code)}",
                            Amount = fornitura.UnitPrice,
                            Score = ScoreText(query, fornitura.Code, fornitura.Description, fornitura.Model, fornitura.Series, fornitura.Season)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var yarns = await hiloQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new HiloFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(HiloListItemDto.Code)
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "yarns",
                    "Hilos",
                    yarns.TotalCount,
                    yarns.Items
                        .Take(displayLimit)
                        .Select(yarn => new GlobalSearchItemDto
                        {
                            Kind = "yarn",
                            Badge = "Hilo",
                            Title = string.IsNullOrWhiteSpace(yarn.Description) ? yarn.Code : $"{yarn.Code} · {yarn.Description}",
                            Subtitle = BuildEntitySubtitle(yarn.Code, $"Proveedor {yarn.SupplierCode}", yarn.VatCode, yarn.Notes),
                            Href = $"/articulos/hilos/editar/{Encode(yarn.Code)}",
                            Amount = yarn.UnitPrice,
                            Score = ScoreText(query, yarn.Code, yarn.Description, yarn.SupplierCode.ToString(), yarn.Notes)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var fabrics = await tejidoQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new TejidoFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(TejidoListItemDto.Code)
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "fabrics",
                    "Tejidos",
                    fabrics.TotalCount,
                    fabrics.Items
                        .Take(displayLimit)
                        .Select(fabric => new GlobalSearchItemDto
                        {
                            Kind = "fabric",
                            Badge = "Tejido",
                            Title = string.IsNullOrWhiteSpace(fabric.Description) ? fabric.Code : $"{fabric.Code} · {fabric.Description}",
                            Subtitle = BuildEntitySubtitle(fabric.Code, fabric.WidthText, $"Máquina {fabric.MachineCode}", $"Tejedor {fabric.WeaverCode}", $"{fabric.AvailableStockMeters:N2} m"),
                            Href = $"/articulos/tejidos/editar/{Encode(fabric.Code)}",
                            Amount = fabric.PricePerMeter,
                            Score = ScoreText(query, fabric.Code, fabric.Description, fabric.WidthText, fabric.MachineCode.ToString(), fabric.WeaverCode.ToString(), fabric.FinisherCode.ToString())
                        }));
            });

            await TryAddAsync(async () =>
            {
                var models = await modeloQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new ModeloFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(ModeloListItemDto.Code)
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "models",
                    "Models",
                    models.TotalCount,
                    models.Items
                        .Take(displayLimit)
                        .Select(model => new GlobalSearchItemDto
                        {
                            Kind = "model",
                            Badge = "Model",
                            Title = string.IsNullOrWhiteSpace(model.Description) ? model.Code : $"{model.Code} · {model.Description}",
                            Subtitle = BuildEntitySubtitle(model.Code, model.ClientName, model.Season, model.Series, model.FabricCode, $"{model.ColorsCount} colores"),
                            Href = $"/articulos/models/editar/{model.Id}",
                            Amount = model.FinalSalePrice,
                            Score = ScoreText(query, model.Code, model.Description, model.ClientName, model.FabricCode, model.Season, model.Series)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var samples = await muestraQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new MuestraFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(MuestraListItemDto.Code)
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "samples",
                    "Muestras",
                    samples.TotalCount,
                    samples.Items
                        .Take(displayLimit)
                        .Select(sample => new GlobalSearchItemDto
                        {
                            Kind = "sample",
                            Badge = "Muestra",
                            Title = string.IsNullOrWhiteSpace(sample.Description) ? sample.Code : $"{sample.Code} · {sample.Description}",
                            Subtitle = BuildEntitySubtitle(sample.Code, sample.ClientName, sample.Reference, sample.Season, sample.MachineName, $"{sample.DetailLinesCount} líneas"),
                            Href = $"/articulos/muestras/editar/{Encode(sample.Code)}",
                            Amount = sample.UnitPrice,
                            Score = ScoreText(query, sample.Code, sample.Description, sample.ClientName, sample.Reference, sample.Season, sample.MachineName)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var dispositions = await disposicionQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new DisposicionFilter
                    {
                        Search = query,
                        IncludeCancelled = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(DisposicionListItemDto.Date),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "dispositions",
                    "Disposiciones",
                    dispositions.TotalCount,
                    dispositions.Items
                        .OrderByDescending(disposition => disposition.Date)
                        .ThenByDescending(disposition => disposition.Code)
                        .Take(displayLimit)
                        .Select(disposition => new GlobalSearchItemDto
                        {
                            Kind = "disposition",
                            Badge = "Disposición",
                            Title = $"Disposición {disposition.Number}",
                            Subtitle = BuildEntitySubtitle(disposition.Code, disposition.ClientName, disposition.FinisherName, disposition.Color, $"{disposition.TotalPieces:N2} piezas", $"{disposition.TotalKilograms:N2} kg"),
                            Href = $"/articulos/disposiciones/editar/{disposition.Code}",
                            Date = disposition.Date,
                            Score = ScoreText(query, disposition.Code.ToString(), disposition.Number.ToString(), disposition.ClientName, disposition.FinisherName, disposition.Color, disposition.OrderReference)
                        }));
            });
        }

        async Task AddPurchaseGroupsAsync()
        {
            await TryAddAsync(async () =>
            {
                var purchaseOrders = await purchaseOrderQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new PurchaseOrderFilter
                    {
                        Search = query,
                        IncludeClosed = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(PurchaseOrderListItemDto.DocumentDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "purchase-orders",
                    "Pedidos de compra",
                    purchaseOrders.TotalCount,
                    purchaseOrders.Items
                        .OrderByDescending(order => order.DocumentDate)
                        .ThenByDescending(order => order.OrderNumber)
                        .Take(displayLimit)
                        .Select(order => new GlobalSearchItemDto
                        {
                            Kind = "purchase-order",
                            Badge = "Pedido compra",
                            Title = $"Pedido compra {order.OrderNumber}",
                            Subtitle = BuildDocumentSubtitle(order.SupplierName, order.Status, order.LineCount),
                            Href = $"/compras/pedidos/editar/{order.OrderNumber}",
                            Date = order.DocumentDate,
                            Amount = order.TotalAmount,
                            Score = ScoreDocument(query, order.OrderNumber.ToString(), order.SupplierName)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var purchaseReceipts = await purchaseOrderQueries.SearchReceiptsAsync(
                    tenantId,
                    companyId,
                    new PurchaseReceiptFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(PurchaseReceiptListItemDto.ReceiptDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "purchase-receipts",
                    "Recepciones de compra",
                    purchaseReceipts.TotalCount,
                    purchaseReceipts.Items
                        .OrderByDescending(receipt => receipt.ReceiptDate)
                        .ThenByDescending(receipt => receipt.ReceiptNumber)
                        .Take(displayLimit)
                        .Select(receipt => new GlobalSearchItemDto
                        {
                            Kind = "purchase-receipt",
                            Badge = "Recepción",
                            Title = $"Recepción {receipt.DisplayNumber}",
                            Subtitle = BuildEntitySubtitle(receipt.ReceiptNumber, receipt.SupplierName, receipt.Warehouse, receipt.Carrier, receipt.SupplierReference, $"{receipt.TotalReceivedQuantity:N2} uds."),
                            Href = $"/compras/recepciones/{receipt.ReceiptNumber}",
                            Date = receipt.ReceiptDate,
                            Score = ScoreDocument(query, receipt.ReceiptNumber.ToString(), receipt.SupplierName)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var purchaseInvoices = await purchaseInvoiceQueries.SearchInvoicesAsync(
                    tenantId,
                    companyId,
                    new PurchaseInvoiceFilter
                    {
                        Search = query,
                        IncludeClosed = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(PurchaseInvoiceListItemDto.DocumentDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "purchase-invoices",
                    "Facturas proveedor",
                    purchaseInvoices.TotalCount,
                    purchaseInvoices.Items
                        .OrderByDescending(invoice => invoice.DocumentDate)
                        .ThenByDescending(invoice => invoice.InvoiceNumber)
                        .Take(displayLimit)
                        .Select(invoice => new GlobalSearchItemDto
                        {
                            Kind = "purchase-invoice",
                            Badge = "Factura proveedor",
                            Title = $"Factura proveedor {invoice.DisplayNumber}",
                            Subtitle = BuildEntitySubtitle(invoice.InvoiceNumber, invoice.SupplierName, invoice.SupplierDocumentNumber, invoice.Status, $"{invoice.OutstandingAmount:N2} pendiente"),
                            Href = $"/compras/facturas/editar/{invoice.InvoiceNumber}",
                            Date = invoice.DocumentDate,
                            Amount = invoice.TotalAmount,
                            Score = ScoreText(query, invoice.InvoiceNumber.ToString(), invoice.DisplayNumber, invoice.SupplierName, invoice.SupplierDocumentNumber)
                        }));
            });
        }

        async Task AddProductionGroupsAsync()
        {
            await TryAddAsync(async () =>
            {
                var finishingOrders = await parteAcabadoQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new ParteAcabadoFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(ParteAcabadoListItemDto.Date),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "finishing-orders",
                    "Partes de acabado",
                    finishingOrders.TotalCount,
                    finishingOrders.Items
                        .OrderByDescending(order => order.Date)
                        .ThenByDescending(order => order.OrderNumber)
                        .Take(displayLimit)
                        .Select(order => new GlobalSearchItemDto
                        {
                            Kind = "finishing-order",
                            Badge = "Parte acabado",
                            Title = $"Parte {order.OrderNumber}",
                            Subtitle = BuildEntitySubtitle(order.OrderNumber, order.ClientName, order.FinisherName, order.MachineName, order.OperationName, order.PrimaryFabricCode, order.PrimaryColor),
                            Href = $"/produccion/acabados/editar/{order.OrderNumber}",
                            Date = order.Date,
                            Score = ScoreText(query, order.OrderNumber.ToString(), order.ClientName, order.FinisherName, order.MachineName, order.OperationName, order.PrimaryFabricCode, order.PrimaryColor)
                        }));
            });
        }

        async Task AddStockGroupsAsync()
        {
            await TryAddAsync(async () =>
            {
                var balances = await stockQueries.SearchBalancesAsync(
                    tenantId,
                    companyId,
                    new StockBalanceFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(StockBalanceListItemDto.LastMovementDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "stock-balances",
                    "Stock actual",
                    balances.TotalCount,
                    balances.Items
                        .OrderByDescending(balance => balance.LastMovementDate)
                        .ThenBy(balance => balance.ItemCode)
                        .Take(displayLimit)
                        .Select(balance => new GlobalSearchItemDto
                        {
                            Kind = "stock-balance",
                            Badge = "Stock",
                            Title = string.IsNullOrWhiteSpace(balance.ItemDescription) ? balance.ItemCode : $"{balance.ItemCode} · {balance.ItemDescription}",
                            Subtitle = BuildEntitySubtitle(balance.ItemCode, balance.Warehouse, $"{balance.CurrentStock:N2} {balance.UnitOfMeasure}", $"{balance.MovementCount:N0} movimientos"),
                            Href = $"/almacen/stock-actual?search={Encode(balance.ItemCode)}",
                            Date = balance.LastMovementDate,
                            Score = ScoreText(query, balance.ItemCode, balance.ItemDescription, balance.Warehouse)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var movements = await stockQueries.SearchMovementsAsync(
                    tenantId,
                    companyId,
                    new StockMovementFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(StockMovementListItemDto.MovementDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "stock-movements",
                    "Movimientos de stock",
                    movements.TotalCount,
                    movements.Items
                        .OrderByDescending(movement => movement.MovementDate)
                        .ThenBy(movement => movement.ItemCode)
                        .Take(displayLimit)
                        .Select(movement => new GlobalSearchItemDto
                        {
                            Kind = "stock-movement",
                            Badge = "Movimiento",
                            Title = string.IsNullOrWhiteSpace(movement.SourceDocumentDisplay)
                                ? $"{movement.MovementType} · {movement.ItemCode}"
                                : $"{movement.SourceDocumentDisplay} · {movement.ItemCode}",
                            Subtitle = BuildEntitySubtitle(movement.ItemCode, movement.ItemDescription, movement.Warehouse, movement.Color, movement.SupplierName, $"{movement.Quantity:N2} {movement.UnitOfMeasure}"),
                            Href = $"/almacen/movimientos?search={Encode(movement.ItemCode)}",
                            Date = movement.MovementDate,
                            Score = ScoreText(query, movement.ItemCode, movement.ItemDescription, movement.Warehouse, movement.Color, movement.SupplierName, movement.SourceDocumentDisplay, movement.Notes)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var transfers = await stockQueries.SearchTransfersAsync(
                    tenantId,
                    companyId,
                    new StockTransferFilter
                    {
                        Search = query,
                        IncludeClosed = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(StockTransferListItemDto.TransferDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "stock-transfers",
                    "Traspasos",
                    transfers.TotalCount,
                    transfers.Items
                        .OrderByDescending(transfer => transfer.TransferDate)
                        .ThenByDescending(transfer => transfer.TransferNumber)
                        .Take(displayLimit)
                        .Select(transfer => new GlobalSearchItemDto
                        {
                            Kind = "stock-transfer",
                            Badge = "Traspaso",
                            Title = $"Traspaso {transfer.TransferNumber}",
                            Subtitle = BuildEntitySubtitle(transfer.TransferNumber, transfer.Status, transfer.FromWarehouse, transfer.ToWarehouse, $"{transfer.LineCount} líneas", $"{transfer.TotalQuantity:N2} uds."),
                            Href = $"/almacen/traspasos/editar/{transfer.TransferNumber}",
                            Date = transfer.TransferDate,
                            Score = ScoreText(query, transfer.TransferNumber.ToString(), transfer.Status, transfer.FromWarehouse, transfer.ToWarehouse, transfer.Notes)
                        }));
            });

            await TryAddAsync(async () =>
            {
                var counts = await stockQueries.SearchCountsAsync(
                    tenantId,
                    companyId,
                    new StockCountFilter
                    {
                        Search = query,
                        IncludeClosed = true,
                        Page = 1,
                        PageSize = queryLimit,
                        SortColumn = nameof(StockCountListItemDto.CountDate),
                        SortDescending = true
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "stock-counts",
                    "Inventarios",
                    counts.TotalCount,
                    counts.Items
                        .OrderByDescending(count => count.CountDate)
                        .ThenByDescending(count => count.CountNumber)
                        .Take(displayLimit)
                        .Select(count => new GlobalSearchItemDto
                        {
                            Kind = "stock-count",
                            Badge = "Inventario",
                            Title = $"Inventario {count.CountNumber}",
                            Subtitle = BuildEntitySubtitle(count.CountNumber, count.Status, count.Warehouse, count.IsBlindCount ? "conteo ciego" : string.Empty, $"{count.LineCount} líneas", $"{count.DifferenceLineCount} diferencias"),
                            Href = $"/almacen/inventarios/editar/{count.CountNumber}",
                            Date = count.CountDate,
                            Score = ScoreText(query, count.CountNumber.ToString(), count.Status, count.Warehouse, count.Notes)
                        }));
            });
        }

        async Task AddBaseGroupsAsync()
        {
            await TryAddAsync(async () =>
            {
                var totalCount = 0;
                var items = new List<GlobalSearchItemDto>();

                foreach (var catalogKey in BaseCatalogKeys.All)
                {
                    var catalog = await baseCatalogQueries.SearchAsync(
                        tenantId,
                        companyId,
                        catalogKey,
                        new BaseCatalogFilter
                        {
                            Search = query,
                            IncludeInactive = true,
                            Page = 1,
                            PageSize = queryLimit
                        },
                        cancellationToken);

                    totalCount += catalog.TotalCount;
                    items.AddRange(catalog.Items
                        .Take(displayLimit)
                        .Select(item => new GlobalSearchItemDto
                        {
                            Kind = "base-catalog",
                            Badge = BaseCatalogTitle(item.CatalogKey),
                            Title = string.IsNullOrWhiteSpace(item.Name) ? item.Code : $"{item.Code} · {item.Name}",
                            Subtitle = BuildEntitySubtitle(item.Code, BaseCatalogTitle(item.CatalogKey), item.Description, item.Reference, item.SecondaryReference, item.IsActive ? "activo" : "inactivo"),
                            Href = $"/base-datos/{item.CatalogKey}/editar/{Encode(item.Code)}",
                            Score = ScoreText(query, item.Code, item.Name, item.Description, item.Reference, item.SecondaryReference, BaseCatalogTitle(item.CatalogKey))
                        }));
                }

                AddGroup(
                    groups,
                    "base-catalogs",
                    "Base",
                    totalCount,
                    items
                        .OrderByDescending(item => item.Score)
                        .ThenBy(item => item.Title)
                        .Take(displayLimit));
            });
        }

        async Task AddMailingGroupsAsync()
        {
            await TryAddAsync(async () =>
            {
                var campaigns = await mailingCampaignQueries.SearchAsync(
                    tenantId,
                    companyId,
                    new MailingCampaignFilter
                    {
                        Search = query,
                        Page = 1,
                        PageSize = queryLimit
                    },
                    cancellationToken);

                AddGroup(
                    groups,
                    "mailing-campaigns",
                    "Mailing",
                    campaigns.TotalCount,
                    campaigns.Items
                        .OrderByDescending(campaign => campaign.UpdatedUtc)
                        .Take(displayLimit)
                        .Select(campaign => new GlobalSearchItemDto
                        {
                            Kind = "mailing-campaign",
                            Badge = "Mailing",
                            Title = campaign.Title,
                            Subtitle = BuildEntitySubtitle(campaign.CampaignId.ToString(), campaign.SourceType, $"{campaign.RecipientCount:N0} destinatarios", campaign.IncludeAllRecipients ? "todos los destinatarios" : string.Empty),
                            Href = $"/base-datos/mailing/editar/{campaign.CampaignId}",
                            Date = campaign.UpdatedUtc,
                            Score = ScoreText(query, campaign.CampaignId.ToString(), campaign.Title, campaign.SourceType)
                        }));
            });
        }
    }

    private static void AddGroup(
        ICollection<GlobalSearchGroupDto> groups,
        string key,
        string title,
        int totalCount,
        IEnumerable<GlobalSearchItemDto> items)
    {
        var materialized = items
            .OrderByDescending(item => item.Score)
            .ThenByDescending(item => item.Date)
            .ThenBy(item => item.Title)
            .ToArray();

        if (materialized.Length == 0 && totalCount == 0)
        {
            return;
        }

        groups.Add(new GlobalSearchGroupDto
        {
            Key = key,
            Title = title,
            TotalCount = totalCount,
            Items = materialized
        });
    }

    private static string BuildClientSubtitle(ClienteListItemDto client)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(client.TaxId))
        {
            parts.Add($"NIF {client.TaxId}");
        }

        if (!string.IsNullOrWhiteSpace(client.City))
        {
            parts.Add(client.City);
        }

        if (!string.IsNullOrWhiteSpace(client.Email))
        {
            parts.Add(client.Email);
        }

        if (client.IsBlocked)
        {
            parts.Add("bloqueado");
        }

        return parts.Count == 0 ? $"Código {client.Code}" : string.Join(" · ", parts);
    }

    private static string BuildEntitySubtitle(object code, params string[] parts)
    {
        var materialized = parts
            .Where(part => !string.IsNullOrWhiteSpace(part))
            .Select(part => part.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return materialized.Length == 0 ? $"Código {code}" : string.Join(" · ", materialized);
    }

    private static string BuildDocumentSubtitle(string clientName, string status, int relatedCount)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(clientName))
        {
            parts.Add(clientName);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            parts.Add(status);
        }

        if (relatedCount > 0)
        {
            parts.Add($"{relatedCount:N0} líneas/docs.");
        }

        return string.Join(" · ", parts);
    }

    private static string BuildInvoiceSubtitle(SalesInvoiceListItemDto invoice)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(invoice.ClientName))
        {
            parts.Add(invoice.ClientName);
        }

        parts.Add(invoice.PaymentStatus);
        if (invoice.OutstandingAmount != 0)
        {
            parts.Add($"pendiente {invoice.OutstandingAmount:N2}");
        }

        return string.Join(" · ", parts);
    }

    private static string BuildRemittanceSubtitle(SalesRemittanceListItemDto remittance)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(remittance.BankName))
        {
            parts.Add(remittance.BankName);
        }

        parts.Add(remittance.Status);
        if (remittance.InvoiceCount > 0)
        {
            parts.Add($"{remittance.InvoiceCount:N0} facturas");
        }

        return string.Join(" · ", parts);
    }

    private static string BaseCatalogTitle(string catalogKey) =>
        catalogKey switch
        {
            BaseCatalogKeys.BanksCashboxes => "Bancos / cajas",
            BaseCatalogKeys.PaymentMethods => "Formas de pago",
            BaseCatalogKeys.Operations => "Operaciones",
            BaseCatalogKeys.Machines => "Máquinas",
            BaseCatalogKeys.Seasons => "Temporadas",
            BaseCatalogKeys.VatTypes => "Tipos IVA",
            BaseCatalogKeys.Incoterms => "Incoterms",
            _ => "Base"
        };

    private static int ScoreClient(string query, ClienteListItemDto client)
    {
        var normalizedQuery = query.Trim();
        if (string.Equals(client.Code.ToString(), normalizedQuery, StringComparison.OrdinalIgnoreCase))
        {
            return 120;
        }

        if (client.Name.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase))
        {
            return 90;
        }

        if (client.TaxId.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase))
        {
            return 80;
        }

        return 50;
    }

    private static int ScoreDocument(string query, string number, string searchableText) =>
        ScoreText(query, number, searchableText);

    private static int ScoreText(string query, string primary, params string[] searchableTexts)
    {
        var normalizedQuery = query.Trim();
        if (string.Equals(primary, normalizedQuery, StringComparison.OrdinalIgnoreCase))
        {
            return 120;
        }

        if (primary.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase))
        {
            return 100;
        }

        foreach (var text in searchableTexts)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                continue;
            }

            if (string.Equals(text, normalizedQuery, StringComparison.OrdinalIgnoreCase))
            {
                return 95;
            }

            if (text.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase))
            {
                return 80;
            }
        }

        return 45;
    }

    private static string Encode(string value) =>
        Uri.EscapeDataString(value);
}
