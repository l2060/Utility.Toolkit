using System;
using System.IO;
using System.Security.Cryptography;



namespace LOM.Shared.Encoding
{

    /// <summary>
    /// LOM平台的字符串，字节流编码类
    /// </summary>
    public static class LOMEncoding
    {

        private readonly static String defaultAESKey = "69D73CE46F0D4FC6B79702ED56D46940";



        public static void loadKey()
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            string str_Private_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(true));
            string str_Public_Key = Convert.ToBase64String(RSAalg.ExportCspBlob(false));
            Console.WriteLine("公钥：" + str_Public_Key);
            Console.WriteLine();
            Console.WriteLine("私钥：" + str_Private_Key);
            Console.WriteLine();
        }

        /// <summary>
        /// 对数据签名
        /// </summary>
        /// <param name="data">待签名数据</param>
        /// <param name="private_Key">私钥</param>
        /// <returns>签名</returns>
        public static byte[] HashAndSign(byte[] data, string private_Key)
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAalg.ImportCspBlob(Convert.FromBase64String(private_Key));
            return RSAalg.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }


        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="data">待验证数据</param>
        /// <param name="signedData">数据签名</param>
        /// <param name="public_Key">公钥</param>
        /// <returns></returns>
        public static bool VerifySignedHash(byte[] data, byte[] signedData, string public_Key)
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAalg.ImportCspBlob(Convert.FromBase64String(public_Key));
            return RSAalg.VerifyData(data, signedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }



        /// <summary>
        /// 生成MD5签名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String MD5(String str)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(str);
            return MD5(data);
        }

        /// <summary>
        /// 生成MD5签名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String MD5(byte[] data)
        {
            using (MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] result = md5.ComputeHash(data);
                return BitConverter.ToString(result).Replace("-", "");
            };
        }


        /// <summary>
        /// 生成SHA256签名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String SHA256(String str)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(str);
            return SHA256(data);
        }

        /// <summary>
        /// 生成SHA256签名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String SHA256(byte[] data)
        {
            using (SHA256 sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashValue = sha.ComputeHash(data);
                return BitConverter.ToString(hashValue, 0).Replace("-", string.Empty);
            }
        }




        public static String EncodePassword(String plainText)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(plainText);
            var result = AESEncrypt(data, "2023-03-26 00:00:00");
            return Convert.ToBase64String(result);
        }



        /// <summary>
        /// AES对称加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String AESEncrypt(String plainText, String Key)
        {
            var data = System.Text.Encoding.UTF8.GetBytes(plainText);
            var result = AESEncrypt(data, Key);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// AES对称解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String AESDecrypt(String cipherText, String Key)
        {
            var data = Convert.FromBase64String(cipherText);
            var result = AESDecrypt(data, Key);
            return System.Text.Encoding.UTF8.GetString(result);
        }




        public static String EncryptPassword(String password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = new Byte[] { 64, 219, 170, 171, 53, 246, 48, 99, 31, 155, 65, 0, 46, 141, 110, 63, 132, 120, 184, 247, 150, 117, 216, 97, 218, 172, 206, 135, 88, 161, 163, 243 };
                aesAlg.IV = new Byte[] { 63, 80, 94, 127, 148, 230, 136, 105, 88, 110, 97, 92, 42, 37, 180, 127 };
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(bytes);
                            csEncrypt.FlushFinalBlock();
                            var outBytes = msEncrypt.ToArray();
                            return Convert.ToBase64String(outBytes).Replace("=", "");
                        }
                    }
                }
            }
        }




        /// <summary>
        /// AES对称加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static byte[] AESEncrypt(byte[] plainText, String Key)
        {

            Key = Key + defaultAESKey;
            if (Key.Length > 32) Key = Key.Substring(0, 32);

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(Key);
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
                            csEncrypt.Write(plainText);
                            csEncrypt.FlushFinalBlock();
                            return msEncrypt.ToArray();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// AES对称解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static byte[] AESDecrypt(byte[] cipherText, String Key)
        {
            Key = Key + defaultAESKey;
            if (Key.Length > 32) Key = Key.Substring(0, 32);
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = cipherText[0..16];
                // Create a decryptor to perform the stream transform.

                // Create the streams used for decryption.
                using (MemoryStream ms = new MemoryStream())
                {
                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(cipherText, 16, cipherText.Length - 16);
                            cs.FlushFinalBlock();
                            return ms.ToArray();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// RSA 非对称加密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encryptKey">加密key</param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(byte[] content, string encryptKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportCspBlob(Convert.FromBase64String(encryptKey));
                return rsa.Encrypt(content, false);
            }
        }


        /// <summary>
        /// RSA 非对称解密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="decryptKey">解密key</param>
        /// <returns></returns>
        public static byte[] RSADecrypt(byte[] content, string decryptKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportCspBlob(Convert.FromBase64String(decryptKey));
                return rsa.Decrypt(content, false);
            }
        }

    }








}
