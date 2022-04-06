using Identity.WebAPI.Data;
using Identity.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.WebAPI.Repository
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private IdentityContext db;
        private IConfiguration configuration;
        private DbSet<UserIdentity> _user;

        public UserIdentityRepository(IdentityContext dbcontext, IConfiguration _configuration)
        {
            db = dbcontext;
            configuration = _configuration;
            _user = db.Set<UserIdentity>();
        }
        public async Task<UserIdentity> ChangePassword(int id, UserIdentity user)
        {
            var a = await _user.FindAsync(id);
            
                a.UserName = user.UserName;
                a.Password = user.Password;
                a.FullName = user.FullName;
                a.Email = user.Email;
                a.Phone = user.Phone;
                a.UserType = user.UserType;

                db.UserIdentity.Update(a);
                await db.SaveChangesAsync();
                return a;
            
 
        }

        public string GetPassword(int id)
        {
            var pass= _user.Single(e => e.Id == id);

            return pass.Password;
        }

        public string GetToken(UserIdentity user)
        {
            
                var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, configuration.GetValue<string>("Jwt:Audience")));
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "Api.Gateway"));
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "JobSeeker.WebAPI"));
                claims.Add(new Claim(ClaimTypes.Role, user.UserType));



                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Secret")));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("Jwt:Issuer"),
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(1440),
                    signingCredentials: credentials
                 );
                return new JwtSecurityTokenHandler().WriteToken(token);

            


        }

        public UserIdentity LoginUser(LoginModel credentials)
        {
            var user = _user.SingleOrDefault(u => u.UserName == credentials.UserName && u.Password == credentials.Password);
            return user;
        }

        public async Task<UserIdentity> RegisterUser(UserIdentity item)
        {
            await _user.AddAsync(item);
            await db.SaveChangesAsync();

            return item;
        }
    }
}
