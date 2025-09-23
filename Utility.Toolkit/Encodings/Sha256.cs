using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace System.Security.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public static class SHA256Extensions
    {
        ///
        extension(SHA256 ss)
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public static String Sum(ReadOnlySpan<byte> source)
            {
                var result = SHA256.HashData(source);
                return BitConverter.ToString(result, 0).Replace("-", string.Empty);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            public static String Sum(Byte[] source)
            {
                var result = SHA256.HashData(source);
                return BitConverter.ToString(result, 0).Replace("-", string.Empty);
            }
        }

    }
}
