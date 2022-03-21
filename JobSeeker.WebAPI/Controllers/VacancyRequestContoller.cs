using JobSeeker.WebAPI.Models;
using JobSeeker.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        [HttpGet("{id}")]
        public ActionResult<List<UserModel>> GetAllJobseekersapplied([FromRoute] int id)
        {
            return Ok(_vacrepo.GetJobseeker(id));
        }

    }
}
