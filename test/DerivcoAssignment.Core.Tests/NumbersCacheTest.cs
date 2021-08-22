using DerivcoAssignment.Core.Infrastructure;
using DerivcoAssignment.Tests.Common;
using DerivcoAssignment.Tests.Common.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace DerivcoAssignment.Core.Tests
{
    public class NumbersCacheTest : TestBase<NumbersCache>
    {
        private readonly IOptions<CoreSettings> _settings;

        public NumbersCacheTest()
        {
            _settings = Options.Create(new CoreSettings { ClearCacheTimeoutSeconds = 3600 });

            TestTarget = new NumbersCache(_logger, _settings);
        }

        [Fact]
        public override void Constructor_ArgumentIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new NumbersCache(null, _settings));
        }

        [Fact]
        public void AddNumbers_EmptyCache_Adds()
        {
            // arrange
            var numbers = new List<BigInteger> { 0, 1, 1, 2, 3, 5, 8 };

            // act
            TestTarget.AddNumbers(numbers, 0);

            // assert
            _logger.DidNotReceiveWithAnyArgs().Log(LogLevel.None, "", null);
        }

        [Fact]
        public void AddNumbers_AlreadyCached_LogsAndDoesNotAdd()
        {
            // arrange
            var numbers = new List<BigInteger> { 0, 1, 1, 2, 3, 5, 8 };
            TestTarget.AddNumbers(numbers, 0);

            // act
            TestTarget.AddNumbers(numbers, 0);

            // assert
            _logger.VerifyLogged(LogLevel.Warning, "NumbersCache already contains all the data it was requested to add (cache length is 7 but addendum last index is 7). No data will be added to cache.");
        }

        [Fact]
        public void AddNumbers_CacheBroken_LogsAndDoesNotAdd()
        {
            // arrange
            var numbers = new List<BigInteger> { 144, 233, 377, 610 };

            // act
            TestTarget.AddNumbers(numbers, 12);

            // assert
            _logger.VerifyLogged(LogLevel.Error, "NumbersCache was requested to add numbers out of it's bounds (cache length is 2 but addendum first index is 12). No data will be added to cache.");
        }

        [Theory]
        [InlineData(4, 4)]
        [InlineData(6, 6)]
        [InlineData(10, 6)]
        public void GetNumbers_ArgumentIsOk_ReturnsCached(int lastIndex, int expectedLastIndex)
        {
            // arrange
            var numbers = new List<BigInteger> { 0, 1, 1, 2, 3, 5, 8 };
            TestTarget.AddNumbers(numbers, 0);

            // act
            var actualResult = TestTarget.GetNumbers(lastIndex);

            // assert
            actualResult.Should().NotBeNull();
            actualResult.ActualLastIndex.Should().Be(expectedLastIndex);
            actualResult.Numbers.Count.Should().Be(expectedLastIndex + 1);
            actualResult.Numbers.SequenceEqual(numbers);
        }
    }
}
