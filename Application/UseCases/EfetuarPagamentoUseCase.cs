using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;

namespace Application.UseCases
{
    public class EfetuarPagamentoUseCase : IEfetuarPagamentoUseCase
    {
        private IEfetuarPagamentoService _efetuarPagamentoService;

        public EfetuarPagamentoUseCase(IEfetuarPagamentoService efetuarPagamentoService)
        {
            _efetuarPagamentoService = efetuarPagamentoService;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(PagamentoRequest request)
        {
            var response = _efetuarPagamentoService.ExecuteAsync(request);
            return new EfetuarPagamentoResponse(Guid.NewGuid(), "APROVED", 100.23, "BRL", Guid.NewGuid());
        }
    }
}
