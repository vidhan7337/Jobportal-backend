using Identity.WebAPI.Data;
using Identity.WebAPI.Models;
using Identity.WebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {


        private IUserIdentityRepository _repo;
        


        public IdentityController(IUserIdentityRepository repository)
        {
            _repo= repository;
            
        }
        //changepassword of user
        [HttpPut("changepassword/{id}")]
        public IActionResult ChangePassword([FromBody] UserIdentity user, [FromRoute] int id)
        {
            var a = _repo.ChangePassword(id, user);
            if (a != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //registering new user
        [HttpPost("register")]
        public IActionResult Register(UserIdentity user)
        {
            TryValidateModel(user);
            if (ModelState.IsValid)
            {
                _repo.RegisterUser(user);
                return Created("", new { FullName = user.FullName, UserName = user.UserName, Email = user.Email, Phone = user.Phone, UserType = user.UserType });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        //login api through login model
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Login(LoginModel credentials)
        {
            TryValidateModel(credentials);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                UserIdentity user = _repo.LoginUser(credentials);
                if (user == null)
                {
                    return Unauthorized();

                }
                else
                {
                    var tokenString = _repo.GetToken(user);
                    return Ok(new { id=user.Id,email=user.Email,phone=user.Phone, FullName = user.FullName,password=user.Password, UserName = user.UserName, userType=user.UserType,tokenString });
                }
            }
        }
        //generating token
        
        [HttpGet("getpassword/{id}")]

        //getting old password to change new password
        public string Getpassword(int id)
        {
            var pass= _repo.GetPassword(id);

            return pass;

        }
    }
}
