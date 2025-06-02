using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities
{
    public interface IPagamentoRepository
    {
        List<PagamentoModel> GetAll();
        void Inserir(PagamentoModel item);
        IEnumerable<PagamentoModel> LocalizarPagamento(PagamentoModel item);
    }
}
