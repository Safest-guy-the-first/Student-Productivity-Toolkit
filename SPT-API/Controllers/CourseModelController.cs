using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPT_API.Services.CourseServices;
using SPT_API.Models;
using SPT_API.Data.DTOs;

namespace SPT_API.Controllers
{
    [Authorize]
    [Route("spt/c/[controller]")]
    [ApiController]
    public class CourseModelController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseModelController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet("courses")]
        public IActionResult GetAllCourses()
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name); // <-- use NameIdentifier
            if (string.IsNullOrEmpty(_cuuid))
                return Unauthorized(new { Message = "User not authenticated or token missing" });

            var courses = _courseService.GetAllCourses(_cuuid);
            if (!courses.Any())
                return NotFound(new { Message = "No Courses Found for the student" });

            return Ok(courses);
        }

        [HttpGet("courses/{courseCode}")]
        public IActionResult GetCourseByCourseCode([FromRoute] string courseCode)
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(_cuuid))
                return Unauthorized(new { Message = "User not authenticated or token missing" });
            var course = _courseService.GetCourse(courseCode, _cuuid);
            return Ok(course);
        }
        [HttpPost("addcourse")]
        public IActionResult AddCourse([FromBody] CourseModel course)
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(_cuuid))
                return Unauthorized(new { Message = "User not authenticated or token missing" });
            var addedCourse = _courseService.AddCourse(course,_cuuid);
            return CreatedAtAction(nameof(GetCourseByCourseCode), new {courseCode = addedCourse.CourseCode}, addedCourse);
        }
        [HttpDelete("delete")]
        public IActionResult DeleteCourse([FromBody] DeleteCourseDTO deleteReq)
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(_cuuid))
                return Unauthorized(new { Message = "User not authenticated or token missing" });
           
            var delCourse = _courseService.DeleteCourse(deleteReq, _cuuid);
            if(delCourse.success == false) { return BadRequest(delCourse);}
            return NoContent();
        }

        [HttpPatch("edit")]
        public IActionResult EditCourse([FromBody] EditCourseDTO editReq)
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(_cuuid))
                return Unauthorized(new { Message = "User not authenticated or token missing" });
            var editedCourse = _courseService.EditCourse(editReq, _cuuid);
            if(editedCourse == null)
            {
                return BadRequest(editReq);
            }
            return Ok(editedCourse);
        }
    }
}
