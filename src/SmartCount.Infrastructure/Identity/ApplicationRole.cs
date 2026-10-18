using Microsoft.AspNetCore.Identity;

namespace SmartCount.Infrastructure.Identity;

public sealed class ApplicationRole : IdentityRole<int>
{
    public string? Description { get; set; }

    public bool IsSystem { get; set; }

    public ApplicationRole()
    {
    }
}