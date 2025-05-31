using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class PagamentoDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string CardId { get; set; }
    }
}
