using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    /// <summary>
    /// Response do login
    /// </summary>
    /// <param name="IsSuccess">Indica se o login foi bem-sucedido</param>
    /// <param name="Token">Token JWT (se sucesso)</param>
    /// <param name="Message">Mensagem de retorno</param>
    public record LoginResponse(
        /// <example>true</example>
        bool IsSuccess,
        /// <example>Bearer.YWRtaW5AdGVzdGUuY29tOjIwMjUtMDUtMjlUMTQ6MzA6MDBa</example>
        string? Token,
        /// <example>Login realizado com sucesso</example>
        string? Message
    );
}
