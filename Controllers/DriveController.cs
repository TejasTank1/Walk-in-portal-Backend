using Backend.Configurations;
using Backend.Models1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{
    [Route("")]
    [ApiController]
    public class DriveController : ControllerBase
    {

        private WalkInPortalDb1Context appdbcont;
        private readonly JwtConfig _jwtconfig;

        public DriveController(WalkInPortalDb1Context apcont, IOptionsMonitor<JwtConfig> _optionsMonitor)
        {
            this.appdbcont= apcont;
            _jwtconfig = _optionsMonitor.CurrentValue;
        }

        //private string GetjwtToken(IdentityUser user)
        //{
        //    var jwthandler = new JwtSecurityTokenHandler();

        //    var key = Encoding.ASCII.GetBytes(_jwtconfig.Secret);

        //    var tokendescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {  
        //            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email),
        //            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
        //            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //                           SecurityAlgorithms.HmacSha256)
        //    };

        //    var token = jwthandler.CreateToken(tokendescriptor);
        //    var jwttoken = jwthandler.WriteToken(token);
        //    return jwttoken;
        //}

        //private string ValidateToken1(string token)
        //{
        //    if (token == null)
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtconfig.Secret);
        //    try
        //    {
        //        tokenHandler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //            ClockSkew = TimeSpan.Zero
        //        }, out SecurityToken validatedToken);

        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        var userId = (jwtToken.Claims.First(x => true).Value);

        //        // return user id from JWT token if validation successful
        //        return userId;
        //    }
        //    catch
        //    {
        //        // return null if validation fails
        //        return null;
        //    }

        //    return "";
        //}

        //
        //
        //
        // drives get requests................
        //
        //
        //


        [HttpGet("getjobrolewithdetail/{id}", Name = "GetJobrole")]
        public async Task<ActionResult> GetJobroleWithdetails(int id)
        {
            var userjobroleswithdetails = await appdbcont.JobRoles.Where(n=>n.RoleId==id).FirstOrDefaultAsync();
            return Ok(userjobroleswithdetails);
        }


        [HttpGet("getSlots", Name = "GetJobslots")]
        public async Task<ActionResult> GetSlots()
        {
            var Slots = await appdbcont.Slots.ToListAsync();
            return Ok(Slots);
        } 

        [HttpGet("getWalkindrives", Name = "GetAllDrives")]
        public async Task<ActionResult> GetAlldrives()
        {
            var drives = await appdbcont.WalkInDrives.ToListAsync();
            return Ok(drives);
        }

        [HttpGet("getWalkindrivebyid/{id}", Name = "GetDriveById")]
        public async Task<ActionResult> GetDriveById(int id)
        { 
            var drive = await appdbcont.WalkInDrives.Where(n=>n.DriveId==id).FirstOrDefaultAsync();
            return Ok(drive);
        }

        [HttpGet("getPrerequisite/{id}", Name = "GetAllPrerequisitefordrive")]
        public async Task<ActionResult> GetAllPrerequisite([FromRoute] int id)
        {
            var prerequisite = await appdbcont.Prerequisites.Where(n => n.PreId == id).FirstOrDefaultAsync();
            if (prerequisite == null)
            {
                return NotFound();
            }
            return Ok(prerequisite);
        }

        [HttpGet("getroundsbyid/{id}", Name = "GetAllRoundsOfDriveById")]
        public async Task<ActionResult> GetAllRoundsOfDriveById([FromRoute] int id)
        {
            var Rounds = await appdbcont.Rounds.Where(n => n.DriveId == id).ToListAsync();
            if (Rounds == null)   
            {
                return NotFound();
            }
            return Ok(Rounds);
        }

        [HttpGet("getalljobrolestable", Name = "GetAlljobrolestable")]
        public async Task<ActionResult> Getalljobrolestable()
        {
            var allroles = await appdbcont.JobRoles.ToListAsync();

            return Ok(allroles);
        }

        [HttpGet("getjobrolesidsbydriveid/{id}", Name = "GetAllJobrolesIdOfDriveById")]
        public async Task<ActionResult> GetAllJobrolesIdOfDriveById([FromRoute] int id)
        {
            var Roundsids = await appdbcont.walk_in_drives_has_job_roles.Where(n=>n.Drive_id == id).ToListAsync();
            if (Roundsids == null)
            {
                return NotFound();
            }
            return Ok(Roundsids);
        }

        [HttpGet("getalldrivesapplied/{userid}", Name = "GetAllDrivesappliedbyuser")]
        public async Task<ActionResult> GetAllDrivesappliedbyuser([FromRoute] int userid)
        {
            var Usersdriveapplied = await appdbcont.DriveApplieds.Where(n=> n.Id==userid).ToListAsync();
            if (Usersdriveapplied == null)
            {
                return NotFound();
            }
            return Ok(Usersdriveapplied);
        }


        [HttpGet("getdrivesjobroles/{userid}", Name = "GetAllJobrolesofAlldrive")]
        public async Task<ActionResult> GetAllJobrolesofdrive([FromRoute] int userid)
        {
            var usersdrivesappliedjobroles = await appdbcont.drive_applied_has_job_roles.Where(n=>n.Id==userid).ToListAsync();
            if (usersdrivesappliedjobroles == null)
            {
                return NotFound();
            }
            return Ok(usersdrivesappliedjobroles);
        }

        [HttpGet("getdrivesavailabletime/{Driveid}", Name = "GetAllAvailabletimeofDrive")]
        public async Task<ActionResult> GetAllAvailabletimeofDrive([FromRoute] int Driveid)
        {
            var drivesavailabletimes = await appdbcont.DriveAvailableTimes.Where(n=>n.WalkInDrivesDriveId==Driveid).ToListAsync();

            if (drivesavailabletimes == null)
            {
                return NotFound();
            }
            return Ok(drivesavailabletimes);
        }
         
        //
        //
        //
        // drives post requests................
        //
        //
        //

        [HttpPost("drive/driveapplied/Add", Name = "AddtoDriveapplied")]
        public async Task<ActionResult> AddtoDriveapplied([FromBody] DriveApplied driveofuser)
        {
            if(driveofuser == null)
            {
                return BadRequest();
            }

            await appdbcont.DriveApplieds.AddAsync(driveofuser);
            await appdbcont.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("drive/driveappliedjobroles/Add", Name = "AddtoDriveappliedjobrole")]
        public async Task<ActionResult> AddtoDriveappliedjobrole([FromBody] drive_applied_has_job_roles jobroleofapplieddrive)
        {
            if (jobroleofapplieddrive == null)
            {
                return BadRequest();
            }

            await appdbcont.drive_applied_has_job_roles.AddAsync(jobroleofapplieddrive);
            await appdbcont.SaveChangesAsync();

            return Ok();
        }

    }
}
