using DerivcoAssignment.Core.Tests.TestData;
using DerivcoAssignment.Web.Helpers;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using Xunit;

namespace DerivcoAssignment.Web.Tests
{
    public class FibonacciResponseConverterTest
    {
        [Fact]
        public void ReadJson_ArgumnetsAreOk_ThrowsNotImplemented()
        {
            // act & assert
            Assert.Throws<NotImplementedException>(() => JsonConvert.DeserializeObject<List<BigInteger>>("101", new FibonacciResponseConverter()));
        }

        [Theory]
        [FibonacciResponseConverterTestData]
        public void WriteJson_ArgumnetsAreOk_WritesJson(List<BigInteger> testList, string expectedResult)
        {
            // act
            var actualResult = JsonConvert.SerializeObject(testList, new FibonacciResponseConverter());

            // assert
            actualResult.Should().Be(expectedResult);
        }
    }
}
