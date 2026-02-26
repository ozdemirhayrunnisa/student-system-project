using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Repository
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetAllAsync() => await _studentRepository.GetAllAsync();

        public async Task<Student> GetByIdAsync(int id) => await _studentRepository.GetByIdAsync(id);

        public async Task AddAsync(Student entity) => await _studentRepository.AddAsync(entity);

        public async void Update(Student entity) =>  _studentRepository.UpdateAsync(entity);
        public async Task AddStudentAsync(Student student) // Yeni metot
        {
            await _studentRepository.AddAsync(student); // Öğrenciyi veritabanına ekleme
        }

    }
}
