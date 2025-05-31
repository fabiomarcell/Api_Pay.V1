using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;
using Shared.DTO;

namespace Application.UseCases
{
    public class EfetuarPagamentoUseCase : IEfetuarPagamentoUseCase
    {
        private IEfetuarPagamentoService _efetuarPagamentoService;
        private IGerarLogUseCase _gerarLogUseCase;
        private readonly DadosProvedoresDTO[] PROVEDORES = new DadosProvedoresDTO[] {
            new DadosProvedoresDTO(){ Nome = "provedor 1" },
            new DadosProvedoresDTO(){ Nome = "provedor 2" }
        };
        public EfetuarPagamentoUseCase(IEfetuarPagamentoService efetuarPagamentoService, IGerarLogUseCase gerarLogUseCase)
        {
            _efetuarPagamentoService = efetuarPagamentoService;
            _gerarLogUseCase = gerarLogUseCase;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(PagamentoRequest request)
        {
            var response = new PagamentoDto();
            foreach (var item in PROVEDORES)
            {
                response = await _efetuarPagamentoService.ExecuteAsync(request, item.Nome);

                if (response == null)
                {
                    _gerarLogUseCase.ExecuteAsync("EfetuarPagamento", $"Não foi possível efetuar o pagamento pelo '{item.Nome}'", request);
                }
                else
                {
                    break;
                }
            }
            return response == null ? null : new EfetuarPagamentoResponse(response.id, response.status, response.originalAmount.ToString(), response.currency, response.cardId);
        }
    }
}
