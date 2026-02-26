using StudentSystem.Server.Model;

namespace StudentTeacherSystemProject.Services.Abstracts
{
    public interface ICourseService
    {
        Task<Course> GetByIdAsync(int id);

        Task<IEnumerable<Course>> GetAllAsync();

        Task AddCourseAsync(Course course);
    }
}
