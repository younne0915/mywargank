namespace Protocol
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class AES128Helper
    {
        public static string Encrypt(string toEncrypt, string key)
        {
            byte[] keyArray = get_key(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            MemoryStream mStream = new MemoryStream();
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(mStream, cTransform, CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(toEncryptArray, 0, toEncryptArray.Length);
                cryptoStream.FlushFinalBlock();
                return ByteToHex(mStream.ToArray());
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                rDel.Clear();
            }
        }

        public static string Decrypt(string toDecrypt, string key)
        {
            byte[] keyArray = get_key(key);
            byte[] toDecryptArray = HexToByte(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private static byte[] get_key(string key)
        {
            byte[] result = Encoding.UTF8.GetBytes(key);
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(result);
        }

        private static byte[] HexToByte(string msg)
        {
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            }
            return comBuffer;
        }

        private static String ByteToHex(Byte[] vByte)
        {
            if (vByte == null || vByte.Length < 1) return null;
            StringBuilder sb = new StringBuilder(vByte.Length * 2);
            for (int i = 0; i < vByte.Length; i++)
            {
                if ((UInt32)vByte[i] < 0) return null;
                UInt32 k = (UInt32)vByte[i] / 16;
                sb.Append((Char)(k + ((k > 9) ? 'A' - 10 : '0')));
                k = (UInt32)vByte[i] % 16;
                sb.Append((Char)(k + ((k > 9) ? 'A' - 10 : '0')));
            }
            return sb.ToString().ToLower();
        }

        public static string getMD5(string password)
        {
            byte[] result = Encoding.UTF8.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            return ByteToHex(md5.ComputeHash(result));
        }
    }
}
