using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace SPT_Management_System_Console_App.Models_Classes
{
    class Result_Model 
    {
        [Key] public int _id;
        public string _RuniqueUserId { get; set; }
        public string courseCode { get; set; }
        public uint courseUnit { get; set; }
        public char grade {  get; set; }
        public Student_Model Student { get; set; }
        public List<Course_Model> studentCourses { get; set; }

    }
}
