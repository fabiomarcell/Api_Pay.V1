using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces
{
    public interface IEfetuarEstornoUseCase
    {
        Task<EfetuarPagamentoResponse> ExecuteAsync(string id, EstornoRequest request);
    }
}
