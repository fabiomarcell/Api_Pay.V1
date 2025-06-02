using Domain.Requests;
using Infrastructure.Repository.Entities.Provedores;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IEfetuarPagamentoService
    {
        Task<PagamentoDto> ExecuteAsync(PagamentoRequest request, ProvedorModel provedor);
    }
}
