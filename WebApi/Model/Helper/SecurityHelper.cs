using System.Security.Cryptography;
using System.Text;

namespace WebApi.Model.Helper
{
    public class SecurityHelper
    {
        private readonly RandomNumberGenerator Random = RandomNumberGenerator.Create();

        public string Getsha256Hash(string value)
        {
            using (SHA256 sha256 = SHA256.Create()) 
            {
                var byteValue = Encoding.UTF8.GetBytes(value);
                var byteHash = sha256.ComputeHash(byteValue);
                return BitConverter.ToString(byteHash).Replace("-", "").ToLower(); 
            }
            //using (SHA256 sha256 = SHA256.Create())
            //{
            //    var byteValue = Encoding.UTF8.GetBytes(value);
            //    var byteHash = sha256.ComputeHash(byteValue);
            //    return Encoding.UTF8.GetString(byteHash);
            //}
            //var algoritm = new SHA256CryptoServiceProvider();
            //var byteValue = Encoding.UTF8.GetBytes(value);
            //var byteHash = algoritm.ComputeHash(byteValue);
            //return Convert.ToBase64String(byteHash);
        }
    
    }
}
