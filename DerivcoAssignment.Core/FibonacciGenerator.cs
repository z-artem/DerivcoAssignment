using DerivcoAssignment.Core.Dtos;
using DerivcoAssignment.Core.Enums;
using DerivcoAssignment.Core.Infrastructure;
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
        private readonly CacheResolver _cacheResolver;
        private readonly ILogger<FibonacciGenerator> _logger;

        private INumbersCache _cache;

        public FibonacciGenerator(CacheResolver cacheResolver, ILogger<FibonacciGenerator> logger)
        {
            _cacheResolver = cacheResolver ?? throw new ArgumentNullException(nameof(cacheResolver));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<FibonacciResultDto> GenerateFibonacci(int firstIndex, int lastIndex, bool useCache, int timeLimit, int memoryLimit)
        {
            var cts = new CancellationTokenSource(timeLimit);
            _cache = _cacheResolver(useCache);

            return await Task.Run(() => RunGenerator(firstIndex, lastIndex, memoryLimit, cts.Token));
        }

        private FibonacciResultDto RunGenerator(int firstIndex, int lastIndex, int memoryLimit, CancellationToken cancellationToken)
        {
            GenerationResult result = GenerationResult.Ok;
            List<BigInteger> fibNumbers = new List<BigInteger>();
            List<BigInteger> toCache = new List<BigInteger>();
            BigInteger currentNumber = 0;
            BigInteger previousNumber = 1;

            try
            {
                var cachedData = _cache.GetNumbers(lastIndex);

                if (cachedData.ActualLastIndex >= firstIndex)
                {
                    fibNumbers.AddRange(cachedData.GetAllFromIndexToEnd(firstIndex));
                }

                if (cachedData.ActualLastIndex == lastIndex)
                {
                    return new FibonacciResultDto
                    {
                        FibonacciNumbers = fibNumbers,
                        Status = result
                    };
                }

                int loopStart = 0;
                int lastCachedIndex = cachedData.ActualLastIndex;
                if (cachedData.ActualLastIndex >= 1)
                {
                    currentNumber = cachedData.Numbers[^1];
                    previousNumber = cachedData.Numbers[^2];
                    loopStart = cachedData.ActualLastIndex + 1;
                }

                long memBefore = GC.GetTotalMemory(false);

                for (int i = loopStart; i <= lastIndex; i++)
                {
                    if (i != 0)
                    {
                        BigInteger temp = currentNumber;
                        currentNumber += previousNumber;
                        previousNumber = temp;
                    }

                    if (i > lastCachedIndex)
                    {
                        toCache.Add(currentNumber);
                        if (toCache.Count == 10)
                        {
                            _cache.AddNumbers(toCache, lastCachedIndex);
                            toCache.Clear();
                            lastCachedIndex = i;
                        }
                    }

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

                _cache.AddNumbers(toCache, lastCachedIndex);
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
