using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities
{
    public class PagamentoModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Id { get; set; }
        public string Provedor { get; set; }
        public string RequestBody { get; set; }
    }
}
