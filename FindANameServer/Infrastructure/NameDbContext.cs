using FindANameServer.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FindANameServer.Infrastructure;

public class NameDbContext(DbContextOptions<NameDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<CandidateName> CandidateNames { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CandidateName>().HasMany(e => e.RejectedBy).WithMany();
    }
}
