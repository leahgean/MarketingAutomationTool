using System;
using System.Text;

namespace Utilities
{
    public class Cryptography
    {
        public string GenerateEncryptedPassword(string password, string salt)
        {
            System.Security.Cryptography.MD5 md5;
            Byte[] OriginalBytes;
            Byte[] EncodedBytes;
            const string strSalt = "02-Mar-09";
            md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            OriginalBytes = ASCIIEncoding.Default.GetBytes(password + salt + strSalt);
            EncodedBytes = md5.ComputeHash(OriginalBytes);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < EncodedBytes.Length; i++)
            {
                sBuilder.Append(EncodedBytes[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }


        public string GenerateRandomString(int resultStringLength)
        {
            return GenerateRandomString(resultStringLength, false);
        }
        public string GenerateRandomString(int resultStringLength, bool useSafeCharacrersOnly)
        {
            string strUnsafeCharacters = "\".,~'{}:;";
            StringBuilder builder = new StringBuilder();
            System.Random random = new System.Random();
            char Char;
            for (int i = 0; i < resultStringLength; i++)
            {
                Char = Convert.ToChar(Convert.ToInt32(Math.Floor(93 * random.NextDouble() + 33)));
                bool blnUnsafeCharDetected = false;
                if (useSafeCharacrersOnly)
                {
                    foreach (char C in strUnsafeCharacters)
                    {
                        if (C == Char)
                        {
                            blnUnsafeCharDetected = true;
                            break;
                        }
                    }
                    if (blnUnsafeCharDetected) { continue; }
                }
                builder.Append(Char);
            }
            return builder.ToString();
        }
    }
}
