using ApiPay.Middleware;
using Application.Interfaces;
using Application.UseCases;
using Domain.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Configuration;
using Shared.Extensions;
using System;

namespace ApiPay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<Variaveis>(builder.Configuration.GetSection("Variaveis"));
            builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
            builder.Services.AddScoped<IGerarLogUseCase, GerarLogUseCase>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "Api Pay",
                    Version = "v1",
                    Description = "API acessando gateways de pagamento"
                });

                // Configuração JWT no Swagger
                c.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Insira o token no formato: Bearer {seu_token}"
                });

                c.AddSecurityRequirement(new()
                {
                    {
                        new()
                        {
                            Reference = new()
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pay v1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "Api Pay";
            });

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

            app.MapPost("/efetuar-pagamento", async () =>
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
            //.Produces<PaymentResponse>(400)
            .Produces(401)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Processar pagamento";
                operation.Description = "Endpoint para processar pagamentos com cartão de crédito. Requer token JWT válido.";
                return operation;
            });


            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}
