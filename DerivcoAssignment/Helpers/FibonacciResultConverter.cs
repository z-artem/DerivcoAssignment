using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace DerivcoAssignment.Web.Helpers
{
    public class FibonacciResultConverter : JsonConverter<List<BigInteger>>
    {
        public override List<BigInteger> ReadJson(JsonReader reader, Type objectType, List<BigInteger> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("FibonacciResultConverter is expected to be used for serialization purposes only");
        }

        public override void WriteJson(JsonWriter writer, List<BigInteger> value, JsonSerializer serializer)
        {
            writer.WriteStartArray();

            foreach (var number in value)
            {
                writer.WriteRawValue(number.ToString());
            }

            writer.WriteEndArray();
        }
    }
}
