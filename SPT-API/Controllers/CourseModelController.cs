using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPT_API.Services.CourseServices;

namespace SPT_API.Controllers
{
    
    [Route("spt_c/[controller]")]
    [ApiController]
    public class CourseModelController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseModelController(ICourseService courseService)
        {
              _courseService = courseService;
        }
        /*[Authorize]*/
        [HttpGet("courses")]
        public IActionResult GetAllCourses()
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name);
            if (_cuuid == null) { return Unauthorized(new{ Message = "Hello"}); }
            var courses = _courseService.GetAllCourses(_cuuid);
            if (!courses.Any()) { return NotFound(new { Message = "No Courses Found for the student"}); }
            return Ok(courses);
        }

        [HttpGet("courseCode")]
        public IActionResult GetCourseByCourseCode()
        {
            return null;
        } 
    }
}
