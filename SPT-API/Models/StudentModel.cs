using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace SPT_API.Models
{
    public class StudentModel :IValidatableObject
    {
        [Key] public int id; //primary key
        [Required(ErrorMessage ="{0} is required")]
        public string firstName { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        public string lastName { get; set; }
        
        [Required(ErrorMessage = "{0} is required")]
        public string department { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string level { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} field must be between {2} and {1} characters")]
        public string? studentUserName { get; set; } = null;


        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} field must be between {2} and {1} characters")]
        public string? studentPassword { get; set; } = null ;
        
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string? email { get; set; } =null;
        
        public string? gpa { get; set; } = null;
        
        public string? cgpa { get; set; } = null;
        
        [Required] public string? uniqueUserId { get; set; } = "m";
         public List<CourseModel>? Courses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(studentUserName))
            {
                yield return new ValidationResult(
                    "Either Email or Username must be provided.",
                    new[] { nameof(email), nameof(studentUserName) }
                );
            }
        }
    }
}
