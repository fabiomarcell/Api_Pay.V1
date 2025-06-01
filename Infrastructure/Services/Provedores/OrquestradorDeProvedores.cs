using Domain.Requests;
using Infrastructure.Interfaces;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Provedores
{
    public class OrquestradorDeProvedores
    {
        private readonly IProvedorStrategy _strategy;
        
        public OrquestradorDeProvedores(IProvedorStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<PagamentoDto> ExecutarPagamento(PagamentoRequest request, HttpClient httpClient)
        {
            var result = await _strategy.EfetuarPagamento(request, httpClient);
            return result;
        }

        public async Task<PagamentoDto> ExecutarCancelamento(string id, EstornoRequest request, HttpClient httpClient)
        {
            return await _strategy.EfetuarCancelamento(id, request, httpClient);
        }

        public async Task<PagamentoDto> ExecutarConsulta(string id, HttpClient httpClient)
        {
            return await _strategy.ConsultarPedido(id, httpClient);
        }
    }
}
