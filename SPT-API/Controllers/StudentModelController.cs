using Microsoft.AspNetCore.Mvc;
using SPT_API.Data;
using SPT_API.Data.DTOs;
using SPT_API.Migrations;
using SPT_API.Models;

namespace SPT_API.Controllers
{
    [Route("spt/[controller]")]
    [ApiController]
    public class StudentModelController : Controller
    {
        private SPT_APIDbContext _db;
        public StudentModelController(SPT_APIDbContext db) { _db = db; }

        [HttpGet]
        public ActionResult<List<StudentModel>> GetUser()
        {
            return Ok(_db.StudentTable.ToList());
        }
        [HttpGet("search")]
        public ActionResult<StudentModel> GetStudentByParams([FromQuery] string FirstName, [FromQuery] string LastName) 
        {
            var studentByParams = _db.StudentTable.FirstOrDefault(s => s.firstName == FirstName && s.lastName == LastName);
            if (studentByParams == null)
            {
                return NotFound();
            }
            return Ok(studentByParams);
        }
        [HttpGet("search/{username}")]
        public ActionResult<StudentModel> GetStudentByUsername([FromRoute] string username)
        {
            var studentByParams = _db.StudentTable.FirstOrDefault(s => s.uniqueUserId== username);
            if (studentByParams == null)
            {
                return NotFound();
            }
            return Ok(studentByParams);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentModel student) //i need this to return username
        {
            if (student == null) { return BadRequest("Data Is Required"); }
            student.uniqueUserId = Guid.NewGuid().ToString("N").Substring(0, 8);
            student.studentUserName =
               student.firstName.Substring(0, 2).ToLower() + student.lastName.Substring(0, 2).ToLower() + student.uniqueUserId.Substring(5, 2).ToLower();


            _db.StudentTable.Add(student);
            _db.SaveChanges();  
            return Ok(student.studentUserName); 
        }

        [HttpDelete("delete")]
        public IActionResult DeleteStudent([FromBody] DeleteUserDTO deleteReq)
        {
            if (deleteReq == null) { return BadRequest("Null Entry"); }
            var studentDelReq = _db.StudentTable.FirstOrDefault(s => s.firstName == deleteReq._firstName 
            && s.lastName == deleteReq._lastName
            && s.studentUserName == deleteReq._studentUsername
            && s.studentPassword == deleteReq._studentPassword);
            if (studentDelReq == null) { NotFound("Student not Found"); }

            _db.Remove(studentDelReq);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("edit/{userName}")] //this function has reflection study it well
        public ActionResult<StudentModel> EditStudent(string userName, [FromBody] updateStudentDTO edit)
        {
            var student = _db.StudentTable.FirstOrDefault(s=>s.studentUserName == userName);
            if (student == null) { NotFound("Student Doesn't Exist"); }

            var studentType = typeof(StudentModel);
            var dtoType = typeof(updateStudentDTO);

            var properties = typeof(StudentModel).GetProperties();
            foreach(var dtoProperty in dtoType.GetProperties())
            {
                var newValue = dtoProperty.GetValue(edit);
                if (newValue != null)
                {
                    var modelProp = studentType.GetProperty(dtoProperty.Name);
                    if (modelProp != null && modelProp.CanWrite)
                    {
                        modelProp.SetValue(student, newValue);
                    }
                    
                }
            }
            _db.SaveChanges();
            return Ok(student);
        }
    }
}
