namespace StudentTeacherSystemProject.DTO
{
    public class CourseDto
    {
        public int? Course_ID { get; set; }
        public string Course_Name { get; set; }
        public int Credits { get; set; }
        public string? Instructor_Full_Name { get; set; }
        public int Instructor_ID { get; set; }
    }
}