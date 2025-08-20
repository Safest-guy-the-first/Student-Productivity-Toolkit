using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SPT_Management_System_Console_App.Models_Classes
{
    class Course_Model
    {
        [Key]public int _id;
        [Required]public string _CuniqueUserId { get; set; }
        public string courseCode {  get; set; }
        public string courseName { get; set; }
        public uint courseUnit { get; set; }
        public Student_Model Student { get; set; }
    }
}
