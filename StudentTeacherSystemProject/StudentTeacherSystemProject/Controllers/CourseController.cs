using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.DTO;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        public CourseController(ICourseService courseService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _instructorService = instructorService;
        }

        // Tüm dersleri listeleme (GET)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = _courseService.GetAllAsync().Result.ToList()
                .Select(x => new CourseDto
                {
                    Course_ID = x.Course_ID,
                    Course_Name = x.Course_Name,
                    Credits = x.Credits,
                    Instructor_Full_Name = x.Instructor.First_Name + " " + x.Instructor.Last_Name
                }).ToList();
            return Ok(courses);
        }

        // Belirli bir dersin detaylarını getirme (GET)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound($"ID'si {id} olan ders bulunamadı.");
            }
            return Ok(course);
        }

        // Ders ekleme (POST)
        [HttpPost("courses")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDto course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Kurs ekleme işlemi

                if(course.Credits <= 0)
                {
                    return BadRequest("Kurs kredisi minimum 1 olmalıdır");
                }

                var newCourse = new Course();
                newCourse.Credits = course.Credits; 
                newCourse.Course_Name = course.Course_Name;
                newCourse.Instructor_ID = course.Instructor_ID;

                var instructor = _instructorService.GetByIdAsync(course.Instructor_ID).Result;
                newCourse.Instructor = instructor;


                await _courseService.AddCourseAsync(newCourse);
                return Ok("Kurs başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }
    }
}
