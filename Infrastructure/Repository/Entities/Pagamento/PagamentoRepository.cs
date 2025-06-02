using Infrastructure.Repository.DBContext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities.Pagamento
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly IDbContext _db;

        public PagamentoRepository(IDbContext context)
        {
            _db = context;
        }

        public void AtualizarPagamentoEstornado(PagamentoModel item)
        {
            var filtro = Builders<PagamentoModel>.Filter.Eq(p => p.Id, item.Id);

            var camposParaAtualizar = new Dictionary<string, object>
            {
                { "Amount", item.Amount },
                { "Status", item.Status },
                { "RequestBody", item.RequestBody }
            };

            _db.Atualizar(filtro, camposParaAtualizar);
        }

        public List<PagamentoModel> GetAll()
        {
            return _db.ListarTodos<PagamentoModel>();
        }

        public void Inserir(PagamentoModel item)
        {
            _db.Inserir(item);
        }

        public IEnumerable<PagamentoModel> LocalizarPagamento(PagamentoModel item)
        {
            var filtro = Builders<PagamentoModel>.Filter.Eq(u => u.Id, item.Id);
            return _db.ListarComQuery<PagamentoModel>(filtro);
        }
    }
}
