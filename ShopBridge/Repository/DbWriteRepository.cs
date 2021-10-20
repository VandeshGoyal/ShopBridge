using Microsoft.EntityFrameworkCore;
using ShopBridge.Context;
using ShopBridge.Helper;
using ShopBridge.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Repositories
{
    public class DbWriteRepository :IDbWriteRepository
    {
        private readonly SqlDbContext _context;

        public DbWriteRepository(SqlDbContext context)
        {
            _context = context;
        }
        public async Task<Response> InsertInDataBase(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            int i = await _context.SaveChangesAsync();
            if (i == 1)
            {
                return ResponseDtoHelper.GetResponseDto(new List<Inventory>() { inventory });
            }
            else
            {
                return new Response() { Message = "Item Not Inserted", success = false, Items = new List<Inventory>() { inventory } };
            }
        }

        public async Task DeleteItemFromDb(int? id)
        {
            Inventory inventory = _context.Inventories.Find(id);
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Inventory inventory)
        {
            _context.Entry(inventory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}