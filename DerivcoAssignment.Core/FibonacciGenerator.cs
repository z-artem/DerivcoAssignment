using DerivcoAssignment.Core.Dtos;
using DerivcoAssignment.Core.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace DerivcoAssignment.Core
{
    public class FibonacciGenerator : IFibonacciGenerator
    {
        private readonly ILogger<FibonacciGenerator> _logger;

        public FibonacciGenerator(ILogger<FibonacciGenerator> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<FibonacciResultDto> GenerateFibonacci(int firstIndex, int lastIndex, int timeLimit, int memoryLimit)
        {
            var cts = new CancellationTokenSource(timeLimit);

            return await Task.Run(() => RunGenerator(firstIndex, lastIndex, memoryLimit, cts.Token));
        }

        private FibonacciResultDto RunGenerator(int firstIndex, int lastIndex, int memoryLimit, CancellationToken cancellationToken)
        {
            GenerationResult result = GenerationResult.Ok;
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
                        result = GenerationResult.Timeout;
                        _logger.LogWarning("Operation cancelled by timeout exceeded");
                        break;
                    }

                    if (GC.GetTotalMemory(false) - memBefore > memoryLimit)
                    {
                        result = GenerationResult.MemExceeded;
                        _logger.LogWarning("Operation cancelled by memory usage limit exceeded");
                        break;
                    }
                }
            }
            catch (OperationCanceledException)
            {
                result = GenerationResult.Timeout;
                _logger.LogWarning("Operation cancelled by timeout exceeded");
            }
            catch (Exception ex)
            {
                result = GenerationResult.UnknownError;
                _logger.LogError(ex, "Execution interrupted by the unknown exception");
            }

            return new FibonacciResultDto
            {
                FibonacciNumbers = fibNumbers,
                Status = result
            };
        }
    }
}
