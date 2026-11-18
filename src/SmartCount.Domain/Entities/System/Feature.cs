
namespace SmartCount.Domain.Entities.System;

public class Feature
{
    public int Id { get; private set; }

    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    private Feature() { }

    private Feature(string code, string name, string? description)
    {
        Code = code;
        Name = name;
        Description = description;
        IsActive = true;
    }

    public static Feature Create(string code,string name,string? description = null)
    {
        code = ValidateCode(code);
        name = ValidateName(name);
        description = NormalizeDescription(description);

        return new Feature(code, name, description);
    }

    public void UpdateDetails(string name, string? description)
    {
        name = ValidateName(name);
        description = NormalizeDescription(description);

        Name = name;
        Description = description;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    private static string ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Feature code is required.", nameof(code));

        code = code.Trim().ToUpperInvariant();

        if (code.Length > 50)
            throw new ArgumentException("Feature code cannot exceed 50 characters.", nameof(code));

        if (code.Any(c => !((c >= 'A' && c <= 'Z') ||
                            (c >= '0' && c <= '9') ||
                            c == '_'))
            )
            throw new ArgumentException("Feature code contains invalid characters.", nameof(code));

        return code;
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Feature name is required.", nameof(name));

        name = name.Trim();

        if (name.Length > 120)
            throw new ArgumentException("Feature name cannot exceed 120 characters.", nameof(name));

        return name;
    }

    private static string? NormalizeDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return null;

        description = description.Trim();

        if (description.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters.", nameof(description));

        return description;
    }
}
