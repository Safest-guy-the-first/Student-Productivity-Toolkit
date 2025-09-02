using Microsoft.AspNetCore.Mvc;
using SPT_API.Models;

namespace SPT_API.Services.CourseServices
{
    public interface ICourseService
    {
        IEnumerable<CourseModel> GetAllCourses(string _cuuid);
        CourseModel GetCourse(string courseCode);
    }
}
