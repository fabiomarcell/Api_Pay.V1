using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    /// <summary>
    /// Response dos logs
    /// </summary>
    /// <param name="Logs">Todos os logs armazenados em memoria</param>
    public record LogsResponse(
        string[] Logs
    );
}
