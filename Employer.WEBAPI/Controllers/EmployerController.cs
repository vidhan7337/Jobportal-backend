using Employer.WEBAPI.Models;
using Employer.WEBAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employer.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private IEmployerRepository _repository;

        public EmployerController(IEmployerRepository repo)
        {
            _repository = repo;
        }

        //GET/ api/employeer
        //[Authorize(Roles = "Employer")]
        [HttpPost]

        public async Task<ActionResult<EmployerDetails>> AddEmployeer(EmployerDetails employeer)
        {
            TryValidateModel(employeer);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _repository.AddEmployeer(employeer);
                return Created("Success", result);
            }
        }

        //[Authorize(Roles = "Employer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeer([FromBody] EmployerDetails employeer, [FromRoute] int id)
        {
            await _repository.UpdateEmployeer(id, employeer);
            return Ok();
        }

        //[Authorize(Roles = "JobSeeker")]
        [HttpGet("{email}")]
        public async Task<ActionResult<EmployerDetails>> GetEmployerByID(string email)
        {
            var item = await _repository.GetEmployerByIDAsync(email);
            if (item == null)
                return NotFound();
            else
                return Ok(item);
        }
    }
}
