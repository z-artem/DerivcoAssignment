using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;

namespace DerivcoAssignment.Core
{
    public class FibonacciGenerator : IFibonacciGenerator
    {
        public string GenerateFibonacci(uint firstIndex, uint lastIndex)
        {
            List<BigInteger> fibNumbers = new List<BigInteger>();
            BigInteger currentNumber = 1;
            BigInteger previousNumber = 0;

            for (int i = 0; i < lastIndex; i++)
            {
                BigInteger temp = currentNumber;
                currentNumber += previousNumber;
                previousNumber = temp;

                if (i >= firstIndex)
                {
                    fibNumbers.Add(currentNumber);
                }
            }

            var jsonResult = JsonSerializer.Serialize(fibNumbers.Select(x => x.ToString()));
            return jsonResult;
        }
    }
}
