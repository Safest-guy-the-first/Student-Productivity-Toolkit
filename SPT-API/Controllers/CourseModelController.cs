using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SPT_API.Services.CourseServices;

namespace SPT_API.Controllers
{
    public class CourseModelController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseModelController(ICourseService courseService)
        {
              _courseService = courseService;
        }
        [HttpGet("(student/{cuuid})")]
        public IActionResult GetAllCourses([FromQuery] string cuuid)
        {
            var courses = _courseService.GetAllCourses(cuuid);
            if (!courses.Any()) { return NotFound("No Courses Found for the student"); }
            return Ok(courses);
        }

        [HttpGet("{courseCode}")]
        public IActionResult GetCourseByCourseCode()
        {
            return null;
        } 
    }
}
