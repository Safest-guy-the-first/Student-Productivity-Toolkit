using System.ComponentModel.DataAnnotations;

namespace SPT_API.Models
{
    public class StudentModel
    {
        [Key] public int id; //primary key
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string department { get; set; }
        public string level { get; set; }
        public string? studentUserName { get; set; } = null; 
        public string? studentPassword { get; set; } = null ;
        public string email { get; set; } = "m";
        [Required] public string? uniqueUserId { get; set; } = "m";
         public List<CourseModel>? Courses { get; set; }
    }
}
