using DerivcoAssignment.Web.Enums;
using DerivcoAssignment.Web.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DerivcoAssignment.Web.ViewModels
{
    public class FibonacciRequestViewModel
    {
        [DefaultValue(10)]
        [Range(0, int.MaxValue, ErrorMessage = "Can't use negative number as index")]
        [Comparison(nameof(LastIndex), ComparisonType.LessOrEquals, ErrorMessage = "First index must be lesss or equal to last index")]
        public int FirstIndex { get; set; } = 10;

        [DefaultValue(15)]
        [Range(0, int.MaxValue, ErrorMessage = "Can't use negative number as index")]
        [Comparison(nameof(FirstIndex), ComparisonType.GreaterOrEquals, ErrorMessage = "Last index must be greater or equal to first index")]
        public int LastIndex { get; set; }

        public bool UseCache { get; set; }

        [DefaultValue(1000)]
        [Range(0, int.MaxValue, ErrorMessage = "Can't use negative number as time limit")]
        public int TimeLimit { get; set; }

        [DefaultValue(1000)]
        [Range(0, int.MaxValue, ErrorMessage = "Can't use negative number as memory limit")]
        public int MemoryLimit { get; set; }
    }
}
