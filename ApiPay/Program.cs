using ApiPay.Extensions;
using ApiPay.Middleware;
using ApiPay.Routes;
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
