using Domain.Requests;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IEfetuarPagamentoService
    {
        Task<PagamentoDto> ExecuteAsync(PagamentoRequest request, string provedor);
    }
}
