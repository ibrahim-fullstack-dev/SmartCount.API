using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SmartCount.Infrastructure.Identity;

// Domain
using SmartCount.Domain.Entities.Accounting;

namespace SmartCount.Infrastructure.Persistence;

public sealed class SmartCountContext:IdentityDbContext<ApplicationUser,ApplicationRole,int>
{
    public SmartCountContext(DbContextOptions<SmartCountContext> options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<AccountType> AccountTypes { get; set; }
}
