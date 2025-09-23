
namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 将Byte转换为Base64编码
        /// </summary>
        /// <returns></returns>
        public static String ToBase64(this Byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
