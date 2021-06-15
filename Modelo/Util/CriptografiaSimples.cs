using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Modelo.Util
{
    public class CriptografiaSimples
    {
        private static AesCryptoServiceProvider csp = new AesCryptoServiceProvider();
        private static ICryptoTransform crypt = GetCryptoTransform(CriptografiaSimples.csp, true);
        private static ICryptoTransform descryt = GetCryptoTransform(CriptografiaSimples.csp, false);

        public static string Encrypt(string raw)
        {
            byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
            byte[] output = crypt.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

            string encrypted = Convert.ToBase64String(output);

            return encrypted;
        }

        public static string Decrypt(string encrypted)
        {
            byte[] output = Convert.FromBase64String(encrypted.Replace(" ", "+"));
            byte[] decryptedOutput = GetCryptoTransform(CriptografiaSimples.csp, false).TransformFinalBlock(output, 0, output.Length);

            string decypted = Encoding.UTF8.GetString(decryptedOutput);
            return decypted;
        }

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var passWord = "adfbo3usadfnasdfb12sadkfm";
            var salt = "soadkfmnoasdnfijosadnfonaslkmasdlk";

            //a random Init. Vector. just for testing
            String iv = "3sf32u8932710hwr";

            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passWord), Encoding.UTF8.GetBytes(salt), 65536);
            byte[] key = spec.GetBytes(16);


            csp.IV = Encoding.UTF8.GetBytes(iv);
            csp.Key = key;
            if (encrypting)
                return csp.CreateEncryptor();
            return csp.CreateDecryptor();
        }
    }
}
