using StudentTeacherSystemProject.Model;

namespace StudentTeacherSystemProject.Services.Abstracts
{
    public interface IStudentCoursesSelectionsService
    {
        Task<IEnumerable<StudentCoursesSelections>> GetAllAsync();

        Task<StudentCoursesSelections> GetByIdAsync(int id);

        Task AddAsync(StudentCoursesSelections entity);

        void Update(StudentCoursesSelections entity);
    }
}
