using System.Security.Cryptography.Pkcs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SPT_API.Data;
using SPT_API.Data.DTOs;
using SPT_API.Migrations;
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
        public IActionResult GetStudents()
        {
            var allStudents = _studentService.GetAllStudents();
            return Ok(allStudents);
        }

        [HttpGet("search")]
        public IActionResult GetStudentByParams([FromQuery] string FirstName, [FromQuery] string LastName)
        {
            var studentByParams = _studentService.GetStudentByParams(FirstName, LastName);
            if (studentByParams == null)
            {
                return NotFound();
            }
            return Ok(studentByParams);
        }
        [HttpGet("search/{username}")]
        public ActionResult<StudentModel> GetStudentByUsername([FromRoute] string username)
        {
            var studentByUserName = _studentService.GetStudentByUsername;
            if (studentByUserName == null)
            {
                return NotFound();
            }
            return Ok(studentByUserName);
        }

        [HttpPost("create")]
        public IActionResult AddStudent([FromBody] StudentModel student, IPasswordService passwordService) 
        {
            if (student == null) { return BadRequest("Data Is Required"); }

            var added = _studentService.AddStudent(student, passwordService);
            return CreatedAtAction(nameof(GetStudentByUsername), new {username = added.studentUserName},added);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteStudent([FromBody] DeleteUserDTO deleteReq, IPasswordService passwordService)
        {
            
            if (deleteReq == null) { return BadRequest("Null Entry"); }
            var deleteStats = _studentService.DeleteStudent(deleteReq, passwordService);
            if (deleteStats.Success == false)
            {
                return BadRequest(DeleteStudent);
            }
           
            return NoContent();
        }

        [HttpPatch("edit/{userName}")] //this function has reflection study it well
        public IActionResult EditStudent(string userName, [FromBody] updateStudentDTO edit)
        {
            var student = _studentService.EditStudent(userName, edit);
            return Ok(student);
        }
        [HttpPost("login")]
        public ActionResult<LoginResponseDTO> Login([FromBody] LoginRequestDTO loginReq, IPasswordService passwordService)
        {
           var loginResponse = _studentService.Login(loginReq, passwordService);
            if (loginResponse.Success == false )
            {
                return Unauthorized(loginResponse);
            }
           return Ok(loginResponse);
        }
    }
}
