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
