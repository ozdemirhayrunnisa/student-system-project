
namespace StudentTeacherSystemProject.DTO
{
    public class LoginUser
    {
        private int _id;
        private string _name;
        private string _email;
        private string _password;
        private string _role;

        //özellik isimleri
        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }


        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Role
        {
            get => _role;
            set => _role = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }
    }
}
