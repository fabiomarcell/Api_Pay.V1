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
            /*var filtro = Builders<PagamentoModel>.Filter.Eq(u => u.Provedor, item.Usuario) /*& 
                            Builders<UsuarioModel>.Filter.Eq(u => u.Senha, item.Senha)*/;
            //return _db.ListarComQuery<PagamentoModel>(filtro);
            return null;
        }
    }
}
