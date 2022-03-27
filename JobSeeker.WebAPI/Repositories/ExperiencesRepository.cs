using JobSeeker.WebAPI.Data;
using JobSeeker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public class ExperiencesRepository : IExperiencesRepository
    {
        private JobSeekerContext db;
        private DbSet<UserExperiences> userModel;
        public ExperiencesRepository(JobSeekerContext dbcontext)
        {
            db = dbcontext;
            userModel = db.Set<UserExperiences>();
        }
        //add new experience
        public async Task<UserExperiences> AddUser(UserExperiences user )
        {
            await userModel.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }
        //update experience
        public async Task UpdateUser(int id, UserExperiences user)
        {
            var seeker = await userModel.FindAsync(id);
            if (seeker != null)
            {
                seeker.UserId = user.UserId;
                seeker.CompanyName = user.CompanyName;
                seeker.StartYear = user.StartYear;
                seeker.EndYear = user.EndYear;
                seeker.ComapanyUrl = user.ComapanyUrl;
                seeker.Designation=user.Designation;
                seeker.JobDescription = user.JobDescription;

                await db.SaveChangesAsync();
            }
        }
        //delete experience
        public async Task DeleteUser(int id)
        {
            var item = await userModel.FindAsync(id);
            userModel.Remove(item);
            await db.SaveChangesAsync();
        }
        //get single experience
        public async Task<UserExperiences> GetUser(int id)
        {
            return await userModel.FindAsync(id);
        }
        //get all experiences of jobseeker
        public List<UserExperiences> GetAll(int userid)
        {
            string Storpro = "EXEC SelectAllexperienceOfUser @userId=" + userid;

            return db.UserExperiences.FromSqlRaw(Storpro).ToList();
        }


    }
}
