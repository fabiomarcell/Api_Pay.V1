using Domain.Requests;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IProvedorStrategy
    {
        Task<PagamentoDto> EfetuarPagamento(PagamentoRequest request, HttpClient httpClient);
        Task<PagamentoDto> EfetuarCancelamento(string id, EstornoRequest request, HttpClient httpClient);
        Task<PagamentoDto> ConsultarPedido(string id, HttpClient httpClient);
    }
}
