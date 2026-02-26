using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class InstructorService : IInstructorService
    {
        private readonly InstructorRepository _instructorRepository;
        public InstructorService(InstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync() => await _instructorRepository.GetAllAsync();

        public async Task<Instructor> GetByIdAsync(int id) => await _instructorRepository.GetByIdAsync(id);

        public async Task AddAsync(Instructor entity) => await _instructorRepository.AddAsync(entity);

        public void Update(Instructor entity) => _instructorRepository.UpdateAsync(entity);
    }
}
