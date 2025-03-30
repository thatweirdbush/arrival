using System.Text.RegularExpressions;

namespace BookingManagementSystem.Helpers;
public class ValidationAccount
{
    public ValidationAccount() { }

    // Kiểm tra username (email hoặc số điện thoại)
    public static bool IsValidUsername(string username)
    {
        // Biểu thức regex cho email
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        // Biểu thức regex cho số điện thoại (ít nhất 10 chữ số)
        var phonePattern = @"^[0-9]{10,}$";

        // Kiểm tra nếu username là email hoặc số điện thoại
        if (Regex.IsMatch(username, emailPattern) || Regex.IsMatch(username, phonePattern))
        {
            return true;
        }
        return false;
    }

    // Kiểm tra password (ít nhất 8 ký tự)
    public static bool IsValidPassword(string password)
    {
        return password.Length >= 8;
    }

    // Kiểm tra password và confirm password giống nhau
    public static bool IsPasswordMatch(string password, string confirmPassword)
    {
        return password == confirmPassword;
    }
}
