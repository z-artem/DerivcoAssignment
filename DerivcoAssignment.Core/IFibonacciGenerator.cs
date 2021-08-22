using DerivcoAssignment.Core.Dtos;
using System.Threading.Tasks;

namespace DerivcoAssignment.Core
{
    public interface IFibonacciGenerator
    {
        Task<FibonacciResultDto> GenerateFibonacci(int firstIndex, int lastIndex, bool useCache, int timeLimit, int memoryLimit);
    }
}
