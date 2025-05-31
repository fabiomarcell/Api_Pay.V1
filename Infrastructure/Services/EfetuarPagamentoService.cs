using Domain.Requests;
using Infrastructure.Interfaces;
using Infrastructure.Services.Provedores;
using Shared.DTO;

namespace Infrastructure.Services
{
    public class EfetuarPagamentoService : IEfetuarPagamentoService
    {
        private readonly HttpClient _httpClient;

        
        public EfetuarPagamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagamentoDto> ExecuteAsync(PagamentoRequest request, string nomeProvedor)
        {
            var strategy = PagamentoFactory.Criar(nomeProvedor);
            var provedor = new OrquestradorDeProvedores(strategy);
            return await provedor.ExecutarPagamento(request, _httpClient);
        }
    }
}
