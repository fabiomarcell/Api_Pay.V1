using ApiPay.Middleware;
using Application.Interfaces;
using Application.UseCases;
using Domain.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiPay.Routes
{
    public static class PagamentosRoute
    {
        public static void PagamentosEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/payments", async (Domain.Requests.PagamentoRequest request, IEfetuarPagamentoUseCase efetuarPagamentoUseCase) =>
            {
                try
                {
                    var result = await efetuarPagamentoUseCase.ExecuteAsync(request);

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {  
                    return Results.Problem(ex.Message);
                } 
            })
            .AddEndpointFilter<LoginValidationMiddleware>()
            .WithName("EfetuaPagamento")
            .WithSummary("Processar pagamento")
            .WithDescription("Processa pagamento com cartão de crédito (requer autenticação)")
            .WithTags("Pagamento")
            .Produces<EfetuarPagamentoResponse>(200)
            .Produces(401)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Processar pagamento";
                operation.Description = "Endpoint para processar pagamentos com cartão de crédito. Requer token JWT válido.";
                return operation;
            });


            app.MapGet("/payments/{id}", async (string id, IConsultarPagamentoUseCase consultarPagamentoUseCase) =>
            {
                try
                {
                    var result = await consultarPagamentoUseCase.ExecuteAsync(id);

                    return Results.Ok(result != null ? result : (new
                    {
                        Message = $"Não foi possível localizar o pagamento {id}.",
                    }));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .AddEndpointFilter<LoginValidationMiddleware>()
            .WithName("Lista Pagamento")
            .WithSummary("Detalhar Pagamento")
            .WithDescription("Exibe pagamento (requer autenticação)")
            .WithTags("Pagamento")
            .Produces<EfetuarPagamentoResponse>(200)
            .Produces(401)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Processar pagamento";
                operation.Description = "Endpoint para listar um pagamento. Requer token JWT válido.";
                return operation;
            });

            app.MapPut("/payments/{id}", async (string id, Domain.Requests.EstornoRequest request, IEfetuarEstornoUseCase estornoPagamentoUseCase) =>
            {
                try
                {
                    var result = await estornoPagamentoUseCase.ExecuteAsync(id, request);

                    return Results.Ok(result != null ? result : (new
                    {
                        Message = $"Não foi possível efetuar o estorno do pagamento {id}.",
                    }));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            //.AddEndpointFilter<LoginValidationMiddleware>()
            .WithName("ExtornaPagamento")
            .WithSummary("Extorna pagamento")
            .WithDescription("Extorna pagamento (requer autenticação)")
            .WithTags("Pagamento")
            .Produces<EfetuarPagamentoResponse>(200)
            .Produces(401)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Processar pagamento";
                operation.Description = "Endpoint para extornar pagamentos com cartão de crédito. Requer token JWT válido.";
                return operation;
            });
        }
    }
}