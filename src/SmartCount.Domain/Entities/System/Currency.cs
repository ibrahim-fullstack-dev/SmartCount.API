namespace SmartCount.Domain.Entities.System;

public class Currency
{
    public int Id { get; private set; }

    public string Code { get; private set; } = null!;

    public string NameAr { get; private set; } = null!;

    public string NameEn { get; private set; } = null!;

    public string? Symbol { get; private set; }

    public byte DecimalPlaces { get; private set; }

    public bool IsActive { get; private set; }

    private Currency()
    {
    }

    private Currency(string code,string nameAr,string nameEn,string? symbol,byte decimalPlaces)
    {
        Code = code.Trim().ToUpperInvariant();
        NameAr = nameAr.Trim();
        NameEn = nameEn.Trim();
        Symbol = string.IsNullOrWhiteSpace(symbol)
            ? null
            : symbol.Trim();
        DecimalPlaces = decimalPlaces;
        IsActive = true;
    }

    public static Currency Create(string code,string nameAr,string nameEn,string? symbol = null,byte decimalPlaces = 2)
    {
        Validate(code, nameAr, nameEn, decimalPlaces);

        return new Currency(code,nameAr,nameEn,symbol,decimalPlaces);
    }

    public void UpdateDetails(string nameAr,string nameEn,string? symbol)
    {
        if (string.IsNullOrWhiteSpace(nameAr))
            throw new ArgumentException(
                "Arabic name is required.", nameof(nameAr));

        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException(
                "English name is required.", nameof(nameEn));

        NameAr = nameAr.Trim();
        NameEn = nameEn.Trim();
        Symbol = string.IsNullOrWhiteSpace(symbol)
            ? null
            : symbol.Trim();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void Validate(string code,string nameAr,string nameEn,byte decimalPlaces)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Currency code is required.", nameof(code));

        if (code.Trim().Length != 3)
            throw new ArgumentException(
                "Currency code must contain exactly 3 characters.",
                nameof(code));

        if (string.IsNullOrWhiteSpace(nameAr))
            throw new ArgumentException(
                "Arabic name is required.", nameof(nameAr));

        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException(
                "English name is required.", nameof(nameEn));

        if (decimalPlaces > 4)
            throw new ArgumentOutOfRangeException(
                nameof(decimalPlaces),
                "Decimal places cannot exceed 4.");
    }
}
