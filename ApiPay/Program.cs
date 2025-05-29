using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ApiPay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            // Login Endpoint
            app.MapPost("/api/login", async () =>
            {
                return Results.Ok("Teste");
            })
            .WithName("Login")
            .WithSummary("Autenticação de usuário")
            .WithDescription("Realiza login e retorna token para autenticação")
            .WithTags("Autenticação")
            .Produces(200)
            .Produces(400)
            .Produces(500)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Login de usuário";
                operation.Description = "Endpoint para autenticação de usuário. Retorna token em caso de sucesso.";
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
