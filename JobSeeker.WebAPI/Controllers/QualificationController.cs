using JobSeeker.WebAPI.Models;
using JobSeeker.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Controllers
{
    [Route("api/JobSeeker/{userid:int}/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
       
        private IQualificationsRepository _qualiRepo;


        public QualificationController( IQualificationsRepository qua)
        {
            
            _qualiRepo = qua;
            
        }
        [HttpPost]
        public async Task<ActionResult<Qualifications>> AddJobseeker(Qualifications user)
        {
            TryValidateModel(user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _qualiRepo.AddUser(user);
                return Created("Success", result);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobseeker([FromBody] Qualifications user, [FromRoute] int id, int userid)
        {
            await _qualiRepo.UpdateUser(id, user,userid);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(int id)
        {
            var item = await _qualiRepo.GetUser(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                await _qualiRepo.DeleteUser(id);
                return NoContent();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Qualifications>> GetJobSeeker(int id)
        {
            var item = await _qualiRepo.GetUser(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }
    }
}
