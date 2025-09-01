
using System.ComponentModel.DataAnnotations;

namespace SPT_API.Models
{
    public class CourseModel
    {
        [Key]public int id { get; set; }
        public string? cuuid {  get; set; } = null;
        public string? CourseCode {  get; set; } = null;
        public string? CourseTitle { get; set; } = null;
        public uint? CourseUnit { get; set; } = null;
        public StudentModel? Student { get; set; }

    }
}
