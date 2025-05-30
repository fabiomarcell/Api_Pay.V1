using ApiPay.Middleware;
using Application.Interfaces;
using Domain.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace ApiPay.Routes
{
    public static class PagamentosRoute
    {
        public static void PagamentosEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/payments", async (Domain.Requests.PagamentoRequest request) =>
            {
                try
                {
                    //var result = await paymentUseCase.ExecuteAsync(request);

                    //if (result.IsSuccess)
                    //  return Results.Ok(result);

                    return Results.BadRequest();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .AddEndpointFilter<LoginValidationMiddleware>()
            .WithName("ProcessPayment")
            .WithSummary("Processar pagamento")
            .WithDescription("Processa pagamento com cartão de crédito (requer autenticação)")
            .WithTags("Pagamento")
            //.Produces<PaymentResponse>(200)
            .Produces(401)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Processar pagamento";
                operation.Description = "Endpoint para processar pagamentos com cartão de crédito. Requer token JWT válido.";
                return operation;
            });
        }
    }
}