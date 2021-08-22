using DerivcoAssignment.Web.Enums;
using DerivcoAssignment.Web.Validation;
using DerivcoAssignment.Web.ViewModels;
using FluentAssertions;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace DerivcoAssignment.Web.Tests
{
    public class CompareAttributeTest
    {
        private const string OtherProperty = "LastIndex";

        private readonly ComparisonAttribute _lessAttribute;
        private readonly ComparisonAttribute _greaterAttribute;
        private readonly ComparisonAttribute _lessOrEqualsAttribute;
        private readonly ComparisonAttribute _greaterOrEqualsAttribute;
        private readonly ComparisonAttribute _equalsAttribute;

        public CompareAttributeTest()
        {
            _lessAttribute = new ComparisonAttribute(OtherProperty, ComparisonType.Less);
            _greaterAttribute = new ComparisonAttribute(OtherProperty, ComparisonType.Greater);
            _lessOrEqualsAttribute = new ComparisonAttribute(OtherProperty, ComparisonType.LessOrEquals);
            _greaterOrEqualsAttribute = new ComparisonAttribute(OtherProperty, ComparisonType.GreaterOrEquals);
            _equalsAttribute = new ComparisonAttribute(OtherProperty, ComparisonType.Equals);
        }

        [Fact]
        public void Constructor_SecondPropertyNotSpecified_Throws()
        {
            // act & assert
            Assert.Throws<ArgumentNullException>(() => new ComparisonAttribute(null, ComparisonType.Equals));
        }

        [Theory]
        [InlineData(0, 100, true)]
        [InlineData(10, 20, true)]
        [InlineData(500, 501, true)]
        [InlineData(100, 0, false)]
        [InlineData(10, 10, false)]
        [InlineData(501, 500, false)]
        public void Less_Validates(int firstIndex, int lastIndex, bool success)
        {
            // arrange
            var model = new FibonacciRequestViewModel { FirstIndex = firstIndex, LastIndex = lastIndex };
            var context = new ValidationContext(model);

            // act & assert
            if (success)
            {
                _lessAttribute.Validate(model.FirstIndex, context);
                context.Items.Should().BeEmpty();
            }
            else
            {
                Assert.Throws<ValidationException>(() => _lessAttribute.Validate(model.FirstIndex, context));
            }
        }

        [Theory]
        [InlineData(100, 0, true)]
        [InlineData(20, 10, true)]
        [InlineData(501, 500, true)]
        [InlineData(0, 100, false)]
        [InlineData(10, 10, false)]
        [InlineData(500, 501, false)]
        public void Greater_Validates(int firstIndex, int lastIndex, bool success)
        {
            // arrange
            var model = new FibonacciRequestViewModel { FirstIndex = firstIndex, LastIndex = lastIndex };
            var context = new ValidationContext(model);

            // act & assert
            if (success)
            {
                _greaterAttribute.Validate(model.FirstIndex, context);
                context.Items.Should().BeEmpty();
            }
            else
            {
                Assert.Throws<ValidationException>(() => _greaterAttribute.Validate(model.FirstIndex, context));
            }
        }

        [Theory]
        [InlineData(0, 100, true)]
        [InlineData(10, 20, true)]
        [InlineData(500, 501, true)]
        [InlineData(100, 0, false)]
        [InlineData(10, 10, true)]
        [InlineData(501, 500, false)]
        public void LessOrEquals_Validates(int firstIndex, int lastIndex, bool success)
        {
            // arrange
            var model = new FibonacciRequestViewModel { FirstIndex = firstIndex, LastIndex = lastIndex };
            var context = new ValidationContext(model);

            // act & assert
            if (success)
            {
                _lessOrEqualsAttribute.Validate(model.FirstIndex, context);
                context.Items.Should().BeEmpty();
            }
            else
            {
                Assert.Throws<ValidationException>(() => _lessOrEqualsAttribute.Validate(model.FirstIndex, context));
            }
        }

        [Theory]
        [InlineData(100, 0, true)]
        [InlineData(20, 10, true)]
        [InlineData(501, 500, true)]
        [InlineData(0, 100, false)]
        [InlineData(10, 10, true)]
        [InlineData(500, 501, false)]
        public void GreaterOrEquals_Validates(int firstIndex, int lastIndex, bool success)
        {
            // arrange
            var model = new FibonacciRequestViewModel { FirstIndex = firstIndex, LastIndex = lastIndex };
            var context = new ValidationContext(model);

            // act & assert
            if (success)
            {
                _greaterOrEqualsAttribute.Validate(model.FirstIndex, context);
                context.Items.Should().BeEmpty();
            }
            else
            {
                Assert.Throws<ValidationException>(() => _greaterOrEqualsAttribute.Validate(model.FirstIndex, context));
            }
        }

        [Theory]
        [InlineData(0, 100, false)]
        [InlineData(10, 20, false)]
        [InlineData(500, 501, false)]
        [InlineData(100, 0, false)]
        [InlineData(10, 10, true)]
        [InlineData(501, 500, false)]
        public void Equals_Validates(int firstIndex, int lastIndex, bool success)
        {
            // arrange
            var model = new FibonacciRequestViewModel { FirstIndex = firstIndex, LastIndex = lastIndex };
            var context = new ValidationContext(model);

            // act & assert
            if (success)
            {
                _equalsAttribute.Validate(model.FirstIndex, context);
                context.Items.Should().BeEmpty();
            }
            else
            {
                Assert.Throws<ValidationException>(() => _equalsAttribute.Validate(model.FirstIndex, context));
            }
        }
    }
}
