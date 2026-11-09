
namespace SmartCount.Domain.Entities.System;

public class Feature
{
    public int Id { get; private set; }

    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    private Feature()
    {
    }

    private Feature(string code,string name,string? description)
    {
        Code = code.Trim().ToUpperInvariant();
        Name = name.Trim();
        Description = description?.Trim();
        IsActive = true;
    }

    public static Feature Create(string code,string name,string? description = null)
    {
        Validate(code, name, description);

        return new Feature(code, name, description);
    }

    public void UpdateDetails(string name,string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Feature name is required.", nameof(name));

        if (name.Trim().Length > 120)
            throw new ArgumentException(
                "Feature name cannot exceed 120 characters.",
                nameof(name));

        if (description?.Length > 500)
            throw new ArgumentException(
                "Description cannot exceed 500 characters.",
                nameof(description));

        Name = name.Trim();
        Description = description?.Trim();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private static void Validate(string code,string name,string? description)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Feature code is required.", nameof(code));

        if (code.Trim().Length > 50)
            throw new ArgumentException(
                "Feature code cannot exceed 50 characters.",
                nameof(code));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Feature name is required.", nameof(name));

        if (name.Trim().Length > 120)
            throw new ArgumentException(
                "Feature name cannot exceed 120 characters.",
                nameof(name));

        if (description?.Length > 500)
            throw new ArgumentException(
                "Description cannot exceed 500 characters.",
                nameof(description));
    }
}
