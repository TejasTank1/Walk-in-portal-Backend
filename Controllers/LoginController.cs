using Backend.Models1;
using Backend.Models1.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private WalkInPortalDb1Context appdbcont;
        public LoginController(IConfiguration config,WalkInPortalDb1Context dbct)
        {
            this.appdbcont= dbct;
            _config = config;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            //your logic for login process
            //If login usrename and password are correct then proceed to generate token

            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
            //  _config["Jwt:Issuer"],
            //  null,
            //  expires: DateTime.Now.AddMinutes(120),
            //  signingCredentials: credentials);

            //var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            var obj=appdbcont.UserRegs.Where(n=> n.Email==loginRequest.Email && n.Password== loginRequest.Password).FirstOrDefault();

            if(obj== null)
            {
                return NotFound();
            }

            var jwttokhandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var identity=new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email,loginRequest.Email),
                new Claim(ClaimTypes.Name,obj.Id.ToString())
            });

            var cerdential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokendescript = new SecurityTokenDescriptor
             {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cerdential
            };

            var token=jwttokhandler.CreateToken(tokendescript);

            var res = new Token() { token = jwttokhandler.WriteToken(token) };

            return Ok(res);
        }

    }
}
