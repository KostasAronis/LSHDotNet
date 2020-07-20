using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSHDotNet
{
    public class MinHasher<IdType>
    {
        public int _signatureSize;
        public Tuple<int, int>[] minHashSeeds;

        public MinHasher(int SignatureSize)
        {
            _signatureSize = SignatureSize;
            minHashSeeds = CreateMinHashSeeds(_signatureSize);
        }
        public MinHasher(int signatureSize, Tuple<int, int>[] minHashes)
        {
            _signatureSize = signatureSize;
            minHashSeeds = minHashes;
        }

        public static Tuple<int, int>[] CreateMinHashSeeds(int signatureSize)
        {
            Tuple<int, int>[] seeds = new Tuple<int, int>[signatureSize];
            HashSet<int> skipDups = new HashSet<int>();
            Random r = new Random();
            for (int i = 0; i < seeds.Length; i++)
            {
                Tuple<int, int> seed = new Tuple<int, int>(r.Next(), r.Next());

                if (skipDups.Add(seed.GetHashCode()))
                    seeds[i] = seed;
                else
                    i--;  //duplicate seed, try again 
            }
            return seeds;
        }
        
        public int[] GetMinHashSignature<TokenType>(TokenType[] tokens)
        {
            //Create a new signature initialized to all int max values
            int[] minHashValues = Enumerable.Repeat(int.MaxValue, _signatureSize).ToArray();

            HashSet<TokenType> skipDups = new HashSet<TokenType>();
            //Go through every single token 
            foreach (var token in tokens)
            {   //We do not want to hash the same token value more than once...
                if (skipDups.Add(token))
                {   //Hash each unique token with each unique hashing function
                    for (int i = 0; i < _signatureSize; i++)
                    {   //Use the same seeds everytime for each hashing function (this is very important!!!)
                        Tuple<int, int> seeds = minHashSeeds[i];
                        int currentHashValue = LSHHash(token, seeds.Item1, seeds.Item2);
                        //Only retain the minimum value produced by each unique hashing function.
                        if (currentHashValue < minHashValues[i])
                            minHashValues[i] = currentHashValue;
                    }
                }
            }
            return minHashValues;
        }

        public int[] GetMinHashSignature(string value)
        {
            return GetMinHashSignature(SplitInParts(value,2));
        }

        private static string[] SplitInParts(string s, int partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            var result = new List<string>();

            for (var i = 0; i < s.Length; i += partLength)
                result.Add(s.Substring(i, Math.Min(partLength, s.Length - i)));
            return result.ToArray();
        }

        public Dictionary<IdType, int[]> CreateMinhashCollection(Dictionary<IdType, string> documents)
        {
            Dictionary<IdType, int[]> minhashCollection = new Dictionary<IdType, int[]>(documents.Count);

            foreach (var document in documents)
            {
                int[] minhashSignature = GetMinHashSignature(document.Value);
                minhashCollection.Add(document.Key, minhashSignature);
            }
            return minhashCollection;
        }

        private static int LSHHash<TokenType>(TokenType inputData, int seedOne, int seedTwo)
        {   //Faster, Does not throw exception for overflows during hashing.
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                hash = hash * 16777619 ^ seedOne.GetHashCode();
                hash = hash * 16777619 ^ seedTwo.GetHashCode();
                hash = hash * 16777619 ^ inputData.GetHashCode();
                return hash;
            }
        }
    }
}
