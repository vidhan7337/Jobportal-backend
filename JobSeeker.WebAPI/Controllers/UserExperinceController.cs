using JobSeeker.WebAPI.Models;
using JobSeeker.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Controllers
{
    [Route("api/JobSeeker/{userid:int}/[controller]")]
    [ApiController]
    public class UserExperinceController : ControllerBase
    {
        
        private IExperiencesRepository _expRepo;

        public UserExperinceController (IExperiencesRepository ex)
        {
          
            _expRepo = ex;
        }
        [HttpPost]
        public async Task<ActionResult<UserModel>> AddExperience(UserExperiences user)
        {
            TryValidateModel(user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _expRepo.AddUser(user);
                return Created("Success", result);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobseeker([FromBody] UserExperiences user, [FromRoute] int id, int userid)
        {
            await _expRepo.UpdateUser(id, user,userid);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(int id)
        {
            var item = await _expRepo.GetUser(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                await _expRepo.DeleteUser(id);
                return NoContent();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserExperiences>> GetJobSeeker(int id)
        {
            var item = await _expRepo.GetUser(id);
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
