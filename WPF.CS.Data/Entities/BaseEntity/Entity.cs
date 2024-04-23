using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WPF.CS.Data.Entities.BaseEntity
{
    public class Entity<T>
    {
        [BsonRepresentation(BsonType.String)]
        public ObjectId _id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public T EntityId { get; set; }
    }
}
