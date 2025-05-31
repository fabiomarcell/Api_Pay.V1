using Domain.Requests;
using Infrastructure.Interfaces;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services.Provedores
{
    public class Provedor2Strategy : IProvedorStrategy
    {

        public void EfetuarCancelamento()
        {
            Console.WriteLine("Cancelamento efetuado via Provedor2.");
        }

        public void ConsultarPedido()
        {
            Console.WriteLine("Consulta de pedido via Provedor2.");
        }

        public async Task<PagamentoDto> EfetuarPagamento(PagamentoRequest request, HttpClient httpClient)
        {
            var json = JsonSerializer.Serialize(new
            {
                amount = request.Amount,
                currency = request.Currency,
                statementDescriptor = request.Description,
                paymentType = request.Type,

                card = new
                {
                    number = request.Number,
                    holder = request.HolderName,
                    cvv = request.Cvv,
                    expiration = request.ExpirationDate,
                    installmentNumber = request.Installments
                }
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://683a335543bb370a867218c6.mockapi.io/transactions", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var retorno = JsonSerializer.Deserialize<PagamentoProvedor2DTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return new PagamentoDto()
            {
                amount = retorno.amount,
                cardId = retorno.cardId,
                createdAt = retorno.date,
                currency = retorno.currency,
                currentAmount = retorno.originalAmount,
                description = retorno.statementDescriptor,
                id = retorno.id,
                originalAmount = retorno.originalAmount,
                paymentMethod = new PaymentMethod() { type = retorno.paymentType },
                status = retorno.status
            };
        }
    }
}
