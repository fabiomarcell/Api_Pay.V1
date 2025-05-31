using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class CardProvedor2
    {
        [JsonConverter(typeof(ToStringConverter))]
        public string number { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string holder { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string cvv { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string expiration { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string installmentNumber { get; set; }
    }

    public class PagamentoProvedor2DTO
    {
        [JsonConverter(typeof(ToStringConverter))]
        public string date { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string status { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string amount { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string originalAmount { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string currency { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string statementDescriptor { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string paymentType { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string cardId { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string id { get; set; }
        public CardProvedor2 card { get; set; }
    }

    public class ToStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Converte qualquer tipo primitivo para string
            return reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => reader.GetDecimal().ToString(),
                JsonTokenType.True => "true",
                JsonTokenType.False => "false",
                JsonTokenType.Null => null,
                _ => throw new JsonException($"Não foi possível converter o tipo {reader.TokenType} para string.")
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
