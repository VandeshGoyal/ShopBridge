using ShopBridge.Helper;
using ShopBridge.Models;
using System.Threading.Tasks;

namespace ShopBridge.Repositories
{
    public interface IDbWriteRepository
    {
        Task<Response> InsertInDataBase(Inventory inventory);
        Task DeleteItemFromDb(int? id);
        Task Update(Inventory inventory);
    }
}
