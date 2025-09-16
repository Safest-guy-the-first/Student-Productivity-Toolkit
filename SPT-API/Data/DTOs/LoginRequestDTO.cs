using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace SPT_API.Data.DTOs
{
    public class LoginRequestDTO :IValidatableObject
    {
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The Username field must be between {2} and {1} characters long.")]
        public string? _UserName { get; set; } = null;
        
        [Required(ErrorMessage = "Password is required")]
        public string _Password { get; set; } = null;
       
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? _Email { get; set; } = null;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(_Email) && string.IsNullOrWhiteSpace(_UserName))
            {
                yield return new ValidationResult(
                    "Either Email or Username must be provided.",
                    new[] { nameof(_Email), nameof(_UserName) }
                );
            }
        }
    }
}
