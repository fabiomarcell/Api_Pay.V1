using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entities.Pagamento;
using Shared.DTO;

namespace Application.UseCases
{
    public class ConsultarPagamentoUseCase : IConsultarPagamentoUseCase
    {
        private IConsultarPagamentoService _consultarPagamentoService;
        private IGerarLogUseCase _gerarLogUseCase;
        private IPagamentoRepository _pagamentoRepository;

        public ConsultarPagamentoUseCase(IConsultarPagamentoService consultarPagamentoService, IGerarLogUseCase gerarLogUseCase, IPagamentoRepository pagamentoRepository)
        {
            _consultarPagamentoService = consultarPagamentoService;
            _gerarLogUseCase = gerarLogUseCase;
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(string id)
        {
            var response = new PagamentoDto();
            var pagamento = _pagamentoRepository.LocalizarPagamento(new PagamentoModel() { Id = id});

            if (!pagamento.Any())
            {
                _gerarLogUseCase.ExecuteAsync("ConsultarPagamento >>>", $"Pagamento não localizado no banco de dados", id);
                return null;
            }

            var provedor = pagamento.ElementAt(0).Provedor;
            try
            {
                response = await _consultarPagamentoService.ExecuteAsync(id, provedor);
            }
            catch(Exception ex)
            {
                _gerarLogUseCase.ExecuteAsync("ConsultarPagamento >>>", $"Erro inesperado ao efetuar a consulta do pagamento pelo '{provedor}' : '{ex.Message}'", id);
                return null;
            }

            if (response == null)
            {
                _gerarLogUseCase.ExecuteAsync("ConsultarPagamento >>>", $"Não foi possível efetuar a consulta do pagamento pelo '{provedor}'", id);
            }

            return response == null ? null : new EfetuarPagamentoResponse(response.id, response.status, response.originalAmount.ToString(), response.currency, response.cardId);
        }
    }
}
