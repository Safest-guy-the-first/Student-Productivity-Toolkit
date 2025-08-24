using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TestSPTAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            students();
        }
        private void students()
        {
            using (var client = new HttpClient())
            {
                try 
                {
                    var serialize = client.GetStringAsync("http://localhost:5119/spt/StudentModel").Result;
                    var deserialize = JsonConvert.DeserializeObject<List<StudentModel>>(serialize);
                    label1.Text = deserialize.FirstOrDefault().firstName;
                }
                catch { }
            }
        }
        public class StudentModel
        {
            [Key] public int _id; //primary key
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string department { get; set; }
            public string level { get; set; }
            public uint _numLevel { get; set; }
            public string studentLogin { get; set; } //temporary 
            [Required] public string uniqueUserId { get; set; }
        }
    }
}
