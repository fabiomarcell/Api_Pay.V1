using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPay.Middleware
{
    public class PaymentValidationMiddleware : IEndpointFilter
    {
        private IGerarLogUseCase _gerarLog;

        public PaymentValidationMiddleware(IGerarLogUseCase gerarLog)
        {
                _gerarLog = gerarLog;
        }

        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            // Busca o PaymentRequest nos argumentos
            /*var paymentRequest = context.Arguments
                .OfType<PaymentRequest>()
                .FirstOrDefault();
            var headers = context.HttpContext.Request.Headers;

            if (paymentRequest == null)
            {
                return Results.BadRequest("Request de pagamento não encontrado");
            }*/

            // Validações do middleware
            var validationErrors = new List<string>();

            var headers = context.HttpContext.Request.Headers["Authorization"];

            if (validationErrors.Any())
            {
                _gerarLog.ExecuteAsync("Pagamento >>> ", string.Join(" | ", validationErrors));
                return Results.BadRequest(new
                {
                    Message = "Não foi possível seguir com o pagamento",
                    Errors = validationErrors
                });
            }

            // Continua para o próximo filtro/endpoint se validação passou
            return await next(context);
        }
    }

}
