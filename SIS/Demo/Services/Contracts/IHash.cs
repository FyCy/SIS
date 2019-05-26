using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Services.Contracts
{
    public interface IHash
    {
        string Hash(string stringToHash);
    }
}
