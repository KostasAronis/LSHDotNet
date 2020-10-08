using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSHDotNet
{
    public class LSHSearch<IdType>
    {
        private readonly MinHasher<IdType> minHasher;
        private readonly Func<int[], int[], double> similarityMeasure;

        public LSHSearch(MinHasher<IdType> minHasher, Func<int[], int[], double> similarityMeasure)
        {
            this.minHasher = minHasher;
            this.similarityMeasure = similarityMeasure;
        }

        public List<Result<IdType>> GetClosest(Dictionary<IdType, ILSHWeightedHashable> searchSpace, string searchString, int count, double threshold = 0.5)
        {
            var searchSpaceHashes = minHasher.CreateMinhashCollection(searchSpace);
            var searchMinHash = minHasher.GetMinHashSignature(searchString);
            var results = new List<Result<IdType>>();
            foreach (var product in searchSpace)
            {
                var id = product.Key;
                var weightedStrings = product.Value.GetWeightedStringsToHash();
                double productSimilarity = 0;
                for(var i = 0; i < weightedStrings.Count; i++)
                {
                    var str = weightedStrings[i].Item1;
                    var weight = weightedStrings[i].Item2;
                    var minhash = searchSpaceHashes[id][i];
                    var weightedSimilarity = similarityMeasure.Invoke(minhash, searchMinHash) * weight;
                    productSimilarity += weightedSimilarity;
                }
                results.Add(new Result<IdType>()
                {
                    Id = id,
                    Similarity = productSimilarity
                });
            }
            results.Sort();
            return results.Take(count).Where(r => r.Similarity > threshold).ToList();
        }

        public List<Result<IdType>> GetClosest(Dictionary<IdType, ILSHashable> searchSpace, string searchString, int count, double threshold = 0.5)
        {
            var searchSpaceHashes = minHasher.CreateMinhashCollection(searchSpace);
            var searchMinHash = minHasher.GetMinHashSignature(searchString);
            var results = new List<Result<IdType>>();
            foreach (var product in searchSpace)
            {
                var id = product.Key;
                var minhash = searchSpaceHashes[id];
                var similarity = similarityMeasure.Invoke(minhash, searchMinHash);
                results.Add(new Result<IdType>()
                {
                    Id = id,
                    Similarity = similarity
                });
            }
            results.Sort();
            return results.Take(count).Where(r => r.Similarity > threshold).ToList();
        }

        public List<Result<IdType>> GetClosest(Dictionary<IdType, string> searchSpace, string searchString, int count, double threshold = 0.5)
        {
            var searchSpaceHashes = minHasher.CreateMinhashCollection(searchSpace);
            var searchMinHash = minHasher.GetMinHashSignature(searchString);
            var results = new List<Result<IdType>>();
            foreach (var product in searchSpace)
            {
                var id = product.Key;
                var minhash = searchSpaceHashes[id];
                var similarity = similarityMeasure.Invoke(minhash, searchMinHash);
                results.Add(new Result<IdType>()
                {
                    Id = id,
                    Similarity = similarity
                });
            }
            results.Sort();
            return results.Take(count).Where(r=>r.Similarity > threshold).ToList();
        }

        public List<T> GetClosest<T>(Dictionary<T,int[]> searchSpaceHashes, int[] searchMinHash, int count)
        {
            var results = new List<Result<T>>();
            foreach (var product in searchSpaceHashes)
            {
                var id = product.Key;
                var minhash = product.Value;
                var similarity = similarityMeasure.Invoke(searchMinHash, minhash);
                results.Add(new Result<T>() { 
                    Id = id, 
                    Similarity = similarity
                });
            }
            results.Sort();
            return results.Take(count).Select(r=>r.Id).ToList();
        }
    }
}
