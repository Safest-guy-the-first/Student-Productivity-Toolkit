using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SPT_API.Models;

namespace BlazorSPT.Services
{
    public class CourseService
    {
        public StudentModel? currentUser {private get;  set; }
        private readonly HttpClient _httpClient;
        private string? _token;
        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<CourseModel?>> GetCourses()
        {
            var courses = await _httpClient.GetFromJsonAsync<List<CourseModel>>("/spt/c/CourseModel/courses");
            if (courses == null) { return null; }
            return courses;
        }
        public async Task<List<CourseModel>> SearchCourses(string searchTerm)
        {
            var allCourses = await GetCourses();
            if (string.IsNullOrWhiteSpace(searchTerm))
                return allCourses;
            return allCourses
            .Where(c =>
                (c.CourseCode?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (c.CourseTitle?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
            )
            .ToList();
        }
        public async void AddCourse(CourseModel addCourse)
        {
            try
            {
                var request = await _httpClient.PostAsJsonAsync("/spt/c/CourseModel/addcourse", addCourse);

                request.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            
            
        }

    }
}
