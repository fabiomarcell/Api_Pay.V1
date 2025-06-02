using Application.Interfaces;
using Application.UseCases;
using Infrastructure.Interfaces;
using Infrastructure.Policies;
using Infrastructure.Repository.DBContext;
using Infrastructure.Repository.Entities;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Configuration;

namespace ApiPay.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Variaveis>(configuration.GetSection("Variaveis"));
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddSingleton<IDbContext, MongoDbContext>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<IGerarLogUseCase, GerarLogUseCase>();
            services.AddScoped<IListarLogUseCase, ListarLogUseCase>();

            services.AddScoped<IEfetuarPagamentoUseCase, EfetuarPagamentoUseCase>();
            services.AddHttpClient<IEfetuarPagamentoService, EfetuarPagamentoService>()
                    .AddPolicyHandler(HttpPolicies.GetRetryPolicy());

            services.AddScoped<IConsultarPagamentoUseCase, ConsultarPagamentoUseCase>();
            services.AddHttpClient<IConsultarPagamentoService, ConsultarPagamentoService>()
                    .AddPolicyHandler(HttpPolicies.GetRetryPolicy());

            services.AddScoped<IEfetuarEstornoUseCase, EfetuarEstornoUseCase>();
            services.AddHttpClient<IEfetuarEstornoService, EfetuarEstornoService>()
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
