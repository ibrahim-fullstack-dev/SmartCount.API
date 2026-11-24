
namespace SmartCount.Domain.Common;

public static class Guard
{
    public static void AgainstInvalidId(int id, string parameterName)
    {
        if (id <= 0)
            throw new ArgumentException(
                $"Invalid {parameterName}.");
    }

    public static void AgainstNullOrWhiteSpace(string? value,string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                $"{parameterName} is required.");
    }

    public static void AgainstMaxLength(string value,int maxLength,string parameterName)
    {
        if (value.Trim().Length > maxLength)
            throw new ArgumentException(
                $"{parameterName} cannot exceed {maxLength} characters.");
    }
}
