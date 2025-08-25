using Microsoft.AspNetCore.Mvc;
using SPT_API.Data;
using SPT_API.Data.DTOs;
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
        [HttpGet("search/{userID}")]
        public ActionResult<StudentModel> GetStudentByUniqueID([FromRoute] string userID)
        {
            var studentByParams = _db.StudentTable.FirstOrDefault(s => s.uniqueUserId== userID);
            if (studentByParams == null)
            {
                return NotFound();
            }
            return Ok(studentByParams);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentModel student)
        {
            if (student == null) { return BadRequest("Data Is Required"); }
            student.uniqueUserId = Guid.NewGuid().ToString("N").Substring(0, 8);
            student.studentLogin =
               student.firstName.Substring(0, 2).ToLower() + student.lastName.Substring(0, 2).ToLower() + student.uniqueUserId.Substring(5, 2).ToLower();


            _db.StudentTable.Add(student);
            _db.SaveChanges();  
            return Ok(student); 
        }

        [HttpDelete("delete")]
        public IActionResult DeleteStudent([FromBody] DeleteUserDTO deleteReq)
        {
            if (deleteReq == null) { return BadRequest("Null Entry"); }
            var studentDelReq = _db.StudentTable.FirstOrDefault(s => s.firstName == deleteReq.firstName 
            && s.lastName == deleteReq.lastName
            && s.studentLogin == deleteReq.studentLogin);
            if (studentDelReq == null) { NotFound("Student not Found"); }

            _db.Remove(studentDelReq);
            _db.SaveChanges();
            return NoContent();
        }

        //add an edit funtion
        [HttpPatch("edit/{uniqueStudentID}")] //this function has refection study it well
        public ActionResult<StudentModel> EditStudent(string uniqueStudentID, [FromBody] updateStudentDTO edit)
        {
            var student = _db.StudentTable.FirstOrDefault(s=>s.uniqueUserId == uniqueStudentID);
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
