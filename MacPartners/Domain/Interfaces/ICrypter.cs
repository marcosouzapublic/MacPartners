using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Domain.Interfaces
{
    public interface ICrypter
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
