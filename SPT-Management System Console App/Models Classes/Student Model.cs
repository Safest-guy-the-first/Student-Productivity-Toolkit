using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;
using SPT_Management_System_Console_App.Models_Classes;

namespace SPT_Management_System_Console_App
{
    class Student_Model
    {
        [Key]public int _id; //primary key
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public string department { get; set; }
        public string level { get; set; }
        public uint _numLevel { get; set; }
        public string studentLogin { get; set; } //temporary 
        [Required]public string uniqueUserId {get; set;} // what will log a user in to their information // foreign key to course
        
        public List<Course_Model> studentCourses { get; set; }
        public List<Result_Model> Results { get; set; }


        public Student_Model()
        {
          uniqueUserId = Guid.NewGuid().ToString("N")/*to remove dashes*/.Substring(0, 8);
        }

    }

}
