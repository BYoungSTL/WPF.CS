using MongoDB.Bson.Serialization;

namespace WPF.CS.Data.Mappers
{
    public abstract class Mapper<T>
    {
        public static void EntityMapper()
        {
            BsonClassMap.RegisterClassMap<T>(classMap =>
            {
                classMap.AutoMap();
                classMap.SetIgnoreExtraElements(true);
            });
        }
    }
}