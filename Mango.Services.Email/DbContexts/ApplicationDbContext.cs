using Mango.Services.Email.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mango.Services.Email.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // tech debt
            optionsBuilder.UseSqlServer("Server=DESKTOP-5JKESUF\\SQLEXPRESS;Database=MangoEmailAPI;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}