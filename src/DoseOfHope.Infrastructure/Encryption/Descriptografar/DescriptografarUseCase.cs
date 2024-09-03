using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Infrastructure.Encryption.Descriptografar
{
    public class DescriptografarUseCase : IDescriptografarUseCase
    {
        private readonly byte[] _key;

        public DescriptografarUseCase(string key)
        {
            _key = HexStringToByteArray(key);
            if (_key.Length != 32)
            {
                throw new ArgumentException("A chave deve ter 256 bits (32 bytes).");
            }
        }
        public int DescriptografarID(string token)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in token)
            {
                if (!char.IsLetter(c))
                {
                    result.Append(c);
                }
            }
            token = result.ToString();
            var id = (Convert.ToInt32(token) + 34) / 362;
            return id;
        }
        public string Decrypt(string encryptedText)
        {
            encryptedText = encryptedText.Replace("_", "/").Replace("-", "+");

            var encryptedBytes = Convert.FromBase64String(encryptedText);

            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = new byte[16];

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(encryptedBytes))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
