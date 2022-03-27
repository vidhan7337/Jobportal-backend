using Identity.WebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.WebAPI.Data
{
    
    
        public class IdentityContext : DbContext
    {
            public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
            public DbSet<UserIdentity> UserIdentity { get; set; }
        //making unique fields
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserIdentity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<UserIdentity>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
       
    }
    

}
