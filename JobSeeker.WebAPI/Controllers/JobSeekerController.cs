using JobSeeker.WebAPI.Models;
using JobSeeker.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private IUserRepository _userRepo;
        private IQualificationsRepository _qualiRepo;
        private IExperiencesRepository _expRepo;

        public JobSeekerController(IUserRepository repo, IExperiencesRepository ex, IQualificationsRepository qua)
        {
            _userRepo = repo;
            _qualiRepo = qua;
            _expRepo = ex;
        }
        //adding new jobseeker profile
        [HttpPost]
        public async Task<ActionResult<UserModel>> AddJobseeker(UserModel user)
        {
            TryValidateModel(user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _userRepo.AddUser(user);
                return Created("Success", result);
            }
        }
        //updating jobseeker profile
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobseeker([FromBody]UserModel user,[FromRoute]int id)
        {
            await _userRepo.UpdateUser(id, user);
            return Ok();
        }
        //delete jobseeker profile
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobSeeker(int id)
        {
            var item = await _userRepo.GetUser(id);
            if(item == null)
            {
                return NotFound();
            }
            else
            {
                await _userRepo.DeleteUser(id);
                return NoContent();
            }
        }
        //jobseeker profile using id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetJobSeeker(int id)
        {
            var item=await _userRepo.GetUser(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item);
            }
        }
        //jobseeker details using email for initial check when login
        [HttpGet("getbyemail/{email}")]
        public async Task<ActionResult<UserModel>> GetJobSeekerByemail(string email)
        {
            var item = await _userRepo.GetJobSeekerByIDAsync(email);
            if (item == null)
                return NotFound();
            else
                return Ok(item);
        }
    }
}
