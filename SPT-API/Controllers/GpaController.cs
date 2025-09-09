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

        [HttpGet("see")]
        public IActionResult see()
        {
            var _cuuid = User.FindFirstValue(ClaimTypes.Name);
            var see = _GpaService.coursesAndGrades(_cuuid);
            if (see == null || !see.Any()) { return NotFound(); }
            Console.WriteLine(see[1].Item1);
            return Ok(see); //works well but it doesnt seriaize properly

        }
    }
}
