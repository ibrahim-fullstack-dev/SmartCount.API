
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

    private Plan()
    {
    }

    private Plan(string code,string name,string? description,decimal monthlyPrice,decimal yearlyPrice,int currencyId)
    {
        Code = code.Trim().ToUpperInvariant();
        Name = name.Trim();
        Description = description?.Trim();
        MonthlyPrice = monthlyPrice;
        YearlyPrice = yearlyPrice;
        CurrencyId = currencyId;
        IsActive = true;
    }

    public static Plan Create(string code,string name,string? description,decimal monthlyPrice,decimal yearlyPrice,int currencyId)
    {
        Validate(code,name,monthlyPrice,yearlyPrice,currencyId);

        return new Plan(code,name,description,monthlyPrice,yearlyPrice,currencyId);
    }

    public void UpdateDetails(string name,string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Plan name is required.", nameof(name));
        }

        if (name.Trim().Length > 120)
        {
            throw new ArgumentException(
                "Plan name cannot exceed 120 characters.",
                nameof(name));
        }

        if (description?.Length > 500)
        {
            throw new ArgumentException(
                "Description cannot exceed 500 characters.",
                nameof(description));
        }

        Name = name.Trim();
        Description = description?.Trim();
    }

    public void UpdatePricing(decimal monthlyPrice,decimal yearlyPrice)
    {
        if (monthlyPrice < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(monthlyPrice));
        }

        if (yearlyPrice < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(yearlyPrice));
        }

        MonthlyPrice = monthlyPrice;
        YearlyPrice = yearlyPrice;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void Validate(string code,string name,decimal monthlyPrice,decimal yearlyPrice,int currencyId)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Plan code is required.", nameof(code));

        if (code.Trim().Length > 36)
            throw new ArgumentException(
                "Plan code cannot exceed 36 characters.",
                nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Plan name is required.", nameof(name));

        if (name.Trim().Length > 120)
            throw new ArgumentException(
                "Plan name cannot exceed 120 characters.",
                nameof(name));

        if (monthlyPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(monthlyPrice));

        if (yearlyPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(yearlyPrice));

        if (currencyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(currencyId));
    }
}
