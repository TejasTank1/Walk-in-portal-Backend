using Backend.Models1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("")]
    [ApiController]
    public class LoginController : ControllerBase
    { 
        private WalkInPortalDb1Context appdbcont;
        public LoginController(WalkInPortalDb1Context walkin)
        {
            this.appdbcont = walkin;
        }
        

        [HttpPost("getuserid", Name = "GetUserIdfromEmail")]
        public async Task<ActionResult> GetIdfromEmail([FromBody]UserReg obj)
        {  
            if (obj == null)
            {
                return BadRequest();
            } 
            var student = appdbcont.UserRegs.Where(n => n.Email == obj.Email).FirstOrDefault();
            if (student == null || obj.Password!=student.Password)
            {
                return NotFound();
            }
            return Ok(student.Id);
        }

        [HttpGet("getqualification", Name = "GetTechnologyTable")]
        public async Task<ActionResult> Getqualificationtable()
        {
            var technologies = await appdbcont.Qualifications.ToListAsync();

            return Ok(technologies);
        }

        [HttpGet("getstream", Name = "GetStreamTable")]
        public async Task<ActionResult> Getstreamtable()
        {
            var streams = await appdbcont.Streams.ToListAsync();

            return Ok(streams);
        }

        [HttpGet("getcolleges", Name = "GetCollegeTable")]
        public async Task<ActionResult> Getcollegetable()
        {
            var colleges = await appdbcont.Colleges.ToListAsync();

            return Ok(colleges);
        }

        [HttpGet("gettechnologies", Name = "GetAllTechnologies")]
        public async Task<ActionResult> GetAllTechnologies()
        {
            var technologies = await appdbcont.TechnologyTables.ToListAsync();

            return Ok(technologies);
        }

        [HttpGet("getjobroles", Name = "GetAllJobroles")]
        public async Task<ActionResult> GetAllJobroles()
        {
            var technologies = await appdbcont.AllJobRolesOfUsers.ToListAsync();

            return Ok(technologies);
        }

        [HttpGet("getuserreghasalljob", Name = "GetUserreghasjobroles")]
        public async Task<ActionResult> AddUserreg()
        {
            var alluserjobroles=await appdbcont.user_reg_has_all_job.ToListAsync();
            return Ok(alluserjobroles);
        }
 


        //post requests............................

        [HttpPost("addtechnologies", Name = "AddJObroles")]
        public async Task<ActionResult> AddJobroles([FromBody]AllJobRolesOfUser jobrole)
        {
            await appdbcont.AllJobRolesOfUsers.AddAsync(jobrole);
            await appdbcont.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("adduserreghasalljob", Name = "AddUserreghasjobroles")]
        public async Task<ActionResult> AddUserreg([FromBody] user_reg_has_all_job userjob)
        {
            await appdbcont.user_reg_has_all_job.AddAsync(userjob);
            await appdbcont.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("user/add", Name = "AddUserLogin")]
        public async Task<ActionResult> AddUser([FromBody] UserReg obj)
        {
            if (obj == null)
            {
                return BadRequest("User should not be null.");
            }

            await appdbcont.UserRegs.AddAsync(obj);
            await appdbcont.SaveChangesAsync();
            return Ok(obj.Id);
        }

        [HttpPost("user/addeduinfo", Name = "AddUserEduinfo")]
        public async Task<ActionResult> AddUserEducation([FromBody] EdiucationInfo eduinfo)
        {
            if (eduinfo == null)
            {
                return BadRequest("EducationInfo should not be null.");
            }

            await appdbcont.EdiucationInfos.AddAsync(eduinfo);
            await appdbcont.SaveChangesAsync();
            return Ok(); 
        }

        [HttpPost("user/addpersonalinfo", Name = "AddUserPersonalinfo")]
        public async Task<ActionResult> AddUserPersonal([FromBody] PersonalInfo perinfo)
        {
            if (perinfo == null)
            {
                return BadRequest("Personalinfo should not be null.");
            }

            await appdbcont.PersonalInfos.AddAsync(perinfo);
            await appdbcont.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("user/addprofesinfo", Name = "AddUserProfesionalinfo")]
        public async Task<ActionResult> AddUserProfesional([FromBody] ProfessionalQualificationInfo profesinfo)
        {
            if (profesinfo == null)
            {
                return BadRequest("Profesional should not be null.");
            }

            await appdbcont.ProfessionalQualificationInfos.AddAsync(profesinfo);
            await appdbcont.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("user/addprofesinfo/techfamilier", Name = "AddtechnologyFamilier")]
        public async Task<ActionResult> AddFamilier([FromBody] professional_qualification_info_has_technology_familier_table techobj)
        {
            if (techobj == null)
            {
                return BadRequest();
            }
            
            await appdbcont.professional_qualification_info_has_technology_familier_table.AddAsync(techobj);
            await appdbcont.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("user/addprofesinfo/techexpert", Name = "AddtechnologyExpert")]
        public async Task<ActionResult> AddExpert([FromBody] professional_qualification_info_has_technology_expert_table techobj)
        {
            if (techobj == null)
            {
                return BadRequest();
            }
            await appdbcont.professional_qualification_info_has_technology_expert_table.AddAsync(techobj);
            await appdbcont.SaveChangesAsync();
            return Ok();
        }
         
         
    }
}
