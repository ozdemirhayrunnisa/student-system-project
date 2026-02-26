using StudentTeacherSystemProject.DTO;

namespace StudentTeacherSystemProject.Services.Abstracts
{
    public interface IJwtService
    {
        string GenerateToken(LoginUser user);
    }

}
