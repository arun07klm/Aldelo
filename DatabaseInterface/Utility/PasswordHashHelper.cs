using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DatabaseInterface.Utility
{
    public class PasswordHashHelper
    {
        public const int PASSWORD_HASH_ITERATIONS = 10000;
        public const int PASSWORD_HASH_BITS = 128;

        //password hash and salt creating.
        public static LoginInfoCreate CreatePasswordHash(string password)
        {
            var loginInfo = new LoginInfoCreate();
            var saltGuid = Guid.NewGuid();
            string saltString = saltGuid.ToString();
            var kd = new Rfc2898DeriveBytes(password, saltGuid.ToByteArray(), PASSWORD_HASH_ITERATIONS);
            var hashBytes = kd.GetBytes(PASSWORD_HASH_BITS);
            var hash = System.Convert.ToBase64String(hashBytes);
            loginInfo.Salt = saltGuid;
            loginInfo.PasswordHash = hash;

            return loginInfo;
        }

        //Password validation method
        public static bool ValidatePasswordHash(LoginInfoCreate info, string password)
        {
            var saltGuid = new Guid(info.SaltVal);
            var kd = new Rfc2898DeriveBytes(password, saltGuid.ToByteArray(), PASSWORD_HASH_ITERATIONS);
            var hashBytes = kd.GetBytes(PASSWORD_HASH_BITS);
            var hash = Convert.ToBase64String(hashBytes);
            return hash == info.PasswordHash;
            //return true;
        }
    }
}
