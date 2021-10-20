using Microsoft.EntityFrameworkCore;
using ShopBridge.Context;
using ShopBridge.Helper;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShopBridge.Repositories
{
    public class DbReadRepository : IDbReadRepository
    {
        private readonly SqlDbContext _context;

        public DbReadRepository(SqlDbContext context)
        {
            _context = context;
        }
        public async Task<Response> ReadAllItemsFromDB()
        {
            List<Inventory> items = await _context.Inventories.ToListAsync();
            Response resp = ResponseDtoHelper.GetResponseDto(items);
            return resp;
        }

        public async Task<Response> ReadItemFromDb(int? id)
        {
            Inventory item = await _context.Inventories.FindAsync(id);
            Response resp = ResponseDtoHelper.GetResponseDto(new List<Inventory>() { item });

            return resp;
        }
    }
}