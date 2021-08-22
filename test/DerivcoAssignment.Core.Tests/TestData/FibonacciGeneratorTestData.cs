using DerivcoAssignment.Core.Dtos;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace DerivcoAssignment.Core.Tests.TestData
{
    public class FibonacciGeneratorTestData : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { 0, 1, new FibonacciResultDto { FibonacciNumbers = new List<System.Numerics.BigInteger> { 1 }, Status = Enums.GenerationResult.Ok } };
            yield return new object[] { 0, 5, new FibonacciResultDto { FibonacciNumbers = new List<System.Numerics.BigInteger> { 1, 2, 3, 5, 8 }, Status = Enums.GenerationResult.Ok } };
            yield return new object[] { 2, 4, new FibonacciResultDto { FibonacciNumbers = new List<System.Numerics.BigInteger> { 3, 5 }, Status = Enums.GenerationResult.Ok } };
            yield return new object[] { 12, 15, new FibonacciResultDto { FibonacciNumbers = new List<System.Numerics.BigInteger> { 377, 610, 987 }, Status = Enums.GenerationResult.Ok } };
        }
    }
}
