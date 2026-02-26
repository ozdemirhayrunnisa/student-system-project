using StudentSystem.Server.Model;

namespace StudentTeacherSystemProject.Repository.Abstracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> GetByIdAsync(int id);

        Task AddAsync(Student entity);

        Task UpdateAsync(Student entity);
    }
}
