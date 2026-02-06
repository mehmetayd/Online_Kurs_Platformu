using System.Security.Cryptography;
using System.Text;

namespace OnlineCourse.Api.Extensions
{
    public static class StringExtensions
    {
        public static string ToHash(this string value) 
        {  
            var byteValues = Encoding.UTF8.GetBytes(value);

            var hashBytes = SHA256.HashData(byteValues);

            var hashBuilder = new StringBuilder();
            foreach (byte x in hashBytes)
            {
                hashBuilder.Append(x.ToString("x2"));
            }

            return hashBuilder.ToString();
        }
    }
}
