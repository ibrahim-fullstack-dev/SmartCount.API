
namespace SmartCount.Domain.Entities.Tenancy;

public class Plan
{
    public int Id { get; private set; }

    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public decimal MonthlyPrice { get; private set; }
    public decimal YearlyPrice { get; private set; }

    public int CurrencyId { get; private set; }

    public bool IsActive { get; private set; }

    private Plan() { }

    private Plan(string code,string name,string? description,decimal monthlyPrice,decimal yearlyPrice,int currencyId)
    {
        Code = code;
        Name = name;
        Description = description;
        MonthlyPrice = monthlyPrice;
        YearlyPrice = yearlyPrice;
        CurrencyId = currencyId;
        IsActive = true;
    }

    public static Plan Create(string code,string name,decimal monthlyPrice,decimal yearlyPrice,int currencyId,string? description = null)
    {
        code = ValidateCode(code);
        name = ValidateName(name);
        description = NormalizeDescription(description);

        ValidatePrice(monthlyPrice, nameof(monthlyPrice));
        ValidatePrice(yearlyPrice, nameof(yearlyPrice));

        if (currencyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(currencyId));

        return new Plan(code,name,description,monthlyPrice,yearlyPrice,currencyId);
    }

    public void UpdateDetails(string name, string? description)
    {
        Name = ValidateName(name);
        Description = NormalizeDescription(description);
    }

    public void UpdatePricing(decimal monthlyPrice, decimal yearlyPrice)
    {
        ValidatePrice(monthlyPrice, nameof(monthlyPrice));
        ValidatePrice(yearlyPrice, nameof(yearlyPrice));

        MonthlyPrice = monthlyPrice;
        YearlyPrice = yearlyPrice;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    private static string ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Plan code is required.", nameof(code));

        code = code.Trim().ToUpperInvariant();

        if (code.Length > 36)
            throw new ArgumentException(
                "Plan code cannot exceed 36 characters.", nameof(code));

        if (code.Any(c => !(
            (c >= 'A' && c <= 'Z') ||
            (c >= '0' && c <= '9') ||
            c == '_'))
        )
            throw new ArgumentException(
                "Plan code contains invalid characters.", nameof(code));

        return code;
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Plan name is required.", nameof(name));

        name = name.Trim();

        if (name.Length > 120)
            throw new ArgumentException(
                "Plan name cannot exceed 120 characters.", nameof(name));

        return name;
    }

    private static string? NormalizeDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return null;

        description = description.Trim();

        if (description.Length > 500)
            throw new ArgumentException(
                "Description cannot exceed 500 characters.", nameof(description));

        return description;
    }

    private static void ValidatePrice(decimal price, string propertyName)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(
                propertyName, "Price cannot be negative.");

        if (decimal.Round(price, 2) != price)
            throw new ArgumentException(
                "Price cannot have more than 2 decimal places.", propertyName);
    }
}
