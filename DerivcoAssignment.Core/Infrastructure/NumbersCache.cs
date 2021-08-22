using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Timers;

namespace DerivcoAssignment.Core.Infrastructure
{
    public class NumbersCache : INumbersCache
    {
        private readonly ILogger<NumbersCache> _logger;

        private readonly Timer _timer;
        private List<BigInteger> _cache;

        private readonly object lockObject = new object();

        public NumbersCache(ILogger<NumbersCache> logger, IOptions<CoreSettings> settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _timer = new Timer(settings.Value.ClearCacheTimeoutSeconds * 1000);
            _timer.AutoReset = false;
            _timer.Elapsed += _timer_Elapsed;

            _cache = new List<BigInteger> { 0, 1 };
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ClearCache();
        }

        public CacheResponse GetNumbers(int lastIndex)
        {
            List<BigInteger> result;
            int actualLastIndex = -1;

            lock (lockObject)
            {
                _timer.Stop();

                actualLastIndex = lastIndex < _cache.Count ? lastIndex : (_cache.Count - 1);
                result = _cache.GetRange(0, actualLastIndex + 1);

                _timer.Start();
            }

            return new CacheResponse
            {
                Numbers = result,
                ActualLastIndex = actualLastIndex
            };
        }

        public void AddNumbers(List<BigInteger> numbers, int firstIndex)
        {
            lock (lockObject)
            {
                _timer.Stop();

                if (firstIndex == _cache.Count - 1)
                {
                    _cache.AddRange(numbers);
                }
                else if (firstIndex < _cache.Count)
                {
                    if (firstIndex + numbers.Count > _cache.Count)
                    {
                        int usefulStartIndex = _cache.Count - firstIndex;
                        int usefulNumbersCount = firstIndex + numbers.Count - _cache.Count;
                        var usefulData = numbers.GetRange(usefulStartIndex, usefulNumbersCount);

                        _cache.AddRange(usefulData);
                    }
                    else
                    {
                        _logger.LogWarning($"NumbersCache already contains all the data it was requested to add (cache length is {_cache.Count} but addendum last index is {firstIndex + numbers.Count}). No data will be added to cache.");
                    }
                }
                else
                {
                    _logger.LogError($"NumbersCache was requested to add numbers out of it's bounds (cache length is {_cache.Count} but addendum first index is {firstIndex}). No data will be added to cache.");
                }

                _timer.Start();
            }
        }

        private void ClearCache()
        {
            lock (lockObject)
            {
                _cache.Clear();
            }
        }
    }
}
