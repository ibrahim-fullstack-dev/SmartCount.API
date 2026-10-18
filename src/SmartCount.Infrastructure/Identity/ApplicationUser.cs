using Microsoft.AspNetCore.Identity;

namespace SmartCount.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<int>
{
    public int CompanyId { get; private set; }

    public bool IsActive { get; private set; } = true;

    public bool IsSystem { get; private set; } = true;


    private ApplicationUser()
    {
    }
}