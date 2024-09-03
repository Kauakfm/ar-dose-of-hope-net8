using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Infrastructure.Encryption.Descriptografar
{
    public interface IDescriptografarUseCase
    {
        int DescriptografarID(string token);
        string Decrypt(string encryptedText);
    }
}
