using DerivcoAssignment.Core.Enums;
using System.Collections.Generic;
using System.Numerics;

namespace DerivcoAssignment.Core.Dtos
{
    public class FibonacciResultDto
    {
        public List<BigInteger> FibonacciNumbers { get; set; }

        public GenerationResult Status { get; set; }
    }
}
