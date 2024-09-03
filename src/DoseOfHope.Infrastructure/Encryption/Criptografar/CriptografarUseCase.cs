using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Infrastructure.Encryption.Criptografar
{
    public class CriptografarUseCase : ICriptografarUseCase
    {
        private readonly byte[] _key;

        public CriptografarUseCase(string key)
        {
            _key = HexStringToByteArray(key);
            if (_key.Length != 32) 
            {
                throw new ArgumentException("A chave deve ter 256 bits (32 bytes).");
            }
        }

        public string CriptografarID(int codigo)
        {
            var id1 = ((codigo * 362) - 34).ToString();
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            char[] characters = id1.ToCharArray();

            var retorno = "";

            foreach (var letra in characters)
            {
                Random random = new Random();
                int index = random.Next(0, alphabet.Length);
                retorno += alphabet[index] + letra.ToString();
            }
            return retorno;
        }

        public string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = _key; 
                aes.IV = new byte[16];

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    var encryptedBytes = ms.ToArray();
                    return Convert.ToBase64String(encryptedBytes).Replace("/", "_").Replace("+", "-");
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
