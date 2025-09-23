using Microsoft.EntityFrameworkCore;
using SPT_API.Data;
using SPT_API.Data.DTOs;
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

        public async Task<IEnumerable<CourseModel>> GetAllCourses(string _cuuid)
        {
            var courses = await _db.CourseTable.Where(c => c.cuuid == _cuuid).ToListAsync();
            return courses;
        }

        public async Task<CourseModel> GetCourse(string courseCode, string _cuuid)
        {
            var course = await _db.CourseTable.FirstOrDefaultAsync(c => c.cuuid == _cuuid && c.CourseCode == courseCode);
            return course;
        }
        public async Task<CourseModel> AddCourse(CourseModel course, string _cuuid)
        {
            course.cuuid = _cuuid;
            _db.CourseTable.AddAsync(course);
            _db.SaveChangesAsync();
            return course;
        }
        public async Task<DeleteCourseResponse> DeleteCourse(DeleteCourseDTO deleteReq, string _cuuid)
        {
            DeleteCourseResponse deleteCourseResponse = new DeleteCourseResponse();
            var course = await _db.CourseTable.FirstOrDefaultAsync(c => c.cuuid == _cuuid && c.CourseCode == deleteReq._CourseCode && c.CourseTitle == deleteReq.CourseDescript);
            if (course == null)
            {
                deleteCourseResponse.success = false;
                deleteCourseResponse.message = "This Course Doesn't Exist";
                return deleteCourseResponse;
            }

            _db.CourseTable.Remove(course);
            _db.SaveChangesAsync();
            deleteCourseResponse.success = true;
            deleteCourseResponse.message = "Succesfully Deleted";
            return deleteCourseResponse;
        }

        public async Task<CourseModel> EditCourse(EditCourseDTO edit, string _cuuid)
        {
            var course = await _db.CourseTable.FirstOrDefaultAsync(c => c.cuuid == _cuuid && (
            c.CourseCode == edit.CourseCode ||
            c.CourseTitle == edit.CourseTitle ||
            c.CourseUnit == edit.CourseUnit ||
            c.Grade == edit.Grade));
            if (course == null) { return null; }

            var courseType = typeof(CourseModel);
            var dtoType = typeof(EditCourseDTO);

            var properties = courseType.GetProperties();
            foreach (var dtoProperty in dtoType.GetProperties())
            { 
                var newValue = dtoProperty.GetValue(edit);
                if (newValue != null)
                {
                    var modelProp = courseType.GetProperty(dtoProperty.Name);
                    if (modelProp != null && modelProp.CanWrite)
                    {
                        modelProp.SetValue(course, newValue);
                    }

                }
            }

            _db.SaveChangesAsync();
            return course;
        }
    }
}
