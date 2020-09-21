using System;
using System.Collections.Generic;
using System.Text;

namespace LSHDotNet
{
    public interface ILSHashable
    {
        public string GetStringToHash();
    }
    public interface ILSHWeightedHashable
    {
        public List<Tuple<string,double>> GetWeightedStringsToHash();
    }
}
