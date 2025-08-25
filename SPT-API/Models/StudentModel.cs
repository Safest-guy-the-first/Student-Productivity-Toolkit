using System.ComponentModel.DataAnnotations;

namespace SPT_API.Models
{
    public class StudentModel
    {
        [Key] public int _id; //primary key
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string department { get; set; }
        public string level { get; set; }
        public uint _numLevel { get; set; }
        public string studentLogin { get; set; } = "m"; // need to encrypt
        public string email { get; set; } = "m";
        [Required] public string uniqueUserId { get; set; } = "m";
    }
}
