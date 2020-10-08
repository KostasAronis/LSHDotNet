# LSHDotNet

LSHDotNet is a dotnet core implementation of a form of Locality Sensitive Hashing called MinHashing as explained in Jake Drew's [article](https://blog.jakemdrew.com/2014/05/08/practical-applications-of-locality-sensitive-hashing-for-unstructured-data/).

## Installation

[Nuget package url](
https://www.nuget.org/packages/LSHDotNet)

Package-Manager Console  
```
Install-Package LSHDotNet 
```
.net CLI  
```
dotnet add package LSHDotNet
```

## Usage

Current tested usage is only for searching among strings. I tried to keep the library as generic as possible and will further abstract it in order for more complex types to be successfully hashed and searched.

`Best usage examples are the working unit tests. :)`


```csharp
using LSHDotNet;

{ ... }

Dictionary<int, string> searchSpace = new Dictionary<int, string>()
{
  { 0, "C24F390FHU" },
  { 1, "S2719DGF" },
  { 2, "CJG50" },
  { 3, "24G2U" },
  { 4, "C24G1" },
  { 5, "P2219H" },
  { 6, "24TL510S-PZ" },
  { 7, "G2590VXQ" },
  { 8, "G2790PX" },
  { 9, "SE2719HR" },
  { 10, "29WL500-B" },
};

int signatureSize = 200;
MinHasher<int> minHasher = new MinHasher<int>(signatureSize, MinHasher<int>.CreateMinHashSeeds(signatureSize));
LSHSearch<int> lshSearcher = new LSHSearch<int>(minHasher, SimilarityMeasures.Jaccard);

var searchString = "C24F390FHU";

// the following query will return the top 5 results having a similarity score of over 0.4.
var result = lshSearcher.GetClosest(searchSpace, searchString, 5, 0.4); 

// first result MUST have a similarity of 1.0 because our search term is identical to the first objects value.
Assert.AreEqual(result.First().Similarity, 1);
// currently we keep the value of the string in the Metadata dictionary of our Result object. This will change.
Assert.AreEqual(result.First().Metadata["Name"], searchString[1]);
Assert.AreEqual(result.First().Id, 0);

```

## Contributing
Issues are wellcome. Pull requests are more than welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[GNU GENERAL PUBLIC LICENSE](LICENSE)

## Dependencies
[Bouncy Castle](https://www.bouncycastle.org/csharp/index.html)
