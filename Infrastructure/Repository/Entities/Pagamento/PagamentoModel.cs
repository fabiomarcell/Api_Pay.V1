using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities.Pagamento
{
    public class PagamentoModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string Provedor { get; set; }
        public string RequestBody { get; set; }
    }
}
