using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class CourseService : ICourseService
    {
        private readonly CourseRepository _courseRepository;

        public CourseService(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> GetByIdAsync(int id) => await _courseRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Course>> GetAllAsync() => await _courseRepository.GetAllAsync();

        public async Task AddCourseAsync(Course course)
        {
            await _courseRepository.AddAsync(course); 
        }
    }
}
