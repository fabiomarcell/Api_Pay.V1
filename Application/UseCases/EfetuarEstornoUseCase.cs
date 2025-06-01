using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;
using Shared.DTO;

namespace Application.UseCases
{
    public class EfetuarEstornoUseCase : IEfetuarEstornoUseCase
    {
        private IEfetuarEstornoService _efetuarEstornoService;
        private IConsultarPagamentoService _consultarPagamentoService;
        private IGerarLogUseCase _gerarLogUseCase;
        private readonly DadosProvedoresDTO[] PROVEDORES = new DadosProvedoresDTO[] {
            new DadosProvedoresDTO(){ Nome = "provedor 1" },
            new DadosProvedoresDTO(){ Nome = "provedor 2" }
        };
        public EfetuarEstornoUseCase(IConsultarPagamentoService consultarPagamentoService, IEfetuarEstornoService efetuarEstornoService, IGerarLogUseCase gerarLogUseCase)
        {
            _efetuarEstornoService = efetuarEstornoService;
            _gerarLogUseCase = gerarLogUseCase;
            _consultarPagamentoService = consultarPagamentoService;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(string id, EstornoRequest request)
        {
            var response = new PagamentoDto();
            var provedor = "";
            foreach (var item in PROVEDORES)
            {
                response = null;

                try
                {
                    response = await _consultarPagamentoService.ExecuteAsync(id, item.Nome);
                }
                catch(Exception ex)
                {
                    _gerarLogUseCase.ExecuteAsync("EfetuarEstorno >>>", $"Erro inesperado ao efetuar o estorno do pagamento pelo '{item.Nome}' : '{ex.Message}'", id);
                    continue;
                }

                if (response == null)
                {
                    _gerarLogUseCase.ExecuteAsync("EfetuarEstorno >>>", $"Não foi possível efetuar o estorno do pagamento pelo '{item.Nome}'", id);
                }
                else
                {
                    provedor = item.Nome;
                    break;
                }
            }

            if (response != null)
            {
                var responseEstorno = await _efetuarEstornoService.ExecuteAsync(id, request, provedor);
                if (response == null)
                {
                    _gerarLogUseCase.ExecuteAsync("EfetuarEstorno >>>", $"Não foi possível localizar o pagamento para estino pelo '{provedor}'", id);
                }
            }

            //com o provedor certo, faço o estorno

            return response == null ? null : new EfetuarPagamentoResponse(response.id, response.status, response.originalAmount.ToString(), response.currency, response.cardId);
        }
    }
}
