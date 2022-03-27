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
        //add new vacancy request
        public async Task<UserVacancyRequests> AddRequest(UserVacancyRequests user)
        {
            await vacancyModel.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }
        //all jobseeker who applied for particular vacancy
        public List<UserModel> GetJobseeker(int id)
        {
            string Storpro = "EXEC SelectJobseekerapplied @vacId=" + id;

            return db.UserModel.FromSqlRaw(Storpro).ToList();
        }
        //checking jobseeker applied for vacancy or not
        public bool Requestaleradyexsits(int vacid, int userid)
        {
            var item= db.UserVacancyRequests.Where(e=>e.UserId == userid&&e.VacancyId==vacid).FirstOrDefault();
            if (item == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //all vacancies id applied by jobseeker
        public List<int> GetVacancyIdApplied(int userid)
        {
            var item = db.UserVacancyRequests.Where(e=>e.UserId==userid).Select(e=>e.VacancyId).ToList();
            return item;
        }

       

    }
}
