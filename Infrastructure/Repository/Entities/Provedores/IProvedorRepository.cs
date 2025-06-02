using Infrastructure.Repository.Entities.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities.Provedores
{
    public interface IProvedorRepository
    {
        List<ProvedorModel> GetAll();
        void Inserir(ProvedorModel item);
    }
}
