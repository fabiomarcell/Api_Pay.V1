using Application.Interfaces;
using Application.UseCases;
using Infrastructure.Interfaces;
using Infrastructure.Policies;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;

namespace ApiPay.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Variaveis>(configuration.GetSection("Variaveis"));


            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IGerarLogUseCase, GerarLogUseCase>();
            services.AddScoped<IListarLogUseCase, ListarLogUseCase>();
            services.AddScoped<IEfetuarPagamentoUseCase, EfetuarPagamentoUseCase>();
            services.AddHttpClient<IEfetuarPagamentoService, EfetuarPagamentoService>()
                    .AddPolicyHandler(HttpPolicies.GetRetryPolicy());

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "Api Pay",
                    Version = "v1",
                    Description = "API acessando gateways de pagamento"
                });

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

            return services;
        }
    }

}
