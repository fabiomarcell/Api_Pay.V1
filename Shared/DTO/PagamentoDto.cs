using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class Card
    {
        public string number { get; set; }
        public string holderName { get; set; }
        public string cvv { get; set; }
        public string expirationDate { get; set; }
        public int installments { get; set; }
    }

    public class PaymentMethod
    {
        public string type { get; set; }
        public Card card { get; set; }
    }

    public class PagamentoDto
    {
        public DateTime createdAt { get; set; }
        public string status { get; set; }
        public int originalAmount { get; set; }
        public int currentAmount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public string cardId { get; set; }
        public string id { get; set; }
        public int amount { get; set; }
    }
}
