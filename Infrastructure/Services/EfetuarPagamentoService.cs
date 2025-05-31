using Domain.Requests;
using Infrastructure.Interfaces;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services
{
    public class EfetuarPagamentoService : IEfetuarPagamentoService
    {
        private readonly HttpClient _httpClient;
        public EfetuarPagamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> ExecuteAsync(PagamentoRequest request)
        {
            var json = JsonSerializer.Serialize(new
            {
                amount = request.Amount,
                currency = request.Currency,
                description = request.Description,
                paymentMethod = new
                {
                    type = request.Type,
                    card = new
                    {
                        number = request.Number,
                        holderName = request.HolderName,
                        cvv = request.Cvv,
                        expirationDate = request.ExpirationDate,
                        installments = request.Installments
                    }
                }
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://683a335543bb370a867218c6.mockapi.io/charges", content);

            var responseContent = await response.Content.ReadAsStringAsync();
            
            var retorno = JsonSerializer.Deserialize<PagamentoDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return retorno;
        }

    }
}
