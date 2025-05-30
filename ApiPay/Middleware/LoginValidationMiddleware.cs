using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shared.Configuration;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPay.Middleware
{
    public class LoginValidationMiddleware : IEndpointFilter
    {
        private IGerarLogUseCase _gerarLog;
        private Variaveis _config;

        public LoginValidationMiddleware(IGerarLogUseCase gerarLog, IOptions<Variaveis> config)
        {
                _gerarLog = gerarLog;
                _config = config.Value;
        }

        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var header = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault() ?? "";

            header.Replace("Bearer", "").Trim().ValidarJWT(_config.JWT, out var error);

            if (!string.IsNullOrEmpty(error))
            {
                await _gerarLog.ExecuteAsync("Login >>> ", error, header);
                return Results.BadRequest(new
                {
                    Message = "Não foi possível seguir com a autenticação.",
                    Errors = error
                });
            }

            // Continua para o próximo filtro/endpoint se validação passou
            return await next(context);
        }
    }

}
