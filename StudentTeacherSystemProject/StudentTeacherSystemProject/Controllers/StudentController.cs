using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Model;
using StudentTeacherSystemProject.DTO;
using StudentTeacherSystemProject.Model;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IStudentCoursesSelectionsService _studentCoursesSelectionsService;

        public StudentController(IStudentService studentService, ICourseService courseService, IStudentCoursesSelectionsService studentCoursesSelectionsService)
        {
            _studentService = studentService;
            _courseService = courseService;
            _studentCoursesSelectionsService = studentCoursesSelectionsService;
        }

        // Tüm öğrencileri listeleme (GET)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        // Öğrenci ekleme (POST)
        [HttpPost("students")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (string.IsNullOrWhiteSpace(student.First_Name) || string.IsNullOrWhiteSpace(student.Last_Name) || string.IsNullOrWhiteSpace(student.Email) || string.IsNullOrWhiteSpace(student.Password) || string.IsNullOrWhiteSpace(student.Password))
                {
                    return BadRequest("Öğrenci adı, soyadı, email, şifre ve bölüm alanları boş olamaz.");
                }


                await _studentService.AddAsync(student);
                return Ok("Öğrenci başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        // Öğrenci güncelleme (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student updatedStudent)
        {
            if (id != updatedStudent.Student_ID)
            {
                return BadRequest("ID uyuşmuyor.");
            }

            var existingStudent = await _studentService.GetByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"ID'si {id} olan öğrenci bulunamadı.");
            }

            // Öğrenci bilgilerini güncelleme
            existingStudent.First_Name = updatedStudent.First_Name;
            existingStudent.Last_Name = updatedStudent.Last_Name;
            existingStudent.Email = updatedStudent.Email;
            existingStudent.Password = updatedStudent.Password;
            existingStudent.Major = updatedStudent.Major;
            existingStudent.Courses_Selected = updatedStudent.Courses_Selected;

            try
            {
                _studentService.Update(existingStudent);
                return Ok(existingStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        // Öğrenciye ders ekleme (POST)
        [HttpPost("{id}/courses")]
        public async Task<IActionResult> SelectCourse(int id, [FromBody] int courseId)
        {
            var student = await _studentService.GetByIdAsync(id);
            var course = await _courseService.GetByIdAsync(courseId);

            if (student == null)
            {
                return NotFound($"ID'si {id} olan öğrenci bulunamadı.");
            }

            if (course == null)
            {
                return NotFound($"ID'si {id} olan kurs bulunamadı.");
            }

            var newStudentSelectCourse = new StudentCoursesSelections();
            newStudentSelectCourse.Course_ID = courseId;
            newStudentSelectCourse.SelectionDate = DateTime.Now;
            newStudentSelectCourse.Student_ID = id;
            newStudentSelectCourse.Student = student;
            newStudentSelectCourse.Course = course;

            try
            {
                await _studentCoursesSelectionsService.AddAsync(newStudentSelectCourse);
                return Ok(newStudentSelectCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        //Öğrencinin seçmiş olduğu tüm kursları getir
        [HttpGet("StudentCourses/{id}")]
        public async Task<IActionResult> SelectedStudentCourse(int id = 0)
        {
            try
            {
                var student = _studentService.GetByIdAsync(id);
                if (student != null && student.Result.Courses_Selected != null && student.Result.Courses_Selected != "")
                {
                    var selectedCourse = student.Result.Courses_Selected.Split(",").Select(x => Int32.Parse(x)).ToList();
                    var allCourse = _courseService.GetAllAsync().Result
                        .Where(x => selectedCourse.Contains(x.Course_ID))
                    .Select(x => new CourseDto
                    {
                        Course_ID = x.Course_ID,
                        Course_Name = x.Course_Name,
                        Credits = x.Credits,
                        Instructor_Full_Name = x.Instructor.First_Name + " " + x.Instructor.Last_Name
                    })
                        .ToList();
                    return Ok(allCourse);
                }
                return BadRequest("Kullanıcı Bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }


        // Öğrencinin seçmediği tüm kurslraı getir
        [HttpGet("StudentUnselectedCourses/{id}")]
        public async Task<IActionResult> StudentUnselectedCourses(int id)
        {
            try
            {
                var student = _studentService.GetAllAsync().Result.FirstOrDefault(x => x.Student_ID == id);
                if (student != null)
                {
                    if (student.Courses_Selected != null && student.Courses_Selected != "")
                    {
                        var selectedCourse = student.Courses_Selected.Split(",").Select(x => Int32.Parse(x)).ToList();
                        var studentApproveWaitCourseIDs = _studentCoursesSelectionsService.GetAllAsync().Result.Where(x => x.Student_ID == id && (!x.Is_Approved.HasValue || (x.Is_Approved.HasValue && x.Is_Approved.Value))).ToList().Select(x => x.Course_ID).ToList();

                        selectedCourse.AddRange(studentApproveWaitCourseIDs);

                        var allCourse = _courseService.GetAllAsync().Result
                            .Where(x => !selectedCourse.Contains(x.Course_ID))
                        .Select(x => new CourseDto
                        {
                            Course_ID = x.Course_ID,
                            Course_Name = x.Course_Name,
                            Credits = x.Credits,
                            Instructor_Full_Name = x.Instructor.First_Name + " " + x.Instructor.Last_Name
                        })
                            .ToList();
                        return Ok(allCourse);
                    }
                    else
                    {
                        var studentApproveWaitCourseIDs = _studentCoursesSelectionsService.GetAllAsync().Result.Where(x => x.Student_ID == id && (!x.Is_Approved.HasValue || (x.Is_Approved.HasValue && x.Is_Approved.Value))).ToList().Select(x => x.Course_ID).ToList();

                        var allCourse = _courseService.GetAllAsync().Result;
                        if (studentApproveWaitCourseIDs.Any())
                        {
                            allCourse = _courseService.GetAllAsync().Result.Where(x => !studentApproveWaitCourseIDs.Contains(x.Course_ID));
                        }

                        var studenNotSelectCourses =allCourse
                        .Select(x => new CourseDto
                        {
                            Course_ID = x.Course_ID,
                            Course_Name = x.Course_Name,
                            Credits = x.Credits,
                            Instructor_Full_Name = x.Instructor.First_Name + " " + x.Instructor.Last_Name
                        })
                            .ToList();
                        return Ok(studenNotSelectCourses);
                    }
                }

                return BadRequest("Kullanıcı Bulunamadı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            return Ok(student);
        }

        [HttpPut("UpdateStudentCoursesSelections/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentCoursesSelectionsDTO studentCoursesSelectionsDTO)
        {

            var selectedCourse = await _studentCoursesSelectionsService.GetByIdAsync(id);

            if (selectedCourse == null)
            {
                return NotFound($"ID'si {id} olan seçilmiş kurs bulunamadı.");
            }

            selectedCourse.Is_Approved = studentCoursesSelectionsDTO.IsApproved;

            try
            {
                _studentCoursesSelectionsService.Update(selectedCourse);
                if (selectedCourse.Is_Approved.HasValue && selectedCourse.Is_Approved.Value)
                {
                    var selectedStudent = _studentService.GetByIdAsync(selectedCourse.Student_ID).Result;
                    if (selectedStudent != null)
                    {
                        if(selectedStudent.Courses_Selected != null && selectedStudent.Courses_Selected != "")
                        {
                            var studentCourses = selectedStudent.Courses_Selected.Split(",").ToList();
                            studentCourses.Add(selectedCourse.Course_ID.ToString());

                            selectedStudent.Courses_Selected = string.Join(",", studentCourses);
                        }
                        else
                        {
                            selectedStudent.Courses_Selected = selectedCourse.Course_ID.ToString();
                        }
                        

                        _studentService.Update(selectedStudent);
                    }
                }
                return Ok(selectedCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
