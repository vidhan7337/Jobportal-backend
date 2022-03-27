using JobSeeker.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Repositories
{
    public interface IVacancyRequestRepository
    {
        Task<UserVacancyRequests> AddRequest(UserVacancyRequests user);
        List<UserModel> GetJobseeker(int id);
        bool Requestaleradyexsits(int vacid, int userid);

        List<int> GetVacancyIdApplied(int userid);
    }
}
