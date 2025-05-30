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
    /// <param name="Currency">Moeda em formato ISO 4217</param>
    /// <param name="Description">Descrição da transação</param>
    /// <param name="PaymentMethod">Informações do método de pagamento</param>
    public record PagamentoRequest(
        /// <example>100.50</example>
        decimal Amount,

        /// <example>BRL</example>
        string Currency,

        /// <example>Compra de produtos promocionais</example>
        string Description,

        PaymentMethodRequest PaymentMethod
    );

    /// <summary>
    /// Método de pagamento 
    /// </summary>
    /// <param name="Type">Tipo de pagamento</param>
    /// <param name="Card">Dados do cartão</param>
    public record PaymentMethodRequest(
        /// <example>cartão de crédito</example>
        string Type,

        CardRequest Card
    );

    /// <summary>
    /// Dados do cartão de crédito
    /// </summary>
    /// <param name="Number">Número do cartão</param>
    /// <param name="HolderName">Nome do titular</param>
    /// <param name="Cvv">Código de segurança</param>
    /// <param name="ExpirationDate">Data de validade (MM/YYYY)</param>
    /// <param name="Installments">Número de parcelas</param>
    public record CardRequest(
        /// <example>4111111111111111</example>
        string Number,

        /// <example>João Silva</example>
        string HolderName,

        /// <example>123</example>
        string Cvv,

        /// <example>12/2026</example>
        string ExpirationDate,

        /// <example>3</example>
        int Installments
    );

}
