using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class Card
    {
        [JsonConverter(typeof(ToStringConverter))]
        public string number { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string holderName { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string cvv { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string expirationDate { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string installments { get; set; }
    }

    public class PaymentMethod
    {
        [JsonConverter(typeof(ToStringConverter))]
        public string type { get; set; }
        public Card card { get; set; }
    }

    public class PagamentoDto
    {
        [JsonConverter(typeof(ToStringConverter))]
        public string createdAt { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string status { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string originalAmount { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string currentAmount { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string currency { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string description { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string cardId { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string id { get; set; }
        [JsonConverter(typeof(ToStringConverter))]
        public string amount { get; set; }
    }
}
