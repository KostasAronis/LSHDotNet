using System;
using System.Collections.Generic;
using System.Text;

namespace LSHDotNet
{
    public class Result<T> : IComparable<Result<T>>
    {
        public T Id { get; set; }
        public double Similarity { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public int CompareTo(Result<T> that)
        {
            if (this.Similarity > that.Similarity) return -1;
            if (this.Similarity == that.Similarity) return 0;
            return 1;
        }
    }
}
