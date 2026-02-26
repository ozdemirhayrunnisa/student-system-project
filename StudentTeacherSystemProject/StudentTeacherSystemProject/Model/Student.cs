using StudentTeacherSystemProject.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentSystem.Server.Model
{
    [Table("Student")] 
    public class Student
    {

        //alan isimleri
        private int _studentId;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _major;
        private string? _coursesSelected;

        //özellik isimleri
        [Key]
        public int Student_ID
        {
            get => _studentId;
            set => _studentId = value;
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

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string Major
        {
            get => _major;
            set => _major = value;
        }

        public string? Courses_Selected
        {
            get => _coursesSelected;
            set => _coursesSelected = value;
        }

        [JsonIgnore]
        public virtual List<StudentCoursesSelections> StudentCoursesSelection { get; set; } = new List<StudentCoursesSelections>();

    }
}