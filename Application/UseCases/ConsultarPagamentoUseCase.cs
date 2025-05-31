using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;
using Shared.DTO;

namespace Application.UseCases
{
    public class ConsultarPagamentoUseCase : IConsultarPagamentoUseCase
    {
        private IConsultarPagamentoService _consultarPagamentoService;
        private IGerarLogUseCase _gerarLogUseCase;
        private readonly DadosProvedoresDTO[] PROVEDORES = new DadosProvedoresDTO[] {
            new DadosProvedoresDTO(){ Nome = "provedor 1" },
            new DadosProvedoresDTO(){ Nome = "provedor 2" }
        };
        public ConsultarPagamentoUseCase(IConsultarPagamentoService consultarPagamentoService, IGerarLogUseCase gerarLogUseCase)
        {
            _consultarPagamentoService = consultarPagamentoService;
            _gerarLogUseCase = gerarLogUseCase;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(string id)
        {
            var response = new PagamentoDto();
            foreach (var item in PROVEDORES)
            {
                response = null;

                try
                {
                    response = await _consultarPagamentoService.ExecuteAsync(id, item.Nome);
                }
                catch(Exception ex)
                {
                    _gerarLogUseCase.ExecuteAsync("ConsultarPagamento >>>", $"Erro inesperado ao efetuar a consulta do pagamento pelo '{item.Nome}' : '{ex.Message}'", id);
                    continue;
                }

                if (response == null)
                {
                    _gerarLogUseCase.ExecuteAsync("ConsultarPagamento >>>", $"Não foi possível efetuar a consulta do pagamento pelo '{item.Nome}'", id);
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
