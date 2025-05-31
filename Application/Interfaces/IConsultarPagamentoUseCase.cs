using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces
{
    public interface IConsultarPagamentoUseCase
    {
        Task<EfetuarPagamentoResponse> ExecuteAsync(string id);
    }
}
