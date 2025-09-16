using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPT_API.Services.CourseServices;
using SPT_API.Services.GpaCalcServices;

namespace SPT_API.Controllers
{
    [Authorize]
    [Route("spt/g")]
    [ApiController]
    public class GpaController : ControllerBase
    {
        private readonly IGpaCalcService _GpaService;
        public GpaController(IGpaCalcService GpaService)
        {
            _GpaService = GpaService;
        }

        [HttpGet("/calculategpa")]
        public IActionResult CalcGPA() 
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(_cuuid) == true || string.IsNullOrEmpty(_cuuid) == true) { return NotFound(); }
            var gpa = _GpaService.CalculateGPA(_cuuid);
            return Ok(gpa);
        }

    }
}
