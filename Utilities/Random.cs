using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    
        public class Random
        {
            public static string GetRandomAlphabeticCharacters(int count, int minCharachterCode, int maxCharacterCode)
            {
                if (maxCharacterCode < minCharachterCode)
                    throw new Exception("Invalid parameters. minCharachterCode must be less than maxCharachterCode");

                if (minCharachterCode < 64 || maxCharacterCode > 122)
                    throw new Exception("Invalid parameters. Character codes must be between 65 and 122");

                string result = string.Empty;
                System.Random r = new System.Random();
                for (int i = 0; i < count; i++)
                {
                    int ch = r.Next(minCharachterCode, maxCharacterCode);
                    while (ch > 91 && ch < 97)
                        ch = r.Next(minCharachterCode, maxCharacterCode);

                    result += char.ConvertFromUtf32(ch);
                }
                return result;
            }

            public static string GetRandomDigits(int count, int min, int max)
            {
                string result = string.Empty;
                System.Random r = new System.Random();
                for (int i = 0; i < count; i++)
                {
                    int ch = r.Next(min, max);
                    result += ch.ToString();
                }
                return result;
            }

            public static string GetRandomLowerCaseCharacters(int count)
            {
                return GetRandomAlphabeticCharacters(count, 97, 122);
            }

            public static string GetRandomUpperCaseCharacters(int count)
            {
                return GetRandomAlphabeticCharacters(count, 65, 90);
            }
        }
    
}
