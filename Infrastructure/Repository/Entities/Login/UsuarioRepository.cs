using Infrastructure.Repository.DBContext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities.Login
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbContext _db;

        public UsuarioRepository(IDbContext context)
        {
            _db = context;
        }

        public List<UsuarioModel> GetAll()
        {
            return _db.ListarTodos<UsuarioModel>();
        }

        public void Inserir(UsuarioModel user)
        {
            _db.Inserir<UsuarioModel>(user);
        }

        public IEnumerable<UsuarioModel> Login(UsuarioModel item)
        {
            var filtro = Builders<UsuarioModel>.Filter.Eq(u => u.Usuario, item.Usuario) /*& 
                            Builders<UsuarioModel>.Filter.Eq(u => u.Senha, item.Senha)*/;
            return _db.ListarComQuery<UsuarioModel>(filtro);
        }
    }
}
