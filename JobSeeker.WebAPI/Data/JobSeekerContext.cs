using JobSeeker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace JobSeeker.WebAPI.Data
{
    public class JobSeekerContext:DbContext
    {
        public JobSeekerContext(DbContextOptions<JobSeekerContext> options) : base(options) { }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<Qualifications> Qualifications { get; set; }
        public DbSet<UserExperiences> UserExperiences { get; set; }
        public DbSet<UserVacancyRequests> UserVacancyRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserModel>()
                .HasIndex(u => u.Email)
                .IsUnique();

            
        }

        internal object FromSqlRaw(string storpro)
        {
            throw new NotImplementedException();
        }
    }
}
