using JobSeeker.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public interface IExperiencesRepository
    {
        Task<UserExperiences> AddUser(UserExperiences user);
        Task UpdateUser(int id, UserExperiences user);
        Task DeleteUser(int id);
        Task<UserExperiences> GetUser(int id);
        public List<UserExperiences> GetAll(int userid);
    }
}
