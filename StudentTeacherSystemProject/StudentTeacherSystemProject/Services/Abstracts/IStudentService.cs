using StudentSystem.Server.Model;

namespace StudentTeacherSystemProject.Services.Abstracts
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> GetByIdAsync(int id);

        Task AddAsync(Student entity);

        void Update(Student entity);
        Task AddStudentAsync(Student student);
    }
}
