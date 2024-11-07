using EmpMgmt.Services.Services.IServices;
using System.Security.Cryptography;
using System.Text;

namespace EmpMgmt.Services.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }
    }
}
