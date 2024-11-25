using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemDaiHoc.Utilities
{
    public class EmailGenerator
    {
        public static string GenerateEmail(string fullName, string id, string domain)
        {
            // Remove diacritics and spaces from the full name
            string normalizedFullName = RemoveDiacritics(fullName).Replace(" ", string.Empty);

            // Construct the email
            string email = $"{normalizedFullName}{id}@{domain}.edu.vn";

            return email;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
