namespace Erp.Application.Suppliers;

public sealed class ProveedorDetailDto
{
    public int? Code { get; set; }
    public string CompanyCenterCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string SecondaryPhone { get; set; } = string.Empty;
    public string Fax { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public string PrimaryEmail { get; set; } = string.Empty;
    public string SecondaryEmail { get; set; } = string.Empty;
    public string PaymentMethodCode { get; set; } = string.Empty;
    public string PaymentMethodName { get; set; } = string.Empty;
    public int? PaymentMethodNumberOfPayments { get; set; }
    public int? PaymentMethodDays { get; set; }
    public int? PaymentMethodFirstPaymentDays { get; set; }
    public string BankCode { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string BankEntityCode { get; set; } = string.Empty;
    public string BankOfficeCode { get; set; } = string.Empty;
    public string BankControlDigit { get; set; } = string.Empty;
    public string BankAccountNumber { get; set; } = string.Empty;
    public int? PaymentDay1 { get; set; }
    public int? PaymentDay2 { get; set; }
    public int? PaymentDay3 { get; set; }
    public string TaxCode { get; set; } = string.Empty;
    public string TaxName { get; set; } = string.Empty;
    public decimal? TaxPercent { get; set; }
    public decimal? SurchargePercent { get; set; }
    public string SubAccount { get; set; } = string.Empty;
    public bool TransferToAccounting { get; set; }
    public string Iban { get; set; } = string.Empty;
    public string Swift { get; set; } = string.Empty;
    public string IncotermCode { get; set; } = string.Empty;
    public string IncotermName { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
