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
    public class QualificationController : ControllerBase
    {

        private IQualificationsRepository _qualiRepo;


        public QualificationController(IQualificationsRepository qua)
        {

            _qualiRepo = qua;

        }
        //add new qualification
        [HttpPost]
        public async Task<ActionResult<Qualifications>> AddQua(Qualifications user)
        {
            TryValidateModel(user);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var result = await _qualiRepo.AddQualification(user);
                return Created("Success", result);
            }
        }
        //update qualification
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQua([FromBody] Qualifications user, [FromRoute] int id)
        {
            await _qualiRepo.UpdateUser(id, user);
            return Ok();
        }
        //delete qualification
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQua(int id)
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
        //get single qualification
        [HttpGet("{id}")]
        public async Task<ActionResult<Qualifications>> GetQualification(int id)
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
        //get all qualification of jobseeker
        [HttpGet("all/{userid}")]

        public ActionResult<List<Qualifications>> GetAllQualification(int userid)
        {
            return Ok(_qualiRepo.GetAll(userid));
        }
    }
}
