using Application.Interfaces;
using Domain.Entities;
using Domain.Requests;
using Domain.Responses;
using Microsoft.Extensions.Options;
using Shared.Configuration;
using Shared.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace Application.UseCases
{
    public class GerarLogUseCase : IGerarLogUseCase
    {
        public async Task<bool> ExecuteAsync(string content)
        {
            Logs.AdicionarLog(content);
            return true;
        }
    }
}