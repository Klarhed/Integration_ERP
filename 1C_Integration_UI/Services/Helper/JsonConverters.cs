using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _1C_Integration_UI.Services.Helper
{
    public class EmptyStringToDecimalConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDecimal();
            }
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (decimal.TryParse(stringValue?.Replace(".", ","), out var value))
                {
                    return value;
                }
            }
            return 0m;
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}