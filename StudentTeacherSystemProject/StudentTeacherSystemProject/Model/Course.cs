using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StudentTeacherSystemProject.Model;
using System.Text.Json.Serialization;

namespace StudentSystem.Server.Model
{
    [Table("Course")]
    public class Course
    {
  
        private int _courseId;
        private string _courseName;
        private int _credits;
        private int _instructorId;
        [Key]
        public int Course_ID
        {
            get => _courseId; // get { return _courseId; }
            set => _courseId = value; //set { _courseId = value; }
        }

        [Required(ErrorMessage = "Kurs adı zorunludur.")]
        public string Course_Name
        {
            get => _courseName;
            set => _courseName = value;
        }

        public int Credits
        {
            get => _credits;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Kredi negatif olamaz.");
                }
                _credits = value;
            }
        }

        public int Instructor_ID
        {
            get => _instructorId;
            set => _instructorId = value;
        }

        public virtual Instructor Instructor { get; set; } = new Instructor();
        [JsonIgnore]
        public virtual List<StudentCoursesSelections> StudentCoursesSelection { get; set; } = new List<StudentCoursesSelections>();

    }
}
