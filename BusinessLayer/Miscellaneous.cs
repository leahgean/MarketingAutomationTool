using Utilities;


namespace BusinessLayer
{
    public class Miscellaneous
    {
        public string GeneratePassword()
        {
            return string.Format("{0}{1}",
                Utilities.Random.GetRandomDigits(6, 1, 9),
                Utilities.Random.GetRandomLowerCaseCharacters(2));
        }

        public string GeneratePasswordSalt()
        {
            Cryptography crypt = new Cryptography();
            return crypt.GenerateRandomString(20);
        }

        public string GenerateEncryptedPassword(string strPassword, string strPasswordSalt)
        {
            Cryptography crypt = new Cryptography();
            return crypt.GenerateEncryptedPassword(strPassword, strPasswordSalt);
        }

    }
}
