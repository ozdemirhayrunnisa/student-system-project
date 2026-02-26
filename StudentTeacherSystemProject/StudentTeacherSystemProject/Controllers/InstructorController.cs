using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.Services.Abstracts;
using StudentTeacherSystemProject.Helper; // ValidationHelper sınıfını dahil ettik
using StudentTeacherSystemProject.DTO;

namespace StudentTeacherSystemProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;

        public InstructorController(IInstructorService instructorService, IStudentService studentService, ICourseService courseService)
        {
            _instructorService = instructorService;
            _courseService = courseService;
            _studentService = studentService;
        }

        // Tüm akademisyenleri listeleme
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instructors = await _instructorService.GetAllAsync();
            return Ok(instructors);
        }

        // Yeni akademisyen ekleme
        [HttpPost]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            // E-posta formatı kontrolü
            if (!ValidationHelper.IsValidEmail(instructor.Email))
            {
                return BadRequest("Geçersiz e-posta formatı.");
            }

            // Ad ve soyad kontrolü
            if (!ValidationHelper.IsValidName(instructor.First_Name) || !ValidationHelper.IsValidName(instructor.Last_Name))
            {
                return BadRequest("Ad ve soyad 2 harften fazla ve 100 harften az olmalı.");
            }

            // Akademisyen ekleme işlemi
            await _instructorService.AddAsync(instructor);
            return CreatedAtAction(nameof(GetAll), new { id = instructor.Instructor_ID }, instructor);
        }

        // Akademisyen güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Instructor updatedInstructor)
        {
            // ID doğrulaması
            if (id != updatedInstructor.Instructor_ID)
            {
                return BadRequest("ID uyuşmuyor.");
            }

            // Veritabanında akademisyen var mı kontrol et
            var existingInstructor = await _instructorService.GetByIdAsync(id);
            if (existingInstructor == null)
            {
                return NotFound($"ID'si {id} olan akademisyen bulunamadı.");
            }

            // E-posta formatı kontrolü
            if (!ValidationHelper.IsValidEmail(updatedInstructor.Email))
            {
                return BadRequest("Geçersiz e-posta formatı.");
            }

            // Ad ve soyad kontrolü
            if (!ValidationHelper.IsValidName(updatedInstructor.First_Name) || !ValidationHelper.IsValidName(updatedInstructor.Last_Name))
            {
                return BadRequest("Ad ve soyad 2 harften fazla ve 100 harften az olmalı.");
            }

            // Akademisyen bilgilerini güncelleme
            existingInstructor.First_Name = updatedInstructor.First_Name;
            existingInstructor.Last_Name = updatedInstructor.Last_Name;
            existingInstructor.Email = updatedInstructor.Email;
            existingInstructor.Department = updatedInstructor.Department;
            existingInstructor.Password = updatedInstructor.Password;

            try
            {
                _instructorService.Update(existingInstructor);
                return Ok(existingInstructor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("GetInctructorById/{id}")]
        public async Task<IActionResult> GetInctructorById(int id)
        {
            var student = await _instructorService.GetByIdAsync(id);
            return Ok(student);
        }

        [HttpGet("GetInstructorCourses/{id}")]
        public async Task<IActionResult> GetInstructorCourses(int id)
        {
            var courses = _courseService.GetAllAsync().Result.Where(x => x.Instructor_ID == id)
                .Select(x => new CourseDto
                {
                    Course_ID = x.Course_ID,
                    Course_Name = x.Course_Name,
                    Credits = x.Credits,
                    Instructor_Full_Name = x.Instructor.First_Name + " " + x.Instructor.Last_Name
                }).ToList();
            return Ok(courses);
        }

        [HttpGet("GetInstructorStudents/{id}")]
        public async Task<IActionResult> GetInstructorStudents(int id)
        {
            var courseIds = _courseService.GetAllAsync().Result.Where(x => x.Instructor_ID == id).Select(x => x.Course_ID).ToList();
            var instructorStudents = new List<Student>();

            if (courseIds.Any())
            {
                var allStudents = _studentService.GetAllAsync().Result.Where(x => x.Courses_Selected != null).ToList();

                var studentSelectedCourses = allStudents.Select(x => new
                {
                    Student_ID = x.Student_ID,
                    Courses_IDs = x.Courses_Selected.Split(",").Select(a => Int32.Parse(a))
                }).ToList();
                var filteredStudents = studentSelectedCourses.Where(student => student.Courses_IDs.Any(courseId => courseIds.Contains(courseId))).Select(x => x.Student_ID).ToList();

                instructorStudents = allStudents.Where(x => filteredStudents.Contains(x.Student_ID)).ToList();
            }

            return Ok(instructorStudents);
        }
    }
}
