using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    /// <summary>
    /// Request para processamento de pagamento
    /// </summary>
    /// <param name="Amount">Valor da transação</param>
    public record EstornoRequest(
        /// <example>100.50</example>
        decimal Amount
    );
}
