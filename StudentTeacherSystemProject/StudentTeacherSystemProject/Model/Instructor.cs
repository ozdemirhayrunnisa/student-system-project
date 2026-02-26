using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentSystem.Server.Model
{

    [Table("Instructor")]
    public class Instructor
    {
        private int _instructorId;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _department;
        private string _password;
        [Key]

        public int Instructor_ID
        {
            get => _instructorId;
            set => _instructorId = value;
        }

        public string First_Name
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string Last_Name
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Department
        {
            get => _department;
            set => _department = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }
        [JsonIgnore]

        public virtual List<Course> Courses { get; set; } = new List<Course>();
    }
}
