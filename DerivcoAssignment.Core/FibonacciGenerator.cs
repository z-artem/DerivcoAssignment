using DerivcoAssignment.Core.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Numerics;

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

            var jsonResult = JsonConvert.SerializeObject(fibNumbers, new FibonacciResultConverter());
            return jsonResult;
        }
    }
}
