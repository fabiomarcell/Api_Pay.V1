using Domain.Requests;
using Infrastructure.Interfaces;
using Infrastructure.Services.Provedores;
using Shared.DTO;

namespace Infrastructure.Services
{
    public class ConsultarPagamentoService : IConsultarPagamentoService
    {
        private readonly HttpClient _httpClient;

        
        public ConsultarPagamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagamentoDto> ExecuteAsync(string id, string nomeProvedor)
        {
            var strategy = PagamentoFactory.Criar(nomeProvedor);
            var provedor = new OrquestradorDeProvedores(strategy);
            await provedor.ExecutarConsulta(id, _httpClient);
            return null;
        }
    }
}
