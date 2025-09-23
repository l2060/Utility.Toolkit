using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utility.Toolkit.Encodings
{

    /// <summary>
    /// 
    /// </summary>
    public class AES
    {
        private readonly String DEFAULT_FILL_KEY = "727c5fb3e4334a71ca442fd254cc1953";
        /// <summary>
        /// 默认的对称AES加解密对象
        /// </summary>
        public static AES Default = new AES();

        private Aes aesAlg = Aes.Create();

        private AES()
        {
            ImportKey("");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64Key"></param>
        public void ImportKey(String base64Key)
        {
            aesAlg.Key = loadBase64Key(base64Key);
        }


        /// <summary>
        /// 默认RSA 导入Key
        /// </summary>
        /// <param name="binaryKey"></param>
        public void ImportKey(Byte[] binaryKey)
        {
            var data = loadBase64Key("");
            byte[] result = binaryKey.Concat(data).Take(32).ToArray();
            aesAlg.Key = result;
        }


        private Byte[] loadBase64Key(String base64Key)
        {
            if (base64Key == null)
            {
                base64Key = DEFAULT_FILL_KEY;
            }
            else
            {
                base64Key = base64Key + DEFAULT_FILL_KEY;
            }
            if (base64Key.Length > 32) base64Key = base64Key.Substring(0, 32);
            return Convert.FromBase64String(base64Key);
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public String Encrypt(String plainText)
        {
            var data = Encoding.UTF8.GetBytes(plainText);
            var result = Encrypt(data);
            return Convert.ToBase64String(result);
        }


        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public String Decrypt(String cipherText)
        {
            var data = Convert.FromBase64String(cipherText);
            var result = Decrypt(data);
            return Encoding.UTF8.GetString(result);
        }





        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="plainData"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] plainData)
        {
            aesAlg.GenerateIV();
            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(aesAlg.IV);
                // Create an encryptor to perform the stream transform.
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainData);
                        csEncrypt.FlushFinalBlock();
                        return msEncrypt.ToArray();
                    }
                }
            }
        }


        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="cipherData"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] cipherData)
        {
            aesAlg.IV = cipherData[0..16];
            // Create the streams used for decryption.
            using (MemoryStream ms = new MemoryStream())
            {
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(cipherData, 16, cipherData.Length - 16);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
