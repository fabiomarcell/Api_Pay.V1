using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests
{
    /// <summary>
    /// Request para autenticação de usuário
    /// </summary>
    /// <param name="Email">Email do usuário</param>
    /// <param name="Password">Senha do usuário</param>
    public record LoginRequest(
        /// <example>admin@teste.com</example>
        string Email,
        /// <example>123456</example>
        string Password
    );
}
