using JobSeeker.WebAPI.Models;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public interface IQualificationsRepository
    {
        Task<Qualifications> AddUser(Qualifications user);
        Task UpdateUser(int id, Qualifications user, int userid);
        Task DeleteUser(int id);
        Task<Qualifications> GetUser(int id);
    }
}
