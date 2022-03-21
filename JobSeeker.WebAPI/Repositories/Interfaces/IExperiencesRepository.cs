using JobSeeker.WebAPI.Models;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public interface IExperiencesRepository
    {
        Task<UserExperiences> AddUser(UserExperiences user);
        Task UpdateUser(int id, UserExperiences user,int userid);
        Task DeleteUser(int id);
        Task<UserExperiences> GetUser(int id);
    }
}
