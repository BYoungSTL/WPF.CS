using MongoDB.Driver;
using WPF.CS.Data.Interfaces.BaseService;

namespace WPF.CS.Data.CRUDs.BaseService
{
    public abstract class BaseService<T>(IMongoDatabase mongoDatabase) : IBaseService<T>
    {
        public virtual async Task AddAsync(T entity)
        {
            var collection = mongoDatabase.GetCollection<T>(typeof(T).Name);
            await collection.InsertOneAsync(entity);
        }
    }
}
