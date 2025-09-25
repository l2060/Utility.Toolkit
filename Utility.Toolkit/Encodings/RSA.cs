using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Utility.Toolkit.Encodings
{
    /// <summary>
    /// RSA 非对称加解密
    /// </summary>
    public class RSA
    {
        public static readonly RSA Default = new RSA();


        /// <summary>
        /// 默认RSA 加密对象
        /// </summary>
        private RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

        /// <summary>
        /// 导入私钥或公钥
        /// </summary>
        /// <param name="base64Key"></param>
        public void ImportKey(String base64Key)
        {
            RSAalg.ImportCspBlob(Convert.FromBase64String(base64Key));
        }


        /// <summary>
        /// 导入私钥或公钥
        /// </summary>
        /// <param name="binaryKey"></param>
        public void ImportKey(Byte[] binaryKey)
        {
            RSAalg.ImportCspBlob(binaryKey);
        }


        /// <summary>
        /// 创建非对称加密密钥
        /// </summary>
        /// <returns></returns>
        public static (Byte[] publicKey, Byte[] privateKey) CreateKey()
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            return (RSAalg.ExportCspBlob(false), RSAalg.ExportCspBlob(true));
        }



        /// <summary>
        /// 使用密钥生成一个RSA加密对象
        /// </summary>
        /// <param name="binaryKey"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider FromKey(Byte[] binaryKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(binaryKey);
            return rsa;
        }


        /// <summary>
        /// 使用密钥生成一个RSA加密对象
        /// </summary>
        /// <param name="base64Key"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider FromKey(String base64Key)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(Convert.FromBase64String(base64Key));
            return rsa;
        }

        /// <summary>
        /// 对数据签名
        /// </summary>
        /// <param name="data">待签名数据</param>
        /// <returns>签名</returns>
        public byte[] HashAndSign(byte[] data)
        {
            return RSAalg.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }


        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="data">待验证数据</param>
        /// <param name="signedData">数据签名</param>
        /// <returns></returns>
        public bool VerifySignedHash(byte[] data, byte[] signedData)
        {
            return RSAalg.VerifyData(data, signedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }


        /// <summary>
        /// RSA 非对称加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] content)
        {
            return RSAalg.Encrypt(content, false);
        }


        /// <summary>
        /// RSA 非对称解密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] content)
        {
            return RSAalg.Decrypt(content, false);
        }


        /// <summary>
        /// RSA 非对称加密
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encryptKey">加密key</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] content, string encryptKey)
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
        public static byte[] Decrypt(byte[] content, string decryptKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportCspBlob(Convert.FromBase64String(decryptKey));
                return rsa.Decrypt(content, false);
            }
        }
    }
}
