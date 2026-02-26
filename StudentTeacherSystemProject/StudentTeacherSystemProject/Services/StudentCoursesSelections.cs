using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Model;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class StudentCoursesSelectionsService : IStudentCoursesSelectionsService
    {
        private readonly StudentCoursesSelectionsRepository _studentCoursesSelectionsRepository;

        public StudentCoursesSelectionsService(StudentCoursesSelectionsRepository studentCoursesSelectionsRepository)
        {
            _studentCoursesSelectionsRepository = studentCoursesSelectionsRepository;
        }

        public async Task<IEnumerable<StudentCoursesSelections>> GetAllAsync() => await _studentCoursesSelectionsRepository.GetAllAsync();

        public async Task<StudentCoursesSelections> GetByIdAsync(int id) => await _studentCoursesSelectionsRepository.GetByIdAsync(id);

        public async Task AddAsync(StudentCoursesSelections entity) => await _studentCoursesSelectionsRepository.AddAsync(entity);

        public void Update(StudentCoursesSelections entity) => _studentCoursesSelectionsRepository.Update(entity);

    }
}
