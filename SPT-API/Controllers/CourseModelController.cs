using Microsoft.AspNetCore.Mvc;

namespace SPT_API.Controllers
{
    public class CourseModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
