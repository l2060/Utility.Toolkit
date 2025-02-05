using System;

namespace Utility.Toolkit.Encodings
{
    /// <summary>
    /// Hex26 encoding.
    /// </summary>
    public static class Hex26
    {
        private const Int32 BASE_NUMBER = 26;
        private static readonly Int32 CACHE_SIZE = 64;
        [ThreadStatic]
        private static Char[] HEX_CACHE = new Char[CACHE_SIZE + 1];


        /// <summary>
        /// Convert a number to a base 26 string. <br/>
        /// n > 0
        /// </summary>
        /// <param name="n"> </param>
        /// <returns></returns>
        public static String ToHex26(this Int64 n)
        {
            // Edge case: if n is 0, return "A" (as base-26 encoding starts from 1, corresponding to 'A')
            if (n == 0) return "@";
            var start = CACHE_SIZE;
            // Perform the conversion process
            while (n > 0)
            {
                long m = (n - 1) % BASE_NUMBER; // Correct the calculation to handle base-26 properly (1-based indexing)
                HEX_CACHE[start] = (char)(m + 'A'); // 'A' is 65 in ASCII, so we add 64 to make 1 -> 'A', 2 -> 'B', ..., 26 -> 'Z'
                n = (n - 1) / BASE_NUMBER; // Move to the next base-26 digit
                start--;
                // Avoid starting too far left in the cache if n is smaller than CACHE_SIZE
                if (start < 0) break;
            }
            // If the number was large enough to fill the cache, return the full string
            return new String(HEX_CACHE, start + 1, CACHE_SIZE - start);
        }



        /// <summary>
        /// Parse a base 26 string to a number.
        /// </summary>
        /// <param name="hex26Str"></param>
        /// <param name="int64Value"></param>
        /// <returns></returns>
        public static Boolean Parse(string hex26Str, out Int64 int64Value)
        {
            int64Value = 0;
            if (hex26Str == "@") return true;
            int len = hex26Str.Length;
            for (int i = 0; i < len; i++)
            {
                char c = hex26Str[i];
                if (c < 'A' || c > 'Z') return false;
                int digitValue = c - 'A' + 1;
                int64Value = int64Value * BASE_NUMBER + digitValue;
            }
            return true;
        }





        /// <summary>
        /// Convert a number to a base 26 string.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static String ToHex26(this Int32 n)
        {
            return ToHex26((Int64)n);
        }

        /// <summary>
        /// Convert a number to a base 26 string.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static String ToHex26(this UInt32 n)
        {
            return ToHex26((Int64)n);
        }

        /// <summary>
        /// Convert a number to a base 26 string.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static String ToHex26(this Int16 n)
        {
            return ToHex26((Int64)n);
        }

        /// <summary>
        /// Convert a number to a base 26 string.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static String ToHex26(this UInt16 n)
        {
            return ToHex26((Int64)n);
        }

        /// <summary>
        /// Convert a number to a base 26 string.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static String ToHex26(this Byte n)
        {
            return ToHex26((Int64)n);
        }
    }
}
