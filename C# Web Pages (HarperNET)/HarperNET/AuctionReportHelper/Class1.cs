using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AuctionReportHelper
{
    public class Crypto
    {
        public static string Decrypt256FromHEX(string keyin, string ivin, string cipherText)
        {
            string result = string.Empty;
            try
            {
                byte[] key = Encoding.UTF8.GetBytes(keyin);
                byte[] iv = Encoding.UTF8.GetBytes(ivin);
                AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
                aesProvider.Mode = CipherMode.CBC;
                aesProvider.Padding = PaddingMode.Zeros;

                byte[] cipherTextBytesForDecrypt = HexStringToByteArray(cipherText);

                ICryptoTransform cryptoDecryptor = aesProvider.CreateDecryptor(key, iv);
                MemoryStream memStreamEncryptData = new MemoryStream(cipherTextBytesForDecrypt);
                CryptoStream rStream = new CryptoStream(memStreamEncryptData, cryptoDecryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytesForDecrypt.Length];
                int decryptedByteCount = rStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memStreamEncryptData.Close();
                rStream.Close();
                result = Encoding.Default.GetString(plainTextBytes, 0, decryptedByteCount).Replace("\0", "");
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                result = cipherText;
            }
            return result;
        }

        public static byte[] HexStringToByteArray(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];
            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
    }
}
