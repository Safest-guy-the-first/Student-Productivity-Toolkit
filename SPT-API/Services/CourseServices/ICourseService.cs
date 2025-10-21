using SPT_API.Data.DTOs;
using SPT_API.Models;

namespace SPT_API.Services.CourseServices
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetAllCourses(string _cuuid);
        Task<CourseModel> GetCourse(string courseCode, string _cuuid);
        Task<CourseModel> AddCourse(CourseModel course, string _cuuid);
        Task DeleteCourse(string _cuuid, string CourseCodetoDel);
        Task<CourseModel> EditCourse(EditCourseDTO edit, string _cuuid);
    }
}
