using JobSeeker.WebAPI.Models;
using JobSeeker.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobSeeker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExperinceController : ControllerBase
    {
        
        private IExperiencesRepository _expRepo;

        public UserExperinceController (IExperiencesRepository ex)
        {
          
            _expRepo = ex;
        }
        //add new experience
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
        //update experience
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExp([FromBody] UserExperiences user, [FromRoute] int id)
        {
            await _expRepo.UpdateUser(id, user);
            return Ok();
        }
        //delete experience
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExp(int id)
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
        //get single experience
        [HttpGet("{id}")]
        public async Task<ActionResult<UserExperiences>> GetExp(int id)
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
        //get all experiences of jobseeker
        [HttpGet("all/{userid}")]

        public ActionResult<List<UserExperiences>> GetAllExperience(int userid)
        {
            return Ok(_expRepo.GetAll(userid));
        }
    }
}
