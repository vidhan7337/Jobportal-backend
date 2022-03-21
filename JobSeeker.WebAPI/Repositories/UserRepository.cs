using JobSeeker.WebAPI.Data;
using JobSeeker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public class UserRepository:IUserRepository
    {
        private JobSeekerContext db;
        private DbSet<UserModel> userModel;
        public UserRepository(JobSeekerContext dbcontext)
        {
            db = dbcontext;
            userModel=db.Set<UserModel>();
        }

        public async Task<UserModel> AddUser(UserModel user)
        {
            await userModel.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }
        public async Task UpdateUser(int id, UserModel user)
        {
            var seeker = await userModel.FindAsync(id);
            if (seeker != null)
            {
                seeker.FirstName = user.FirstName;
                seeker.LastName = user.LastName;
                seeker.Email = user.Email;
                seeker.Phone = user.Phone;
                seeker.Address = user.Address;
                seeker.TotalExperience = user.TotalExperience;
                seeker.ExpectedSalary = user.ExpectedSalary;
                seeker.DateOfBirth = user.DateOfBirth;

                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteUser(int id)
        {
            var item = await userModel.FindAsync(id);
            userModel.Remove(item);
            await db.SaveChangesAsync();
        }
        public async Task<UserModel> GetUser(int id)
        {
            return await userModel.FindAsync(id);
        }

    }
}
