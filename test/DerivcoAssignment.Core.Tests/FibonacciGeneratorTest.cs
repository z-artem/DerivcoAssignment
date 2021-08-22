using DerivcoAssignment.Core.Dtos;
using DerivcoAssignment.Core.Infrastructure;
using DerivcoAssignment.Core.Tests.Extensions;
using DerivcoAssignment.Core.Tests.TestData;
using DerivcoAssignment.Tests.Common;
using DerivcoAssignment.Tests.Common.Helpers;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DerivcoAssignment.Core.Tests
{
    public class FibonacciGeneratorTest : TestBase<FibonacciGenerator>
    {
        private readonly CacheResolver _cacheResolver;

        public FibonacciGeneratorTest()
        {
            _cacheResolver = new CacheResolver(_ => new DummyCache());

            TestTarget = new FibonacciGenerator(_cacheResolver, _logger);
        }

        [Fact]
        public override void Constructor_ArgumentIsNull_Throws()
        {
            // act & assert
            AssertHelper.ThrowsArgumentNullException(Tuple.Create(_cacheResolver, _logger), (t) => new FibonacciGenerator(t.Item1, t.Item2));
        }

        [Theory]
        [FibonacciGeneratorTestData]
        public async Task GenerateFibonacci_ArgumentsAreOk_Generates(int firstIndex, int lastIndex, FibonacciResultDto expectedResult)
        {
            // act
            var actualResult = await TestTarget.GenerateFibonacci(firstIndex, lastIndex, false, 999999, 999999);

            // assert
            actualResult.Should().NotBeNull();
            actualResult.Status.Should().Be(Enums.GenerationResult.Ok);
            actualResult.FibonacciNumbers.Should().NotBeNullOrEmpty();
            Assert.True(actualResult.EqualsTo(expectedResult));
        }
    }
}
