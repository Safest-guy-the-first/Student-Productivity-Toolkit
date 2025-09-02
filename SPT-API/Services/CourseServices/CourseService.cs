using Microsoft.AspNetCore.Mvc;
using SPT_API.Data;
using SPT_API.Models;

namespace SPT_API.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private readonly SPT_APIDbContext _db;

        public CourseService(SPT_APIDbContext db)
        {
            _db = db;
        }
        
        public IEnumerable<CourseModel> GetAllCourses(string _cuuid)
        {
            var course = _db.CourseTable.Where(c=>c.cuuid == _cuuid).ToList();
            return course;
        }
   
        public CourseModel GetCourse([FromQuery] string courseCode)
        {
            return null;
        }
    }
}
