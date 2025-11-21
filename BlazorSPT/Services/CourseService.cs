using SPT_API.Models;

namespace BlazorSPT.Services
{
    public class CourseService
    {
        public StudentModel? currentUser {private get;  set; }
        private readonly HttpClient _httpClient;
        
        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<List<CourseModel?>> GetCourses()
        {
            var courses = new List<CourseModel>();
            try
            {
                courses = await _httpClient.GetFromJsonAsync<List<CourseModel>>("/spt/c/CourseModel/courses");
            }
            catch (Exception ex)
            {
                if (courses == null) { courses = new(); }
            }
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
        public async Task AddCourse(CourseModel addCourse)
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

        public async Task DeleteCourse(string CourseCodeToDel)
        {
            try
            {
                var deleteRequest = await _httpClient.DeleteAsync($"/spt/c/CourseModel/delete/{CourseCodeToDel}");

                deleteRequest.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blazor SPT Delete Course Error: {ex.Message}");
            }
        }

    }
}
