using EFCorePractice.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
    }
}