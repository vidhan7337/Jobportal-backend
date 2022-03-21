using Identity.WebAPI.Data;
using Identity.WebAPI.Models;

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
        private readonly IdentityContext db;
        private IConfiguration configuration;
        


        public IdentityController(IdentityContext dbContext, IConfiguration _configuration)
        {
            db = dbContext;
            configuration = _configuration;
            
        }

        [HttpPut("changepassword/{id}")]
        public async Task<IActionResult> ChangePassword([FromBody]UserIdentity user,[FromRoute]int id)
        {
            var a = await db.UserIdentity.FindAsync(id);
            if (a != null)
            {
                a.UserName = user.UserName;
                a.Password = user.Password;
                a.FullName = user.FullName;
                a.Email = user.Email;
                a.Phone = user.Phone;
                a.UserType = user.UserType;

                db.UserIdentity.Update(a);
                await db.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserIdentity user)
        {
            TryValidateModel(user);
            if (ModelState.IsValid)
            {
                await db.UserIdentity.AddAsync(user);
                await db.SaveChangesAsync();
                return Created("", new {FullName = user.FullName, UserName = user.UserName, Email = user.Email, Phone = user.Phone, UserType = user.UserType });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

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
                var user = db.UserIdentity.SingleOrDefault(u => u.UserName == credentials.UserName && u.Password == credentials.Password);
                if (user == null)
                {
                    return Unauthorized();

                }
                else
                {
                    var tokenString = GetToken(user);
                    return Ok(new { id=user.Id,email=user.Email,phone=user.Phone, FullName = user.FullName,password=user.Password, UserName = user.UserName, userType=user.UserType,tokenString });
                }
            }
        }

        private string GetToken(UserIdentity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, configuration.GetValue<string>("Jwt:Audience")));
            claims.Add(new Claim(ClaimTypes.Role, user.UserType));



            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer:configuration.GetValue<string>("Jwt:Issuer"),
                audience:null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
