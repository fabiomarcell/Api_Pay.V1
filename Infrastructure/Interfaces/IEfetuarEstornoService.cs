using Domain.Requests;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IEfetuarEstornoService
    {
        Task<PagamentoDto> ExecuteAsync(string id, EstornoRequest request, string provedor);
    }
}
