using MongoDB.Driver;
using WPF.CS.Data.CRUDs.BaseService;
using WPF.CS.Data.Entities;
using WPF.CS.Data.Interfaces;

namespace WPF.CS.Data.CRUDs
{
    public class ImageService : IImageService//(IMongoDatabase mongoDatabase) : BaseService<Image>(mongoDatabase), IImageService
    {
    }
}
