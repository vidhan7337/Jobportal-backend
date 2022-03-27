using Employer.WEBAPI.Models;
using System.Threading.Tasks;

namespace Employer.WEBAPI.Repositories
{
    public interface IEmployerRepository
    {
        Task<EmployerDetails> AddEmployeer(EmployerDetails item);
        Task UpdateEmployeer(int empId, EmployerDetails item);
        Task<EmployerDetails> GetEmployerByIDAsync(string email);
        Task<EmployerDetails> GetEmployerByNameAsync(string org);
    }
}
