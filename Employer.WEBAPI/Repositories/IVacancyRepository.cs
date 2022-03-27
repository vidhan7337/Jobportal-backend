using Employer.WEBAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employer.WEBAPI.Repositories
{
    public interface IVacancyRepository
    {
        ResponseModel GetAllVacanciesAsync(string? search, decimal? minsalary, decimal? maxsalary, string? sortby, int page);
        Task<VacancyDetails> GetVacancyByIDAsync(int vacId);
        Task<VacancyDetails> AddVacancyAsync(VacancyDetails vacancy);
        Task UpdateVacancyAsync(int vacId, VacancyDetails vacancy);
        //Task UpdateEmployeePatchAsync(int empid, JsonPatchDocument employeeModel);
        Task DeleteVacancyAsync(int vacId);

        List<VacancyDetails> GetAllSearchedVacanciesAsync(string search);
        ResponseModel SubmittedVacancy(string org,decimal? minsalary, decimal? maxsalary, string? sortby, int page);
    }
}
