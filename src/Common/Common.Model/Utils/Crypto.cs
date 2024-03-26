using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Model
{
    public class Crypto
    {
        private static byte[] Clave = Encoding.UTF8.GetBytes("80E37YTR317BK137");
        private static byte[] IV = Encoding.UTF8.GetBytes("F2Y09W0530L36G01");

        public static string Encrypt(string Cadena)
        {
            if (string.IsNullOrEmpty(Cadena)) return string.Empty;
            byte[] inputBytes = Encoding.UTF8.GetBytes(Cadena);
            byte[] encripted;
            var cripto = Aes.Create("AesManaged");
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        public static string Decrypt(string Cadena)
        {
            string textoLimpio = String.Empty;
            try
            {
                if (string.IsNullOrEmpty(Cadena)) return string.Empty;
                byte[] inputBytes = Convert.FromBase64String(Cadena);
                byte[] resultBytes = new byte[inputBytes.Length];
                var cripto = Aes.Create("AesManaged");
                using (MemoryStream ms = new MemoryStream(inputBytes))
                    using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
                        using (StreamReader sr = new StreamReader(objCryptoStream, true))
                            textoLimpio = sr.ReadToEnd();
            }
            catch (Exception)
            {
                textoLimpio = "Error";
            }
            return textoLimpio;
        }
    }
}