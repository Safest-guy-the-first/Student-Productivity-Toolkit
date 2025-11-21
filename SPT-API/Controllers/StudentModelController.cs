using Microsoft.AspNetCore.Mvc;
using SPT_API.Data.DTOs;
using SPT_API.Models;
using SPT_API.Services.Password;
using SPT_API.Services.StudentServices;

namespace SPT_API.Controllers
{
    [Route("spt/[controller]")]
    [ApiController]
    public class StudentModelController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentModelController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var allStudents = await _studentService.GetAllStudents();
            return Ok(allStudents);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetStudentByParams([FromQuery] string FirstName, [FromQuery] string LastName)
        {
            var studentByParams = await _studentService.GetStudentByParams(FirstName, LastName);
            if (studentByParams == null)
            {
                return NotFound();
            }
            return Ok(studentByParams);
        }
        [HttpGet("search1/{username}")]
        public async Task<ActionResult<StudentModel>> GetStudentByUsername([FromRoute] string username)
        {
            var studentByUserName = await _studentService.GetStudentByUsername(username);
            if (studentByUserName == null)
            {
                return NotFound();
            }
            return Ok(studentByUserName);
        }
        [HttpGet("search2/{email}")]
        public async Task<ActionResult<StudentModel>> GetStudentByEmail([FromRoute] string email)
        {
            var studentByUserName = await _studentService.GetStudentByEmail(email);
            if (studentByUserName == null)
            {
                return NotFound();
            }
            return Ok(studentByUserName);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddStudent([FromBody] StudentModel student, IPasswordService passwordService) 
        {
            if (student == null) { return BadRequest("Data Is Required"); }

            var added = await _studentService.AddStudent(student, passwordService);
            return CreatedAtAction(nameof(GetStudentByUsername), new {username = added.studentUserName},added);
        }

        [HttpDelete("delete")]// it doesnt need a from body
        public async Task<IActionResult> DeleteStudent([FromBody] StudentModel deleteReq, IPasswordService passwordService)
        {
            
            if (deleteReq == null) { return BadRequest("Null Entry"); }
            var deleteStats = await _studentService.DeleteStudent(deleteReq, passwordService);
            if (deleteStats.Success == false)
            {
                return BadRequest(DeleteStudent);
            }
           
            return NoContent();
        }

        [HttpPatch("edit/{userName}")] //this function has reflection study it well
        public async Task<IActionResult> EditStudent(string userName, [FromBody] updateStudentDTO edit)
        {
            var student = await _studentService.EditStudent(userName, edit);
            return Ok(student);
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO loginReq, IPasswordService passwordService)
        {
           var loginResponse = await _studentService.Login(loginReq, passwordService);
            if (loginResponse.Success == false )
            {
                return Unauthorized(loginResponse);
            }
           return Ok(loginResponse);
        }
    }
}
