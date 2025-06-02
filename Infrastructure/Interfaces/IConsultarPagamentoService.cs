using Domain.Requests;
using Infrastructure.Repository.Entities.Provedores;
using Shared.DTO;

namespace Infrastructure.Interfaces
{
    public interface IConsultarPagamentoService
    {
        Task<PagamentoDto> ExecuteAsync(string id, ProvedorModel nomeProvedor);
    }
}
