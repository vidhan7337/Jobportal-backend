using JobSeeker.WebAPI.Models;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> AddUser(UserModel user);
        Task UpdateUser(int id, UserModel user);
        Task DeleteUser(int id);
        Task<UserModel> GetUser(int id);
        Task<UserModel> GetJobSeekerByIDAsync(string email);


    }
}
