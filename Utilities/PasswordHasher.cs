using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemDaiHoc.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string hashedInputPassword = HashPassword(password);
            return string.Equals(hashedInputPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
