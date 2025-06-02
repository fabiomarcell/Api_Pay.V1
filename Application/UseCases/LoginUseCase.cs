using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Repository.Entities.Login;
using Microsoft.Extensions.Options;
using Shared.Configuration;
using Shared.Extensions;

namespace Application.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private Variaveis _config;
        private IUsuarioRepository _usuarioRepository;

        public LoginUseCase(IOptions<Variaveis> config, IUsuarioRepository usuarioRepository)
        {
            _config = config.Value;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
        {
            // Validação de entrada
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginResponse(false, null, "Email e senha são obrigatórios");
            }

            var potentialUsers = _usuarioRepository.Login(new UsuarioModel() { Usuario = request.Email});
            if (potentialUsers.Count() != 1)
            {
                return null;
            }

            if (ValidPassword(request.Password, potentialUsers.ElementAt(0).Senha))
            {
                var jwt = request.Email.GerarTokenJWT(_config.JWT);

                return new LoginResponse(true, jwt, "Login realizado com sucesso");
            }

            return null;
        }

        private bool ValidPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}