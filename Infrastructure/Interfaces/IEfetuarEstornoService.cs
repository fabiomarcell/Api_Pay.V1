using Domain.Requests;
using Infrastructure.Repository.Entities.Provedores;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IEfetuarEstornoService
    {
        Task<PagamentoDto> ExecuteAsync(string id, EstornoRequest request, ProvedorModel provedor);
    }
}
