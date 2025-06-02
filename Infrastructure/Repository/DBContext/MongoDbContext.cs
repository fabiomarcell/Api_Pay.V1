using MongoDB.Driver;


namespace Infrastructure.Repository.DBContext
{
    public class MongoDbContext : IDbContext
    {
        private IMongoDatabase _database;

        public MongoDbContext(IMongoDbSettings settings)
        {
            _database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        }

        public void Inserir<T>(T item)
        {
            var _collection = _database.GetCollection<T>(typeof(T).Name);
            _collection.InsertOne(item);
        }

        public void Atualizar<T>(FilterDefinition<T> filtro, Dictionary<string, object> camposAtualizar)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);

            var atualizacoes = new List<UpdateDefinition<T>>();

            foreach (var campo in camposAtualizar)
            {
                atualizacoes.Add(Builders<T>.Update.Set(campo.Key, campo.Value));
            }

            var updateDefinition = Builders<T>.Update.Combine(atualizacoes);

            collection.UpdateOne(filtro, updateDefinition);
        }

        public List<T> ListarTodos<T>()
        {
            var _collection = _database.GetCollection<T>(typeof(T).Name);
            return _collection.AsQueryable().ToList();
        }

        public List<T> ListarComQuery<T>(MongoDB.Driver.FilterDefinition<T> filtro)
        {
            var _collection = _database.GetCollection<T>(typeof(T).Name);
            return _collection.Find(filtro).ToList();
        }
    }
}
