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
    public class VacancyRequestContoller : ControllerBase
    {
        private IVacancyRequestRepository _vacrepo;

        public VacancyRequestContoller(IVacancyRequestRepository vacRepo,IUserRepository repo, IExperiencesRepository ex, IQualificationsRepository qua)
        {
            _vacrepo = vacRepo;
        }
        //all jobseeker who applied for particular vacancy
        [HttpGet("{id}")]
        public ActionResult<List<UserModel>> GetAllJobseekersapplied([FromRoute] int id)
        {
            return Ok(_vacrepo.GetJobseeker(id));
        }
        //add new vacancy request
        [HttpPost]
        public async Task<ActionResult<UserVacancyRequests>> AddRequest(UserVacancyRequests req) 
        {
            
            TryValidateModel(req);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            else
            {
                if (_vacrepo.Requestaleradyexsits(req.VacancyId, req.UserId) == true)
                {
                    return BadRequest("Already applied");
                }
                else
                {
                    var result = await _vacrepo.AddRequest(req);
                    return Created("Success", result);
                }
                
            }
        }
        //checking jobseeker applied for vacancy or not
        [HttpGet("{vacid}/user/{userid}")]
        public bool AlreadyApplied(int vacid, int userid)
        {
            return _vacrepo.Requestaleradyexsits(vacid,userid);
        }
        //all vacancies id applied by jobseeker
        [HttpGet("/vacancyapplied/{userid}")]

        public ActionResult<List<int>> AppliedVacancies(int userid)
        {
            return Ok(_vacrepo.GetVacancyIdApplied(userid));
        }

    }
}
