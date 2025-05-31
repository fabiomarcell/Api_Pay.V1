using ApiPay.Extensions;
using ApiPay.Routes;
using Microsoft.AspNetCore.Builder;
using System;

namespace ApiPay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices(builder.Configuration);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pay v1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "Api Pay";
            });

            app.LoginEndpoints();
            app.PagamentosEndpoints();
            app.LogsEndpoints();

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
