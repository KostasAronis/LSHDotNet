using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSHDotNet
{
    public class MinHasher<IdType>
    {
        public int _signatureSize;
        public int[][] minHashSeeds;

        public MinHasher(int SignatureSize)
        {
            _signatureSize = SignatureSize;
            minHashSeeds = CreateMinHashSeeds(_signatureSize);
        }
        public MinHasher(int signatureSize, int[][] minHashes)
        {
            _signatureSize = signatureSize;
            minHashSeeds = minHashes;
        }

        public static int[][] CreateMinHashSeeds(int signatureSize)
        {
            var seeds = new int[signatureSize][];
            HashSet<int> skipDups = new HashSet<int>();
            Random r = new Random();
            for (int i = 0; i < seeds.Length; i++)
            {
                Tuple<int, int> seed = new Tuple<int, int>(r.Next(), r.Next());

                if (skipDups.Add(seed.GetHashCode()))
                    seeds[i] = new int[] { seed.Item1, seed.Item2 };
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
                        int[] seed = minHashSeeds[i];
                        int currentHashValue = LSHHash(token, seed[0], seed[1]);
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
            return GetMinHashSignature(SplitInParts(value,1));
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

        public Dictionary<IdType, List<int[]>> CreateMinhashCollection(Dictionary<IdType, ILSHWeightedHashable> documents)
        {
            Dictionary<IdType, List<int[]>> minhashCollection = new Dictionary<IdType, List<int[]>>(documents.Count);

            foreach (var document in documents)
            {
                var weightedStrings = document.Value.GetWeightedStringsToHash();
                var minHashSignatures = new List<int[]>();
                foreach(var weightedString in weightedStrings)
                {
                    minHashSignatures.Add(GetMinHashSignature(weightedString.Item1));
                }
                minhashCollection.Add(document.Key, minHashSignatures);
            }
            return minhashCollection;
        }

        public Dictionary<IdType, int[]> CreateMinhashCollection(Dictionary<IdType, ILSHashable> documents)
        {
            Dictionary<IdType, int[]> minhashCollection = new Dictionary<IdType, int[]>(documents.Count);

            foreach (var document in documents)
            {
                int[] minhashSignature = GetMinHashSignature(document.Value.GetStringToHash());
                minhashCollection.Add(document.Key, minhashSignature);
            }
            return minhashCollection;
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
