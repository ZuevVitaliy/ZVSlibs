using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZVS.Global.Security
{
    public static class Crypter
    {
        public static string Encrypt(string str, string strPassword)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(str), strPassword));
        }

        public static byte[] Encrypt(byte[] data, string strPassword)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateEncryptor((new PasswordDeriveBytes(strPassword, null)).GetBytes(16), new byte[16]);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);

            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();

            byte[] result = ms.ToArray();

            ms.Close();
            ms.Dispose();

            cs.Close();
            cs.Dispose();

            ct.Dispose();

            return result;
        }

        public static string Decrypt(string str, string strPassword)
        {
            string result;

            try
            {
                byte[] data = Decrypt(Convert.FromBase64String(str), strPassword);
                result = Encoding.UTF8.GetString(data);
            }
            catch (CryptographicException)
            {
                return null;
            }

            return result;
        }

        public static byte[] Decrypt(byte[] data, string strPassword)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateDecryptor((new PasswordDeriveBytes(strPassword, null)).GetBytes(16), new byte[16]);

            MemoryStream ms = new MemoryStream(data);
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
            MemoryStream resultStream = new MemoryStream();
            cs.CopyTo(resultStream);
            return resultStream.ToArray();
        }
    }
}
