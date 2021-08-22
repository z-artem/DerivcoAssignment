using DerivcoAssignment.Web.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DerivcoAssignment.Web.Validation
{
    public class ComparisonAttribute : ValidationAttribute
    {
        private readonly string _compareTo;
        private readonly ComparisonType _comparisonType;

        public ComparisonAttribute(string compareTo, ComparisonType comparisonType)
        {
            _compareTo = compareTo ?? throw new ArgumentNullException(nameof(compareTo));
            _comparisonType = comparisonType;
        }

        public override bool RequiresValidationContext => true;

        public override bool IsValid(object value) => false;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            object compareToValue = type.GetProperty(_compareTo).GetValue(instance, null);

            if (value == null || compareToValue == null) return ValidationResult.Success;

            bool result = false;
            int comparisonResult = ((IComparable)value).CompareTo(compareToValue);

            switch (_comparisonType)
            {
                case ComparisonType.Equals:
                    result = comparisonResult == 0;
                    break;
                case ComparisonType.Greater:
                    result = comparisonResult == 1;
                    break;
                case ComparisonType.Less:
                    result = comparisonResult == -1;
                    break;
                case ComparisonType.GreaterOrEquals:
                    result = comparisonResult >= 0;
                    break;
                case ComparisonType.LessOrEquals:
                    result = comparisonResult <= 0;
                    break;
                default:
                    break;
            }

            return result ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
