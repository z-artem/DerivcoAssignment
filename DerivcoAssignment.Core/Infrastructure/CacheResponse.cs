using System.Collections.Generic;
using System.Numerics;

namespace DerivcoAssignment.Core.Infrastructure
{
    public class CacheResponse
    {
        public List<BigInteger> Numbers { get; set; } = new List<BigInteger>();

        public int ActualLastIndex { get; set; }

        public List<BigInteger> GetAllFromIndexToEnd(int index) => Numbers.GetRange(index, Numbers.Count - index);
    }
}
