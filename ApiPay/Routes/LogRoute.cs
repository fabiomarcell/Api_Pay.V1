using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using Application.Interfaces;
using Domain.Responses;

namespace ApiPay.Routes
{
    public static class LogRoute
    {
        public static void LogsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/logs", async (IListarLogUseCase listarLog) =>
            {
                try
                {
                    var result = await listarLog.ExecuteAsync();
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("Logs")
            .WithSummary("Logs do sistema")
            .WithDescription("Permite visualizar os logs em memória")
            .WithTags("Logs")
            .Produces<LogsResponse>(200)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Logs do sistema";
                operation.Description = "Retorna logs em caso de sucesso.";
                return operation;
            });
        }
    }
}