using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace DerivcoAssignment.Core
{
    public class FibonacciGenerator : IFibonacciGenerator
    {
        public async Task<List<BigInteger>> GenerateFibonacci(int firstIndex, int lastIndex, int timeLimit, int memoryLimit)
        {
            var cts = new CancellationTokenSource(timeLimit);

            return await Task.Run(() => RunGenerator(firstIndex, lastIndex, memoryLimit, cts.Token));
        }

        private List<BigInteger> RunGenerator(int firstIndex, int lastIndex, int memoryLimit, CancellationToken cancellationToken)
        {
            List<BigInteger> fibNumbers = new List<BigInteger>();
            BigInteger currentNumber = 1;
            BigInteger previousNumber = 0;

            try
            {
                long memBefore = GC.GetTotalMemory(false);

                for (int i = 0; i < lastIndex + 1; i++)
                {
                    BigInteger temp = currentNumber;
                    currentNumber += previousNumber;
                    previousNumber = temp;

                    if (i >= firstIndex)
                    {
                        fibNumbers.Add(currentNumber);
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return fibNumbers;
                    }

                    if (GC.GetTotalMemory(false) - memBefore > memoryLimit)
                    {
                        return fibNumbers;
                    }
                }
            }
            catch (OperationCanceledException) { }

            return fibNumbers;
        }
    }
}
