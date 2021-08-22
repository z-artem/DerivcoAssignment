using DerivcoAssignment.Core.Dtos;
using System;
using System.Linq;

namespace DerivcoAssignment.Core.Tests.Extensions
{
    public static class FibonacciResultDtoExtension
    {
        public static bool EqualsTo(this FibonacciResultDto dto1, FibonacciResultDto dto2) =>
            dto1.Status == dto2.Status &&
            dto1.FibonacciNumbers.Count == dto2.FibonacciNumbers.Count &&
            dto1.FibonacciNumbers.SequenceEqual(dto2.FibonacciNumbers);
    }
}
