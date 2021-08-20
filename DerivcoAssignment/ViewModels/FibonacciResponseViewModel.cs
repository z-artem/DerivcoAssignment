using DerivcoAssignment.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DerivcoAssignment.Web.ViewModels
{
    public class FibonacciResponseViewModel
    {
        public string FibonacciNumbers { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GenerationResult Status { get; set; }
    }
}
