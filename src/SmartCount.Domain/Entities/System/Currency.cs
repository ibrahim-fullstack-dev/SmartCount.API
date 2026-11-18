
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

    private Currency() { }

    private Currency(string code,string nameAr,string nameEn,string? symbol,byte decimalPlaces)
    {
        Code = code;
        NameAr = nameAr;
        NameEn = nameEn;
        Symbol = symbol;
        DecimalPlaces = decimalPlaces;
        IsActive = true;
    }

    public static Currency Create(string code,string nameAr,string nameEn,string? symbol = null,byte decimalPlaces = 2)
    {
        code = ValidateRequiredText(code, nameof(Code), 3).ToUpperInvariant();

        code = ValidateCode(code);
        nameAr = ValidateRequiredText(nameAr, nameof(NameAr), 120);
        nameEn = ValidateRequiredText(nameEn, nameof(NameEn), 120);
        symbol = ValidateSymbol(symbol);

        if (decimalPlaces > 4)
            throw new ArgumentOutOfRangeException(nameof(decimalPlaces));

        return new Currency(code, nameAr, nameEn, symbol, decimalPlaces);
    }

    public void UpdateDetails(string code, string nameAr, string nameEn, string? symbol)
    {
        code = ValidateRequiredText(code, nameof(Code), 3)
        .ToUpperInvariant();


        code = ValidateCode(code);
        nameAr = ValidateRequiredText(nameAr, nameof(NameAr), 120);
        nameEn = ValidateRequiredText(nameEn, nameof(NameEn), 120);
        symbol = ValidateSymbol(symbol);

        Code = code;
        NameAr = nameAr;
        NameEn = nameEn;
        Symbol = symbol;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    private static string ValidateRequiredText(string? value,string propertyName,int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{propertyName} is required.", propertyName);

        value = value.Trim();

        if (value.Length > maxLength)
            throw new ArgumentException(
                $"{propertyName} cannot exceed {maxLength} characters.",
                propertyName);

        return value;
    }

    private static string ValidateCode(string code)
    {
        if (code.Any(c => c < 'A' || c > 'Z'))
            throw new ArgumentException(
                "Code must contain English letters only.",
                nameof(code));

        return code;
    }

    private static string? ValidateSymbol(string? symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            return null;

        symbol = symbol.Trim();

        if (symbol.Length > 10)
            throw new ArgumentException("Symbol cannot exceed 10 characters.", nameof(symbol));

        return symbol;
    }
}
