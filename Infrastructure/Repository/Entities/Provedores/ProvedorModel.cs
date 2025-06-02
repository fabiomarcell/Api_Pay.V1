using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Entities.Provedores
{
    public class ProvedorModel
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Nome { get; set; }
    }
}
