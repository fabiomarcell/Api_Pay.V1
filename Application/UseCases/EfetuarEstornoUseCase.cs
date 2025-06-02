using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entities.Pagamento;
using Infrastructure.Repository.Entities.Provedores;
using Shared.DTO;
using System.Text.Json;

namespace Application.UseCases
{
    public class EfetuarEstornoUseCase : IEfetuarEstornoUseCase
    {
        private IEfetuarEstornoService _efetuarEstornoService;
        private IConsultarPagamentoService _consultarPagamentoService;
        private IPagamentoRepository _pagamentoRepository;
        private IGerarLogUseCase _gerarLogUseCase;
        
        public EfetuarEstornoUseCase(IConsultarPagamentoService consultarPagamentoService, IEfetuarEstornoService efetuarEstornoService, IGerarLogUseCase gerarLogUseCase, IPagamentoRepository pagamentoRepository)
        {
            _efetuarEstornoService = efetuarEstornoService;
            _gerarLogUseCase = gerarLogUseCase;
            _consultarPagamentoService = consultarPagamentoService;
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(string id, EstornoRequest request)
        {
            var response = new PagamentoDto();
            var provedor = new ProvedorModel();

            response = null;
            provedor = null;
            var pagamento = _pagamentoRepository.LocalizarPagamento(new PagamentoModel() { Id = id });

            if (!pagamento.Any())
            {
                _gerarLogUseCase.ExecuteAsync("EfetuarEstorno >>>", $"Pagamento não localizado no banco de dados", id);
                return null;
            }

            provedor = pagamento.ElementAt(0).Provedor;
            try
            {
                response = await _consultarPagamentoService.ExecuteAsync(id, provedor);
            }
            catch(Exception ex)
            {
                _gerarLogUseCase.ExecuteAsync("EfetuarEstorno >>>", $"Erro inesperado ao efetuar o estorno do pagamento pelo '{provedor}' : '{ex.Message}'", id);
                return null;
            }

            if (response == null)
            {
                _gerarLogUseCase.ExecuteAsync("EfetuarEstorno >>>", $"Não foi possível efetuar o estorno do pagamento pelo '{provedor}'", id);
            }

            if (response != null)
            {
                var responseEstorno = await _efetuarEstornoService.ExecuteAsync(id, request, provedor);
                if (response == null)
                {
                    _gerarLogUseCase.ExecuteAsync("EfetuarEstorno >>>", $"Não foi possível localizar o pagamento para estino pelo '{provedor}'", id);
                }

                _pagamentoRepository.AtualizarPagamentoEstornado(new PagamentoModel() { Id = id, Amount = Convert.ToDouble(responseEstorno.amount), Status = responseEstorno.status, RequestBody = JsonSerializer.Serialize(responseEstorno) });

            }

            return response == null ? null : new EfetuarPagamentoResponse(response.id, response.status, response.originalAmount.ToString(), response.currency, response.cardId);
        }
    }
}
