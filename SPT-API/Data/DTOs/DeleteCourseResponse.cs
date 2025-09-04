using Microsoft.AspNetCore.Components.Web;

namespace SPT_API.Data.DTOs
{
    public class DeleteCourseResponse
    {
        public bool success { get; set; } = false;
        public string? message { get; set; } = null;
    }
}
