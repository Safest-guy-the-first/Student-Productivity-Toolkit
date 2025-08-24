using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPT_API.Data;
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
    }
}
