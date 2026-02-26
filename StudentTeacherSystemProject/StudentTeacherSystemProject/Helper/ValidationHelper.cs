using System.Text.RegularExpressions;

namespace StudentTeacherSystemProject.Helper
{
    public static class ValidationHelper
    {
        // E-posta formatını kontrol etme
        public static bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Adın 2 harften fazla ve belirli bir uzunlukta olup olmadığını kontrol etme
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length > 2 && name.Length < 100;
        }
    }
}
//Eğer static anahtar kelimesi kullanılmasaydı, metodun çağrılması için sınıfın bir örneğini (instance) oluşturmanız gerekirdi. Yani, metodu sınıf adı üzerinden değil, sınıfın bir nesnesi aracılığıyla çağırmak zorunda kalırdınız.