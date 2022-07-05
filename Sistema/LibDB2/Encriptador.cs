using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibDB2
{
    public class Encriptador
    {
        public static string DecoPwd(string pwd)
        {
            string s = string.Empty;
            for (int i = 0; i < pwd.Length; i++)
            {
                s += ((char)((System.Text.Encoding.ASCII.GetBytes(pwd.Substring(i, 1))[0] - i - 1))).ToString();
            }
            return s;
        }
        
        public static string CodPwd(string pwd)
        {
            string s = string.Empty;
            for (int i = 0; i < pwd.Length; i++)
            {
                s += ((char)((System.Text.Encoding.ASCII.GetBytes(pwd.Substring(i, 1))[0] + i + 1))).ToString();
            }
            return s;
        }
        
    }
}
