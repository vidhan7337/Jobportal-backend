using Employer.WEBAPI.Data;
using Employer.WEBAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employer.WEBAPI.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private EmployerContext db;
        private DbSet<VacancyDetails> vac;

        public VacancyRepository(EmployerContext dbcontext)
        {
            db = dbcontext;
            vac = db.Set<VacancyDetails>();
        }
        public async Task<VacancyDetails> AddVacancyAsync(VacancyDetails vacancy)
        {
            await vac.AddAsync(vacancy);
            await db.SaveChangesAsync();
            return vacancy;
        }

        public async Task DeleteVacancyAsync(int vacId)
        {
            var item = await vac.FindAsync(vacId);
            vac.Remove(item);
            await db.SaveChangesAsync();
        }

        public List<VacancyDetails> GetAllVacanciesAsync()
        {
            return vac.ToList();
        }

        public async Task<VacancyDetails> GetVacancyByIDAsync(int vacId)
        {
            var item = await vac.FindAsync(vacId);
            return item;
        }

        public async Task UpdateVacancyAsync(int vacId, VacancyDetails vacancy)
        {
            var vacn = await vac.FindAsync(vacId);
            if (vacn != null)
            {
                vacn.PublishedDate = vacancy.PublishedDate;
                vacn.PublishedBy = vacancy.PublishedBy;
                vacn.NoOfVacancies = vacancy.NoOfVacancies;
                vacn.MinimumQualification = vacancy.MinimumQualification;
                vacn.JobDescription = vacancy.JobDescription;
                vacn.ExperienceRequired = vacancy.ExperienceRequired;
                vacn.LastDate = vacancy.LastDate;
                vacn.MinSalary = vacancy.MinSalary;
                vacn.MinSalary = vacancy.MinSalary;


                await db.SaveChangesAsync();
            }
        }

        public List<VacancyDetails> GetAllSearchedVacanciesAsync(string search)
        {
            
                List<VacancyDetails> x = db.Vacancies.Where(e => e.JobDescription.Contains(search)|| e.PublishedBy.Contains(search)).ToList();
                return x;
            
        }

        public List<VacancyDetails> SubmittedVacancy(string org)
        {
            string StoredProc = "EXEC SelectAllSubmittedVacancies @Org = '" + org + "'";

            return db.Vacancies.FromSqlRaw(StoredProc).ToList();



            /*List<VacancyDetails> x= db.Vacancies.Where(u=>u.PublishedBy==org).ToList();
            return x;*/

        }
    }

}
