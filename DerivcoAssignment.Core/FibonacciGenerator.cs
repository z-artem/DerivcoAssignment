using System.Collections.Generic;
using System.Numerics;

namespace DerivcoAssignment.Core
{
    public class FibonacciGenerator : IFibonacciGenerator
    {
        public List<BigInteger> GenerateFibonacci(int firstIndex, int lastIndex)
        {
            List<BigInteger> fibNumbers = new List<BigInteger>();
            BigInteger currentNumber = 1;
            BigInteger previousNumber = 0;

            for (int i = 0; i < lastIndex + 1; i++)
            {
                BigInteger temp = currentNumber;
                currentNumber += previousNumber;
                previousNumber = temp;

                if (i >= firstIndex)
                {
                    fibNumbers.Add(currentNumber);
                }
            }

            return fibNumbers;
        }
    }
}
