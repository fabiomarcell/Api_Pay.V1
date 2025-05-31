using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Microsoft.Extensions.Options;
using Shared.Configuration;
using Shared.Extensions;

namespace Application.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private Variaveis _config;

        public LoginUseCase(IOptions<Variaveis> config)
        {
            _config = config.Value;
        }

        public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
        {
            // Validação de entrada
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginResponse(false, null, "Email e senha são obrigatórios");
            }

            var jwt = request.Email.GerarTokenJWT(_config.JWT);

            return new LoginResponse(true, jwt, "Login realizado com sucesso");
        }


    }
}