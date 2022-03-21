using Employer.WEBAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employer.WEBAPI.Repositories
{
    public interface IVacancyRepository
    {
        List<VacancyDetails> GetAllVacanciesAsync();
        Task<VacancyDetails> GetVacancyByIDAsync(int vacId);
        Task<VacancyDetails> AddVacancyAsync(VacancyDetails vacancy);
        Task UpdateVacancyAsync(int vacId, VacancyDetails vacancy);
        //Task UpdateEmployeePatchAsync(int empid, JsonPatchDocument employeeModel);
        Task DeleteVacancyAsync(int vacId);

        List<VacancyDetails> GetAllSearchedVacanciesAsync(string search);
        List<VacancyDetails> SubmittedVacancy(string org);
    }
}
