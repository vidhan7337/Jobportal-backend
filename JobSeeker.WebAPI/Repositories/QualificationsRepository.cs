using JobSeeker.WebAPI.Data;
using JobSeeker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        //add new qualification
        public async Task<Qualifications> AddQualification(Qualifications user)
        {
            await userModel.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }
        //update qualification
        public async Task UpdateUser(int id, Qualifications user)
        {
            var seeker = await userModel.FindAsync(id);
            if (seeker != null)
            {
                seeker.UserId = user.UserId;
                seeker.QualificationName = user.QualificationName;
                seeker.University=user.University;
                seeker.YearOfCompletion=user.YearOfCompletion;
                seeker.GradeOrScore=user.GradeOrScore;

                await db.SaveChangesAsync();
            }
        }
        //delete qualification
        public async Task DeleteUser(int id)
        {
            var item = await userModel.FindAsync(id);
            userModel.Remove(item);
            await db.SaveChangesAsync();
        }
        //get single qualification
        public async Task<Qualifications> GetUser(int id)
        {
            return await userModel.FindAsync(id);
        }
        //get all qualification of jobseeker
        public List<Qualifications> GetAll(int userid)
        {
            string Storpro = "EXEC SelectAllQualificationOfUser @userId=" + userid;

            return  db.Qualifications.FromSqlRaw(Storpro).ToList();
        }

    }
}
