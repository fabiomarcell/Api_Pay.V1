using MongoDB.Driver;

namespace Infrastructure.Repository.DBContext
{
    public interface IDbContext
    {
        List<T> ListarTodos<T>();
        void Inserir<T>(T item);
        public List<T> ListarComQuery<T>(MongoDB.Driver.FilterDefinition<T> filtro);

        public void Atualizar<T>(FilterDefinition<T> filtro, Dictionary<string, object> camposAtualizar);
    }
}
