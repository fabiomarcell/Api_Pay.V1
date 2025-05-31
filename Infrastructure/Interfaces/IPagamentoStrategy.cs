using Domain.Requests;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IProvedorStrategy
    {
        Task<PagamentoDto> EfetuarPagamento(PagamentoRequest request, HttpClient httpClient);
        void EfetuarCancelamento();
        void ConsultarPedido();
    }
}
