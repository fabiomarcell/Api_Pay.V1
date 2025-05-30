using Application.Interfaces;
using Domain.Entities;
using Domain.Requests;
using Domain.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Config;
using NLog.Targets;
using Shared.Configuration;
using Shared.Extensions;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Application.UseCases
{
    public class GerarLogUseCase : IGerarLogUseCase
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> ExecuteAsync(string origem, string content, object obj = null)
        {
            ConfigureNLog();
            
            var logcontent = string.Concat(origem, content);
            if (obj != null)
                logcontent = string.Concat(logcontent, " >>> ", JsonSerializer.Serialize(obj));
            logger.Info(logcontent);

            return true;
        }

        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();

            // Console target
            var logConsole = new ConsoleTarget("logconsole")
            {
                Layout = "${longdate}|${level:uppercase=true}|PID=${processid}|${message}"
            };

            // File target
            var logFile = new FileTarget("logfile")
            {
                FileName = "logs/log.txt",
                Layout = "${longdate}|${level:uppercase=true}|PID=${processid}|${message}",
                KeepFileOpen = false,           
                ConcurrentWrites = true,        
                AutoFlush = true
            };

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logConsole);
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logFile);

            LogManager.Configuration = config;
        }
    }
}
