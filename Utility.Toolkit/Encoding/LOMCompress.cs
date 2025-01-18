using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace LOM.Shared.Compression
{
    /// <summary>
    ///  Use      Time : None < Encrypt < GZip < Zlib ≈ Deflate < Brotli \
    ///  Compress Size : Brotli < Deflate < zLib ≈ GZip < None < Encrypt \
    ///  数据量小时用 Deflate \
    ///  数据量大时用 GZip zLib Deflate
    /// </summary>
    public enum CompressType
    {
        /// <summary>
        /// 不做处理
        /// </summary>
        None = 0,
        /// <summary>
        /// AES加密处理
        /// </summary>
        Encrypt = 1,
        /// <summary>
        /// Brotli算法压缩
        /// 压缩效果好 速度慢 
        /// 小数据量压缩效果好
        /// </summary>
        Brotli = 2,
        /// <summary>
        /// GZip算法压缩
        /// 压缩效果适中 速度快 
        /// 小数据量压缩反弹严重
        /// </summary>
        GZip = 3,
        /// <summary>
        /// zLib算法压缩
        /// 压缩效果适中 速度快
        /// 小数据量压缩适中
        /// </summary>
        ZLib = 4,
        /// <summary>
        /// Deflate算法压缩
        /// 压缩效果适中 速度快  
        /// 小数据量压缩效果最好
        /// </summary>
        Deflate = 5
    }





    //BrotliStream     50=>15    550=>16    21s
    //       50=>33    550=>39    11s
    //       50=>21    550=>27    11s
    //


    /// <summary>
    /// LOM 平台压缩工具类
    /// 实现了多种压缩算法的统一接口
    /// </summary>
    public static class LOMCompress
    {
        private delegate byte[] CompressDelegate(byte[] bytes);
        private readonly static Byte[] aes_keys = { 135, 177, 160, 59, 66, 128, 206, 49, 250, 131, 173, 42, 37, 87, 117, 179, 103, 128, 182, 66, 16, 224, 162, 66, 149, 99, 140, 65, 3, 18, 33, 66 };
        private readonly static Byte[] aes_iv = { 247, 64, 177, 69, 237, 140, 48, 14, 217, 47, 210, 182, 194, 154, 170, 145 };
        private readonly static Dictionary<CompressType, CompressDelegate> MapOfCompress = new Dictionary<CompressType, CompressDelegate>() {
            { CompressType.Deflate, LOMCompress.DeflateCompress },
            { CompressType.GZip, LOMCompress.GZipCompress },
            { CompressType.Encrypt, LOMCompress.EncryptCompress },
            { CompressType.ZLib, LOMCompress.ZLibCompress },
            { CompressType.Brotli, LOMCompress.BrotliCompress }
        };

        private readonly static Dictionary<CompressType, CompressDelegate> MapOfDecompress = new Dictionary<CompressType, CompressDelegate>() {
            { CompressType.Deflate, LOMCompress.DeflateDecompress },
            { CompressType.GZip, LOMCompress.GZipDecompress },
            { CompressType.Encrypt, LOMCompress.EncryptDecompress },
            { CompressType.ZLib, LOMCompress.ZLibDecompress },
            { CompressType.Brotli, LOMCompress.BrotliDecompress }
        };

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] bytes, CompressType type)
        {
            if (MapOfCompress.TryGetValue(type, out var compress))
            {
                return compress(bytes);
            }
            return bytes;
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static byte[] Decompress(byte[] bytes, CompressType type)
        {
            if (MapOfDecompress.TryGetValue(type, out var decompress))
            {
                return decompress(bytes);
            }
            return bytes;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] EncryptCompress(byte[] bytes)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aes_keys;
                aesAlg.IV = aes_iv;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(bytes);
                            csEncrypt.FlushFinalBlock();
                            return msEncrypt.ToArray();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] EncryptDecompress(byte[] bytes)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = aes_keys;
                aesAlg.IV = aes_iv;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(bytes);
                            csEncrypt.FlushFinalBlock();
                            return msEncrypt.ToArray();
                        }
                    }
                }
            }
        }



        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] GZipCompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var brotliStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    brotliStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// GZip解压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] GZipDecompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }


        /// <summary>
        /// ZLib压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] ZLibCompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var brotliStream = new ZLibStream(memoryStream, CompressionLevel.Optimal))
                {
                    brotliStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// ZLib解压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] ZLibDecompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new ZLibStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }


        /// <summary>
        /// Deflate压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] DeflateCompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var brotliStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
                {
                    brotliStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Deflate解压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] DeflateDecompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }


        /// <summary>
        /// Brotli压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] BrotliCompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var brotliStream = new BrotliStream(memoryStream, CompressionLevel.Optimal))
                {
                    brotliStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }


        /// <summary>
        /// Brotli解压缩
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] BrotliDecompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new BrotliStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }


    }
}
