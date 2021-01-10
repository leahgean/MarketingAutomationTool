using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public static class Extensions
    {
        public static bool IsPasswordString(string p_sPassword)
        {
            //return Regex.IsMatch(p_sPassword, "(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+");
            return Regex.IsMatch(p_sPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$");
        }
    }
}
