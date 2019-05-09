using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamplatedString
{
    class Program
    {
        static void Main(string[] args)
        {
            string teststring = " СТБ  ГОСТ Р  IEC/  ISO  555.5- 2001";
            Console.WriteLine(Test(teststring));
            Console.ReadKey();
        }
        public static string Test(string line)
        {
            line = line.Trim();
            char[] foo = line.ToCharArray();
            StringBuilder outp = new StringBuilder();
            for (int i = 0; i < foo.Length; i++)
            {
                if (char.IsLetterOrDigit(foo[i]))
                {
                    outp.Append(foo[i]);
                }
                if (char.IsPunctuation(foo[i]))
                {
                    outp.Append(foo[i]);
                }
                if (char.IsWhiteSpace(foo[i]))
                {
                    if (char.IsWhiteSpace(foo[i - 1])) { continue; }
                    if (char.IsPunctuation(foo[i - 1]) || char.IsPunctuation(foo[i + 1])) { continue; }
                    else { outp.Append(foo[i]); }
                }

            }
            return outp.ToString();
        }
    }
}
