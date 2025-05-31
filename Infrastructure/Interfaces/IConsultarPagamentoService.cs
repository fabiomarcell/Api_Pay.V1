using Domain.Requests;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IConsultarPagamentoService
    {
        Task<PagamentoDto> ExecuteAsync(string id, string provedor);
    }
}
