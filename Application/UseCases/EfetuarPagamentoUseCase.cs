using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entities;
using Shared.DTO;
using System.Text.Json;

namespace Application.UseCases
{
    public class EfetuarPagamentoUseCase : IEfetuarPagamentoUseCase
    {
        private IEfetuarPagamentoService _efetuarPagamentoService;
        private IGerarLogUseCase _gerarLogUseCase;
        private IPagamentoRepository _pagamentoRepository;

        private readonly DadosProvedoresDTO[] PROVEDORES = new DadosProvedoresDTO[] {
            new DadosProvedoresDTO(){ Nome = "provedor 1" },
            new DadosProvedoresDTO(){ Nome = "provedor 2" }
        };
        public EfetuarPagamentoUseCase(IEfetuarPagamentoService efetuarPagamentoService, IGerarLogUseCase gerarLogUseCase, IPagamentoRepository pagamentoRepository)
        {
            _efetuarPagamentoService = efetuarPagamentoService;
            _gerarLogUseCase = gerarLogUseCase;
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(PagamentoRequest request)
        {
            var response = new PagamentoDto();
            var provedor = "";
            foreach (var item in PROVEDORES)
            {
                response = null; 
                try
                {
                    response = await _efetuarPagamentoService.ExecuteAsync(request, item.Nome);

                }
                catch(Exception ex)
                {
                    _gerarLogUseCase.ExecuteAsync("EfetuarPagamento >>>", $"Erro inesperado ao efetuar o pagamento pelo '{item.Nome}' : '{ex.Message}'", request);
                    continue;
                }

                if (response == null)
                {
                    _gerarLogUseCase.ExecuteAsync("EfetuarPagamento >>>", $"Não foi possível efetuar o pagamento pelo '{item.Nome}'", request);
                }
                else
                {
                    provedor = item.Nome;
                    break;
                }
            }

            _pagamentoRepository.Inserir(new PagamentoModel()
            {
                Provedor = response == null ? null : provedor,
                RequestBody = JsonSerializer.Serialize(request),
                Id = response.id
            });

            return response == null ? null : new EfetuarPagamentoResponse(response.id, response.status, response.originalAmount.ToString(), response.currency, response.cardId);
        }
    }
}
