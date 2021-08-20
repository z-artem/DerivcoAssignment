using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace DerivcoAssignment.Core
{
    public interface IFibonacciGenerator
    {
        Task<List<BigInteger>> GenerateFibonacci(int firstIndex, int lastIndex, int timeLimit);
    }
}
