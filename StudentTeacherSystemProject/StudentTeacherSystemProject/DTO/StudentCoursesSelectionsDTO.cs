namespace StudentTeacherSystemProject.DTO
{
    public class StudentCoursesSelectionsDTO
    {
        public int? SelectedCourse_ID { get; set; }
        public int? Course_ID { get; set; }
        public int? Student_ID { get; set; }
        public int? Instructor_ID { get; set; }
        public string? Course_Name { get; set; }
        public string? Student_Full_Name { get; set; }
        public string? Instructor_Full_Name { get; set; }
        public bool? IsApproved { get; set; }
    }
}
