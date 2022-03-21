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

        //[Authorize(Roles = "Employer")]
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
        //[Authorize(Roles = "Employer")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVacancy([FromBody] VacancyDetails vac, [FromRoute] int id)
        {
            await _repository.UpdateVacancyAsync(id, vac);
            return Ok();
        }
       // [Authorize(Roles = "Employer")]
        [HttpDelete("{id:int}")]

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
        //[Authorize(Roles = "JobSeeker")]
        [HttpGet]

        public ActionResult<List<VacancyDetails>> GetVacancies()
        {
            return Ok(_repository.GetAllVacanciesAsync());
        }
        //[Authorize(Roles = "Employer")]
        [HttpGet("{id:int}")]

        public async Task<ActionResult<VacancyDetails>> GetVacancyByID(int id)
        {
            var item = await _repository.GetVacancyByIDAsync(id);
            if (item == null)
                return NotFound();
            else
                return Ok(item);
        }
        //[Authorize(Roles = "JobSeeker")]
        [HttpGet("{searchItem}")]

        public ActionResult<List<VacancyDetails>> GetSearchedVacancy([FromRoute]string searchItem)
        {
            return Ok(_repository.GetAllSearchedVacanciesAsync(searchItem));

        }
        //[Authorize(Roles ="Employer")]
        [HttpGet("submitted/{org}")]
        public ActionResult<List<VacancyDetails>> GetAllSubmittedVacancies([FromRoute]string org)
        {
            return Ok(_repository.SubmittedVacancy(org));
        }
    }  
}
