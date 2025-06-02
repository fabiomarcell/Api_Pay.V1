using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities.Login
{
    public interface IUsuarioRepository
    {
        List<UsuarioModel> GetAll();
        void Inserir(UsuarioModel item);
        IEnumerable<UsuarioModel> Login(UsuarioModel item);
    }
}
