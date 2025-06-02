using Domain.Requests;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entities.Provedores;
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

        public async Task<PagamentoDto> ExecuteAsync(string id, ProvedorModel nomeProvedor)
        {
            var strategy = PagamentoFactory.Criar(nomeProvedor._id.ToString());
            var provedor = new OrquestradorDeProvedores(strategy);
            return await provedor.ExecutarConsulta(id, _httpClient);
            
        }
    }
}
