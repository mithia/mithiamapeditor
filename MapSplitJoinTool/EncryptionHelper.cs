using System.IO;
using System.Security.Cryptography;

namespace Aesir5
{
    public class EncryptionHelper
    {
        private static readonly byte[] key = new Rfc2898DeriveBytes("Aesir Map Editor", new byte[]{82,111,98,101,114,116,46,75}).GetBytes(16);

        public static Stream Encrypt(MemoryStream stream)
        {
            byte[] results;
            byte[] dataToEncrypt = stream.ToArray();
            stream.Close();

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };

            try
            {
                ICryptoTransform encryptor = TDESAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally { TDESAlgorithm.Clear(); }

            return new MemoryStream(results);
        }

        public static Stream Decrypt(MemoryStream stream)
        {
            byte[] results;
            byte[] dataToDecrypt = stream.ToArray();
            stream.Close();

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };

            try
            {
                ICryptoTransform decryptor = TDESAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally { TDESAlgorithm.Clear(); }

            return new MemoryStream(results);
        }
    }
}
