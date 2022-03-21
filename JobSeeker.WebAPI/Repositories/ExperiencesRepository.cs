using JobSeeker.WebAPI.Data;
using JobSeeker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<UserExperiences> AddUser(UserExperiences user )
        {
            await userModel.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }
        public async Task UpdateUser(int id, UserExperiences user,int userid)
        {
            var seeker = await userModel.FindAsync(id);
            if (seeker != null)
            {
                seeker.UserId = userid;
                seeker.CompanyName = user.CompanyName;
                seeker.StartYear = user.StartYear;
                seeker.EndYear = user.EndYear;
                seeker.ComapanyUrl = user.ComapanyUrl;
                seeker.Designation=user.Designation;
                seeker.JobDescription = user.JobDescription;

                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteUser(int id)
        {
            var item = await userModel.FindAsync(id);
            userModel.Remove(item);
            await db.SaveChangesAsync();
        }
        public async Task<UserExperiences> GetUser(int id)
        {
            return await userModel.FindAsync(id);
        }

        
    }
}
