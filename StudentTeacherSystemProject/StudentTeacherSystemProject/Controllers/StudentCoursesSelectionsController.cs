using Microsoft.AspNetCore.Mvc;
using StudentTeacherSystemProject.DTO;
using StudentTeacherSystemProject.Services.Abstracts;

namespace StudentTeacherSystemProject.Controllers
{
    public class StudentCoursesSelectionsController : Controller
    {
        private readonly IStudentCoursesSelectionsService _studentCoursesSelectionsService;

        public StudentCoursesSelectionsController(IStudentCoursesSelectionsService studentCoursesSelectionsService)
        {
            _studentCoursesSelectionsService = studentCoursesSelectionsService;
        }

        [HttpGet("GetWaitApprovedCourse/{id}")]
        public async Task<IActionResult> GetWaitApprovedCourse(int id)
        {
            var allWaitingCourse = _studentCoursesSelectionsService
                .GetAllAsync()
                .Result
                .Where(x => !x.Is_Approved.HasValue && x.Course.Instructor_ID == id)
                .ToList()
                .Select(x => new StudentCoursesSelectionsDTO
                {
                    Course_ID = x.Course_ID,
                    Instructor_ID = x.Course.Instructor_ID,
                    SelectedCourse_ID = x.Selection_ID,
                    Student_ID = x.Student_ID,
                    Student_Full_Name = x.Student.First_Name + " " + x.Student.Last_Name,
                    Course_Name = x.Course.Course_Name,
                })
                .ToList();

            return Ok(allWaitingCourse);
        }

        [HttpGet("GetAllApprovedCourse/{id}")]
        public async Task<IActionResult> GetAllApprovedCourse(int id)
        {
            var allWaitingCourse = _studentCoursesSelectionsService
                .GetAllAsync()
                .Result
                .Where(x => x.Is_Approved.HasValue && x.Is_Approved.Value && x.Course.Instructor_ID == id)
                .ToList()
                .Select(x => new StudentCoursesSelectionsDTO
                {
                    Course_ID = x.Course_ID,
                    Instructor_ID = x.Course.Instructor_ID,
                    SelectedCourse_ID = x.Selection_ID,
                    Student_ID = x.Student_ID,
                    Student_Full_Name = x.Student.First_Name + " " + x.Student.Last_Name,
                    Course_Name = x.Course.Course_Name,
                })
                .ToList();

            return Ok(allWaitingCourse);
        }


        [HttpGet("GetAllRejectedCourse/{id}")]
        public async Task<IActionResult> GetAllRejectedCourse(int id)
        {
            var allWaitingCourse = _studentCoursesSelectionsService
                .GetAllAsync()
                .Result
                .Where(x => x.Is_Approved.HasValue && !x.Is_Approved.Value && x.Course.Instructor_ID == id)
                .ToList()
                .Select(x => new StudentCoursesSelectionsDTO
                {
                    Course_ID = x.Course_ID,
                    Instructor_ID = x.Course.Instructor_ID,
                    SelectedCourse_ID = x.Selection_ID,
                    Student_ID = x.Student_ID,
                    Student_Full_Name = x.Student.First_Name + " " + x.Student.Last_Name,
                    Course_Name = x.Course.Course_Name,
                })
                .ToList();

            return Ok(allWaitingCourse);
        }
    }
}
