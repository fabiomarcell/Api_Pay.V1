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
    public class ListarLogUseCase : IListarLogUseCase
    {
        public async Task<LogsResponse> ExecuteAsync()
        {
            string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString());
            var logs = File.Exists($"{curDir}\\logs\\log.txt") ? File.ReadLines($"{curDir}\\logs\\log.txt").ToArray() : new string[] { };
            return new LogsResponse(logs);
        }
    }
}