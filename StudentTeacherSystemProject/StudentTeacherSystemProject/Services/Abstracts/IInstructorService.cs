using StudentSystem.Server.Model;

namespace StudentTeacherSystemProject.Services.Abstracts
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllAsync();

        Task<Instructor> GetByIdAsync(int id);

        Task AddAsync(Instructor entity);

        void Update(Instructor entity);
    }
}
