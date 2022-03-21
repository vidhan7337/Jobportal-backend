using JobSeeker.WebAPI.Data;
using JobSeeker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public class VacancyRequestRepository:IVacancyRequestRepository
    {
        private JobSeekerContext db;
        private DbSet<UserVacancyRequests> vacancyModel;
        private DbSet<UserModel> userModel;
        public VacancyRequestRepository(JobSeekerContext dbcontext)
        {
            db = dbcontext;
            vacancyModel = db.Set<UserVacancyRequests>();
            userModel = db.Set<UserModel>();
        }

        public async Task<UserVacancyRequests> AddRequest(UserVacancyRequests user)
        {
            await vacancyModel.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public List<UserModel> GetJobseeker(int id)
        {
            string Storpro = "EXEC SelectJobseekerapplied @vacId=" + id;

            return db.UserModel.FromSqlRaw(Storpro).ToList();
        }


    }
}
