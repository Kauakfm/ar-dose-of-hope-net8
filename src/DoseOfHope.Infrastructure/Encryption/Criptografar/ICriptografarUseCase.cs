using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Infrastructure.Encryption.Criptografar
{
    public interface ICriptografarUseCase
    {
        string CriptografarID(int codigo);

        string Encrypt(string plainText);
    }
}
