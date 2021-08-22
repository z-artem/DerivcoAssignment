using System.Collections.Generic;
using System.Numerics;

namespace DerivcoAssignment.Core.Infrastructure
{
    public class DummyCache : INumbersCache
    {
        public void AddNumbers(List<BigInteger> numbers, int firstIndex) { }

        public CacheResponse GetNumbers(int lastIndex) => new CacheResponse();
    }
}
