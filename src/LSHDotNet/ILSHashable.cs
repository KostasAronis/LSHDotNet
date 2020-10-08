using System;
using System.Collections.Generic;
using System.Text;

namespace LSHDotNet
{
    public interface BaseHashable
    {

    }
    public interface ILSHashable : BaseHashable
    {
        public string GetStringToHash();
    }
    public interface ILSHWeightedHashable : BaseHashable
    {
        public List<Tuple<string,double>> GetWeightedStringsToHash();
    }
    public interface IWeightedHashed : BaseHashable
    {
        public List<Tuple<int[], double>> GetWeightedHashes();
    }
}
