using DerivcoAssignment.Core.Dtos;
using System.Threading.Tasks;

namespace DerivcoAssignment.Core
{
    public interface IFibonacciGenerator
    {
        Task<FibonacciResultDto> GenerateFibonacci(int firstIndex, int lastIndex, int timeLimit, int memoryLimit);
    }
}
