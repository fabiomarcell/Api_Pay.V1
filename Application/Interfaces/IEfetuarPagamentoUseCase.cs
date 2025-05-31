using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces
{
    public interface IEfetuarPagamentoUseCase
    {
        Task<EfetuarPagamentoResponse> ExecuteAsync(PagamentoRequest request);
    }
}
