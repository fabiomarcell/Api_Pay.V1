using ApiPay.Middleware;
using Application.Interfaces;
using Domain.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace ApiPay.Routes
{
    public static class LoginRoute
    {
        public static void LoginEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (Domain.Requests.LoginRequest request, ILoginUseCase loginUseCase) =>
            {
                try
                {
                    var result = await loginUseCase.ExecuteAsync(request);

                    if (result.IsSuccess)
                        return Results.Ok(result);

                    return Results.BadRequest(result);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("Login")
            .WithSummary("Autenticação de usuário")
            .WithDescription("Realiza login e retorna token para autenticação")
            .WithTags("Autenticação")
            .Produces<LoginResponse>(200)
            .Produces(401)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Login de usuário";
                operation.Description = "Endpoint para autenticação de usuário. Retorna token em caso de sucesso.";
                return operation;
            });
        }
    }
}