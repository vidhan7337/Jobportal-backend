using JobSeeker.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public interface IQualificationsRepository
    {
        Task<Qualifications> AddQualification(Qualifications user);
        Task UpdateUser(int id, Qualifications user);
        Task DeleteUser(int id);
        Task<Qualifications> GetUser(int id);
        List<Qualifications> GetAll(int userid);
    }
}
