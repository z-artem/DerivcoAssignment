using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using Xunit.Sdk;

namespace DerivcoAssignment.Core.Tests.TestData
{
    public class FibonacciResponseConverterTestData : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { new List<BigInteger> { 0, 1 }, "[0,1]" };
            yield return new object[] { new List<BigInteger> { 0, 1, 1, 2, 3, 5 }, "[0,1,1,2,3,5]" };
            yield return new object[] { new List<BigInteger> { 1, 2, 3 }, "[1,2,3]" };
            yield return new object[] { new List<BigInteger> { 144, 233, 377, 610 }, "[144,233,377,610]" };
        }
    }
}
