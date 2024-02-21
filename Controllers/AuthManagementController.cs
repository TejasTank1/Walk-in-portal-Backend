using Backend.Configurations;
using Backend.Models1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{ 
    [Route("")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    { 
        private readonly JwtConfig _jwtconfig;

        public AuthManagementController(IOptionsMonitor<JwtConfig> _optionsMonitor)
        { 
            _jwtconfig = _optionsMonitor.CurrentValue;
        }

        private string GetjwtToken(IdentityUser user)
        {
            var jwthandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtconfig.Secret);

            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                   SecurityAlgorithms.HmacSha256)
            };

            var token = jwthandler.CreateToken(tokendescriptor);
            var jwttoken = jwthandler.WriteToken(token);
            return jwttoken;
        }

 
        [HttpPost("Gettokenandregister",Name = "Authenticate")]
        public async Task<ActionResult> Register([FromBody] Register obj)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest("Add the Required Field.");
            }

            var user = new IdentityUser() { 
               Email = obj.Email
            }; 

            var token = GetjwtToken(user); 


            return Ok(token);

        }

         
        private string ValidateToken1(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtconfig.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = (jwtToken.Claims.First(x => true).Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }

            return "";
        }



    }
}
