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
        private static int PAGE_SIZE { get; set; } = 5;

        public VacancyRepository(EmployerContext dbcontext)
        {
            db = dbcontext;
            vac = db.Set<VacancyDetails>();
        }
        //adding new vacancy to database
        public async Task<VacancyDetails> AddVacancyAsync(VacancyDetails vacancy)
        {
            await vac.AddAsync(vacancy);
            await db.SaveChangesAsync();
            return vacancy;
        }
        //updating vacancy data in database
        public async Task DeleteVacancyAsync(int vacId)
        {
            var item = await vac.FindAsync(vacId);
            vac.Remove(item);
            await db.SaveChangesAsync();
        }
        //getting all vacancies to show to jobseeker
        public ResponseModel GetAllVacanciesAsync(string?search,decimal? minsalary, decimal? maxsalary, string? sortby, int page)
        {


            //return vac.ToList();
            var items = db.Vacancies.AsQueryable();
            //filters
            if (minsalary.HasValue)
            {
                items = items.Where(e => e.MinSalary >= minsalary);
            }
            if (maxsalary.HasValue)
            {
                items = items.Where(e => e.MaxSalary <= maxsalary);
            }
            if (!string.IsNullOrEmpty(search))
            {
                items = items.Where(e => e.JobDescription.Contains(search) || e.PublishedBy.Contains(search));
            }
            //sorting
            items = items.OrderBy(e => e.PublishedDate);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "noofvacancy_desc": items = items.OrderByDescending(e => e.NoOfVacancies); break;
                    case "noofvacancy_asc": items = items.OrderBy(e => e.NoOfVacancies); break;
                    case "publisheddate_desc": items = items.OrderByDescending(e => e.PublishedDate); break;
                    case "experience_asc": items = items.OrderBy(e => e.ExperienceRequired); break;
                    case "experience_desc": items = items.OrderByDescending(e => e.ExperienceRequired); break;
                    case "lastdate_asc": items = items.OrderBy(e => e.LastDate); break;
                    case "lastdate_desc": items = items.OrderByDescending(e => e.LastDate); break;

                }
            }
            //pagination
            var result = PaginatedList<VacancyDetails>.Create(items, page, PAGE_SIZE);
            //response model
            return new ResponseModel() {
                VacancyDetails = result.ToList(),
                pageIndex = result.PageIndex,
                totalPage = result.TotalPage,
                totalItems = items.Count()
            };
        }
        //data of single vacancy
        public async Task<VacancyDetails> GetVacancyByIDAsync(int vacId)
        {
            var item = await vac.FindAsync(vacId);
            return item;
        }
        //update vacancy
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
        //searching vacacny using jobdescription and company name
        public List<VacancyDetails> GetAllSearchedVacanciesAsync(string search)
        {
            
                List<VacancyDetails> x = db.Vacancies.Where(e => e.JobDescription.Contains(search)|| e.PublishedBy.Contains(search)).ToList();
                return x;
            
        }
        //showing submitted vacancies to employer
        public ResponseModel SubmittedVacancy(string org, decimal? minsalary,decimal? maxsalary,string? sortby,int page)
        {
            /*List<VacancyDetails> x= db.Vacancies.Where(u=>u.PublishedBy==org).ToList();
            return x;*/
            //string StoredProc = "EXEC SelectAllSubmittedVacancies @Org = '" + org + "'";

            var items= db.Vacancies.Where(u => u.PublishedBy == org).AsQueryable();
            //filter
            if (minsalary.HasValue)
            {
                items = items.Where(e => e.MinSalary >= minsalary);
            }
            if (maxsalary.HasValue)
            {
                items = items.Where(e => e.MaxSalary <= maxsalary);
            }
            //sorting
            items = items.OrderBy(e => e.PublishedDate);
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "noofvacancy_desc": items=items.OrderByDescending(e=>e.NoOfVacancies); break;
                    case "noofvacancy_asc":items = items.OrderBy(e => e.NoOfVacancies);break;
                    case "publisheddate_desc": items = items.OrderByDescending(e => e.PublishedDate); break;
                    case "experience_asc": items = items.OrderBy(e => e.ExperienceRequired); break;
                    case "experience_desc": items = items.OrderByDescending(e => e.ExperienceRequired); break;
                    case "lastdate_asc": items = items.OrderBy(e => e.LastDate); break;
                    case "lastdate_desc": items = items.OrderByDescending(e => e.LastDate); break;
                    
                }
            }
            //pagination
            var result = PaginatedList<VacancyDetails>.Create(items, page, PAGE_SIZE);
            //response model
            return new ResponseModel() {
                VacancyDetails= result.ToList(),
                pageIndex=result.PageIndex,
                totalPage=result.TotalPage,
                totalItems=items.Count()
            };

            

        }
    }

}
