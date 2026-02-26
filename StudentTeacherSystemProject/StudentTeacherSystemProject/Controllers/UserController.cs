using Microsoft.AspNetCore.Mvc;
using StudentTeacherSystemProject.DTO;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Controllers
{
    public class UserController : Controller
    {

        private readonly IInstructorService _instructorService;
        private readonly IStudentService _studentService;
        private readonly IJwtService _jwtService;

        public UserController(IInstructorService instructorService, IJwtService jwtService, IStudentService studentService)
        {
            _instructorService = instructorService;
            _jwtService = jwtService;
            _studentService = studentService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Email ve Şifre gereklidir.");
            }

            var user = new LoginUser();

            if (model.Role == "Admin")
            {
                if (User.Email == model.Email && User.Password == model.Password)
                {
                    user.Email = User.Email;
                    user.Name = "Admin";
                    user.ID = 0;
                    user.Role = "Admin";
                }
                else
                {
                    return Unauthorized("Geçersiz e-posta veya şifre.");
                }
            }
            else if (model.Role == "Instructor")
            {
                var instructors = await _instructorService.GetAllAsync();
                var instructor = instructors.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if (instructor == null)
                {
                    return Unauthorized("Geçersiz e-posta veya şifre.");
                }

                user.Email = instructor.Email;
                user.Name = instructor.First_Name + " " + instructor.Last_Name;
                user.ID = instructor.Instructor_ID;
                user.Role = "Instructor";
            }
            else if (model.Role == "Student")
            {
                var students = await _studentService.GetAllAsync();
                var student = students.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if (student == null)
                {
                    return Unauthorized("Geçersiz e-posta veya şifre.");
                }

                user.Email = student.Email;
                user.Name = student.First_Name + " " + student.Last_Name;
                user.ID = student.Student_ID;
                user.Role = "Student";
            }
            else
            {
                return BadRequest("Kullanıcı bulunamamıştır.");
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                UserId = user.ID,
                UserRole = user.Role,
                UserName = user.Name
            });
        }


        private static LoginUser User = new LoginUser
        {
            Email = "system.admin@gmail.com",
            Role = "admin",
            Password = "123456admin"
        };
    }
}
