using StudentSystem.Server.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTeacherSystemProject.Model
{
    [Table("StudentCoursesSelections")]
    public class StudentCoursesSelections
    {
        private int _selectionId;
        private int _courseId;
        private int _studentId;
        private bool? _isApproved;
        private DateTime _selectionDate;

        [Key]

        public int Selection_ID
        {
            get => _selectionId; // get { return _courseId; }
            set => _selectionId = value; //set { _courseId = value; }
        }

        public int Course_ID
        {
            get => _courseId; // get { return _courseId; }
            set => _courseId = value; //set { _courseId = value; }
        }

        public int Student_ID
        {
            get => _studentId; // get { return _courseId; }
            set => _studentId = value; //set { _courseId = value; }
        }

        public bool? Is_Approved
        {
            get => _isApproved;
            set => _isApproved = value;
        }

        public DateTime SelectionDate
        {
            get => _selectionDate;
            set => _selectionDate = value;
        }


        public virtual Course Course { get; set; } = new Course();
        public virtual Student Student { get; set; } = new Student();
    }
}
