using Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IEfetuarPagamentoService
    {
        Task<object> ExecuteAsync(PagamentoRequest request);
    }
}
