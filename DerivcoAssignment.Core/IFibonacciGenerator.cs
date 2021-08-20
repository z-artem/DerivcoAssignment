using System.Collections.Generic;
using System.Numerics;

namespace DerivcoAssignment.Core
{
    public interface IFibonacciGenerator
    {
        List<BigInteger> GenerateFibonacci(int firstIndex, int lastIndex);
    }
}
