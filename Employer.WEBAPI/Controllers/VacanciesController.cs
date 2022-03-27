using Employer.WEBAPI.Models;
using Employer.WEBAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Employer.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private IVacancyRepository _repository;

        public VacanciesController(IVacancyRepository repo)
        {
            _repository = repo;
        }

        // add new vacancy
        [HttpPost]

        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<VacancyDetails>> AddVacancy(VacancyDetails vac)
        {
            TryValidateModel(vac);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _repository.AddVacancyAsync(vac);
                return Created("Success", result);
            }
        }
        //update vacancy
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVacancy([FromBody] VacancyDetails vac, [FromRoute] int id)
        {
            await _repository.UpdateVacancyAsync(id, vac);
            return Ok();
        }
       
        [HttpDelete("{id:int}")]
        //delete vacancy
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var item = await _repository.GetVacancyByIDAsync(id);
            if (item == null)
                return NotFound();
            else
            {
                await _repository.DeleteVacancyAsync(id);
                return NoContent();
            }
        }
        //get all vacancies to show jobseeker
        [HttpGet]

        public ActionResult<ResponseModel> GetVacancies(string? search, decimal? maxsalary, decimal? minsalary, string? sortby, int page)
        {
            return Ok(_repository.GetAllVacanciesAsync(search,minsalary,maxsalary,sortby,page));
        }
        //get single vacancy by id
        [HttpGet("{id:int}")]

        public async Task<ActionResult<VacancyDetails>> GetVacancyByID(int id)
        {
            var item = await _repository.GetVacancyByIDAsync(id);
            if (item == null)
                return NotFound();
            else
                return Ok(item);
        }
        //search vacancy by job description or company name
        [HttpGet("{searchItem}")]

        public ActionResult<List<VacancyDetails>> GetSearchedVacancy([FromRoute]string searchItem)
        {
            return Ok(_repository.GetAllSearchedVacanciesAsync(searchItem));

        }
        //get submitted vacancies to show employer
        [HttpGet("submitted/{org}")]
        public ActionResult<ResponseModel> GetAllSubmittedVacancies(string org, decimal? maxsalary,decimal? minsalary, string? sortby, int page)
        {
            return Ok(_repository.SubmittedVacancy(org,minsalary,maxsalary,sortby,page));
        }
    }  
}
