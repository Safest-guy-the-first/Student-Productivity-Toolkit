using System.ComponentModel.DataAnnotations;

namespace SPT_API.Models
{
    public class CourseModel
    {
        [Key]public int id { get; set; }

        public bool isSelected { get; set; } = false;
        
        public string? cuuid {  get; set; } = null;


        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression(@"^[A-Z]{3}\d{3}$", ErrorMessage = "{0} should be in the form 'AAA111'.")]
        public string? CourseCode {  get; set; } = null;
        
        
        [RegularExpression(@"^[a-zA-Z0-9\:\s]*$", ErrorMessage = "{0} cannot contain special characters.")]
        public string? CourseTitle { get; set; } = null;

        
        [Required]
        [RegularExpression(@"^[0-9]{1}$", ErrorMessage = "{0} must be a single digit 0-9")]
        public uint CourseUnit { get; set; } = 0;
        

        [Required]
        public char? Grade { get; set; } = 'S';

        public StudentModel? Student { get; set; } = null;

    }
}
