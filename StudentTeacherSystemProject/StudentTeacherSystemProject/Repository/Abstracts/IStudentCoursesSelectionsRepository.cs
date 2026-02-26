using StudentTeacherSystemProject.Model;

namespace StudentTeacherSystemProject.Repository.Abstracts
{
    public interface IStudentCoursesSelectionsRepository
    {
        Task<StudentCoursesSelections> GetByIdAsync(int id);
        Task<IEnumerable<StudentCoursesSelections>> GetAllAsync();
        Task AddAsync(StudentCoursesSelections entity);
        void Update(StudentCoursesSelections entity);
    }
}
