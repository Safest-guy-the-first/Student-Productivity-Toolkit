using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<CourseModel> GetAllCourses(string _cuuid)
        {
            var courses = _db.CourseTable.Where(c => c.cuuid == _cuuid).ToList();
            return courses;
        }

        public CourseModel GetCourse(string courseCode, string _cuuid)
        {
            var course = _db.CourseTable.FirstOrDefault(c => c.cuuid == _cuuid && c.CourseCode == courseCode);
            return course;
        }
        public CourseModel AddCourse(CourseModel course, string _cuuid)
        {
            course.cuuid = _cuuid;
            _db.CourseTable.Add(course);
            _db.SaveChanges();
            return course;
        }
        public DeleteCourseResponse DeleteCourse(DeleteCourseDTO deleteReq, string _cuuid)
        {
            DeleteCourseResponse deleteCourseResponse = new DeleteCourseResponse();
            var course = _db.CourseTable.FirstOrDefault(c => c.cuuid == _cuuid && c.CourseCode == deleteReq._CourseCode && c.CourseTitle == deleteReq.CourseDescript);
            if (course == null)
            {
                deleteCourseResponse.success = false;
                deleteCourseResponse.message = "This Course Doesn't Exist";
                return deleteCourseResponse;
            }

            _db.CourseTable.Remove(course);
            _db.SaveChanges();
            deleteCourseResponse.success = true;
            deleteCourseResponse.message = "Succesfully Deleted";
            return deleteCourseResponse;
        }

        public CourseModel EditCourse(EditCourseDTO edit, string _cuuid)
        {
            var course = _db.CourseTable.FirstOrDefault(c => c.cuuid == _cuuid && (
            c.CourseCode == edit.CourseCode ||
            c.CourseTitle == edit.CourseTitle ||
            c.CourseUnit == edit.CourseUnit));
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

            _db.SaveChanges();
            return course;
        }
    }
}
