using Employer.WEBAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Employer.WEBAPI.Data
{
    public class EmployerContext : DbContext
    {

        public EmployerContext(DbContextOptions<EmployerContext> options) : base(options) { }
        public DbSet<EmployerDetails> Employeers { get; set; }
        public DbSet<VacancyDetails> Vacancies { get; set; }
       //making column unique
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EmployerDetails>()
                .HasIndex(u => u.CompanyEmail)
                .IsUnique();

            builder.Entity<EmployerDetails>()
                .HasIndex(u => u.Organization)
                .IsUnique();
            builder.Entity<EmployerDetails>()
                .HasIndex(u => u.CreatedBy)
                .IsUnique();

        }
    }
}
