using JobSeeker.WebAPI.Data;
using JobSeeker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public class QualificationsRepository:IQualificationsRepository
    {
        private JobSeekerContext db;
        private DbSet<Qualifications> userModel;
        public QualificationsRepository(JobSeekerContext dbcontext)
        {
            db = dbcontext;
            userModel = db.Set<Qualifications>();
        }

        public async Task<Qualifications> AddUser(Qualifications user)
        {
            await userModel.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }
        public async Task UpdateUser(int id, Qualifications user, int userid)
        {
            var seeker = await userModel.FindAsync(id);
            if (seeker != null)
            {
                seeker.UserId = userid;
                seeker.QualificationName = user.QualificationName;
                seeker.University=user.University;
                seeker.YearOfCompletion=user.YearOfCompletion;
                seeker.GradeOrScore=user.GradeOrScore;

                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteUser(int id)
        {
            var item = await userModel.FindAsync(id);
            userModel.Remove(item);
            await db.SaveChangesAsync();
        }
        public async Task<Qualifications> GetUser(int id)
        {
            return await userModel.FindAsync(id);
        }

    }
}
