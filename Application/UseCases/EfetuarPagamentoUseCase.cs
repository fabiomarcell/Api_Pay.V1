using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Interfaces;
using Infrastructure.Repository.Entities;
using Infrastructure.Repository.Entities.Pagamento;
using Infrastructure.Repository.Entities.Provedores;
using Shared.DTO;
using System.Text.Json;

namespace Application.UseCases
{
    public class EfetuarPagamentoUseCase : IEfetuarPagamentoUseCase
    {
        private IEfetuarPagamentoService _efetuarPagamentoService;
        private IGerarLogUseCase _gerarLogUseCase;
        private IPagamentoRepository _pagamentoRepository;
        private IProvedorRepository _provedorRepository;
        
        public EfetuarPagamentoUseCase(IEfetuarPagamentoService efetuarPagamentoService, IGerarLogUseCase gerarLogUseCase, IPagamentoRepository pagamentoRepository, IProvedorRepository provedorRepository)
        {
            _efetuarPagamentoService = efetuarPagamentoService;
            _gerarLogUseCase = gerarLogUseCase;
            _pagamentoRepository = pagamentoRepository;
            _provedorRepository = provedorRepository;
        }

        public async Task<EfetuarPagamentoResponse> ExecuteAsync(PagamentoRequest request)
        {
            var response = new PagamentoDto();
            var provedor = new ProvedorModel();

            var provedores = _provedorRepository.GetAll();

            foreach (var item in provedores)
            {
                response = null;
                provedor = null;

                try
                {
                    response = await _efetuarPagamentoService.ExecuteAsync(request, item);

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
                    provedor = item;
                    break;
                }
            }

            _pagamentoRepository.Inserir(new PagamentoModel()
            {
                Provedor = response == null ? null : provedor,
                RequestBody = JsonSerializer.Serialize(request),
                Id = response.id,
                Amount = Convert.ToDouble(response.amount),
                Status = response.status

            });

            return response == null ? null : new EfetuarPagamentoResponse(response.id, response.status, response.originalAmount.ToString(), response.currency, response.cardId);
        }
    }
}
