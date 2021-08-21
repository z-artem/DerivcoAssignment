using System.Collections.Generic;
using System.Numerics;

namespace DerivcoAssignment.Core.Infrastructure
{
    public interface INumbersCache
    {
        CacheResponse GetNumbers(int lastIndex);

        void AddNumbers(List<BigInteger> numbers, int firstIndex);
    }
}
