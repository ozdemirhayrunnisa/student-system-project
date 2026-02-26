using StudentSystem.Server.Model;

namespace StudentTeacherSystemProject.Repository.Abstracts
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();

        Task<Instructor> GetByIdAsync(int id);

        Task AddAsync(Instructor entity);

        Task UpdateAsync(Instructor entity);
    }
}
