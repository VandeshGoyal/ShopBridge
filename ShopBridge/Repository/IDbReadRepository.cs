using ShopBridge.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Repositories
{
    public interface IDbReadRepository
    {
        Task<Response> ReadAllItemsFromDB();
        Task<Response> ReadItemFromDb(int? id);
    }
}
