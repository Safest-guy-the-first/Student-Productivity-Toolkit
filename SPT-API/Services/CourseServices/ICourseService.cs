using SPT_API.Data.DTOs;
using SPT_API.Models;

namespace SPT_API.Services.CourseServices
{
    public interface ICourseService
    {
        IEnumerable<CourseModel> GetAllCourses(string _cuuid);
        CourseModel GetCourse(string courseCode, string _cuuid);
        CourseModel AddCourse(CourseModel course, string _cuuid);
        DeleteCourseResponse DeleteCourse(DeleteCourseDTO deleteReq, string _cuuid);
        CourseModel EditCourse(EditCourseDTO edit, string _cuuid);
    }
}
