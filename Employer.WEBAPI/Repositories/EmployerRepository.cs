using Employer.WEBAPI.Data;
using Employer.WEBAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Employer.WEBAPI.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private EmployerContext db;
        private DbSet<EmployerDetails> employeer;
        public EmployerRepository(EmployerContext dbcontext)
        {
            db = dbcontext;
            employeer = db.Set<EmployerDetails>();
        }
        public async Task<EmployerDetails> AddEmployeer(EmployerDetails item)
        {
            await employeer.AddAsync(item);
            await db.SaveChangesAsync();
            return item;
        }

        public async Task UpdateEmployeer(int empId, EmployerDetails item)
        {
            var emp = await employeer.FindAsync(empId);
            if (emp != null)
            {
                
                emp.Organization = item.Organization;
                emp.OrganizationType = item.OrganizationType;
                emp.CompanyEmail = item.CompanyEmail;
                emp.CompanyPhone = item.CompanyPhone;
                emp.NoOfEmployees = item.NoOfEmployees;
                emp.StartYear = item.StartYear;
                emp.About = item.About;
                emp.CreatedBy = item.CreatedBy;

                await db.SaveChangesAsync();
            }
        }

        public async Task<EmployerDetails> GetEmployerByIDAsync(string email)
        {
            var item= await employeer.SingleOrDefaultAsync(e=>e.CreatedBy== email);
            return item;
            }

    }

}
