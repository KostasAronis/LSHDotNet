using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSHDotNet
{
    public static class SimilarityMeasures
    {
        public static double Jaccard(int[] setA, int[] setB)
        {
            int intersection = setA.Intersect(setB).Count();
            return intersection / (double)setA.Length;
        }
        public static double Cosine(int[] setA, int[] setB)
        {
            int intersection = setA.Intersect(setB).Count();
            return intersection / (double)setA.Length;
        }
    }
}
