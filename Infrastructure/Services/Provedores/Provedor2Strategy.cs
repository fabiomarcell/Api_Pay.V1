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

        public async Task<PagamentoDto> EfetuarCancelamento(string id, EstornoRequest request, HttpClient httpClient)
        {
            var json = JsonSerializer.Serialize(new
            {
                id = Guid.NewGuid(),
                amount = request.Amount
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync("https://683a335543bb370a867218c6.mockapi.io/transactions/{id}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (responseContent == "Invalid request")
            {
                return null;
            }

            var retorno = JsonSerializer.Deserialize<PagamentoDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return retorno;
        }

        public async Task<PagamentoDto> ConsultarPedido(string id, HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"https://683a335543bb370a867218c6.mockapi.io/transactions/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (responseContent == "Invalid request" || responseContent.Trim('"') == "Not found")
            {
                return null;
            }

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
