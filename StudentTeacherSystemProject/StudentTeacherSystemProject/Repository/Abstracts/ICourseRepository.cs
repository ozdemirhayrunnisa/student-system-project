using StudentSystem.Server.Model;

namespace StudentTeacherSystemProject.Repository.Abstracts
{
    public interface ICourseRepository
    {
        Task<Course> GetByIdAsync(int id);

        Task<IEnumerable<Course>> GetAllAsync();

        Task AddAsync(Course entity);
    }
}
