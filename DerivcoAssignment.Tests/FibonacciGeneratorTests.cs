using DerivcoAssignment.Core;
using FluentAssertions;
using Xunit;

namespace DerivcoAssignment.Tests
{
    public class FibonacciGeneratorTests
    {
        private readonly IFibonacciGenerator generator;

        public FibonacciGeneratorTests()
        {
            generator = new FibonacciGenerator();
        }

        [Theory]
        [InlineData(0, 1, "[\"1\"]")]
        [InlineData(0, 5, "[\"1\",\"2\",\"3\",\"5\",\"8\"]")]
        [InlineData(2, 4, "[\"3\",\"5\"]")]
        [InlineData(12, 15, "[\"377\",\"610\",\"987\"]")]
        public void GenerateFibonacci_ArgumentsAreOk_Generates(uint firstIndex, uint lastIndex, string expectedResult)
        {
            // act
            var actualResult = generator.GenerateFibonacci(firstIndex, lastIndex);

            // assert
            actualResult.Should().Be(expectedResult);
        }
    }
}
