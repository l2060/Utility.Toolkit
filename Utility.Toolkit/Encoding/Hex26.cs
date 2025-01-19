using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Toolkit.Encoding
{
    public static class Hex26
    {



        public static String ToHex26(this Int32 n)
        {
            string s = string.Empty;
            while (n > 0)
            {
                int m = n % 26;
                if (m == 0) m = 26;
                s = (char)(m + 64) + s;
                n = (n - m) / 26;
            }
            return s;
        }


        public static String ToHex26(this Int16 n)
        {
            string s = string.Empty;
            while (n > 0)
            {
                int m = n % 26;
                if (m == 0) m = 26;
                s = (char)(m + 64) + s;
                n = (Int16)((n - m) / 26);
            }
            return s;
        }


        public static String ToHex26(this Byte n)
        {
            string s = string.Empty;
            while (n > 0)
            {
                int m = n % 26;
                if (m == 0) m = 26;
                s = (char)(m + 64) + s;
                n = (Byte)((n - m) / 26);
            }
            return s;
        }
    }
}
