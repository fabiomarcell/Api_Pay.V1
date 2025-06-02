using Infrastructure.Repository.DBContext;
using Infrastructure.Repository.Entities.Login;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities.Provedores
{
    public class ProvedorRepository : IProvedorRepository
    {
        private readonly IDbContext _db;

        public ProvedorRepository(IDbContext context)
        {
            _db = context;
        }

        public List<ProvedorModel> GetAll()
        {
            return _db.ListarTodos<ProvedorModel>();
        }

        public void Inserir(ProvedorModel user)
        {
            _db.Inserir<ProvedorModel>(user);
        }
    }
}
