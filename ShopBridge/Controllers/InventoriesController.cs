using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Context;
using ShopBridge.Helper;
using ShopBridge.Models;
using ShopBridge.Repositories;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private IDbReadRepository _dbReadRepository;
        private IDbWriteRepository _dbWriteRepository;

        public InventoriesController(IDbReadRepository dbReadRepository, IDbWriteRepository dbWriteRepository)
        {
            _dbReadRepository = dbReadRepository;
            _dbWriteRepository = dbWriteRepository;
        }

        [HttpGet]
        public async Task<Response> GetAllItems()
        {
            try
            {
                Response resp = (Response)await _dbReadRepository.ReadAllItemsFromDB();
                return resp;
            }
            catch
            {
                return new Response() { Message = "Error While Processing", success = false, Items = new List<Inventory>() };
            }
        }

        [HttpGet("{id}")]
        public async Task<Response> GetInventory(int id)
        {
            try
            {
                Response resp = await _dbReadRepository.ReadItemFromDb(id);
                return resp;
            }
            catch
            {
                return new Response() { Message = "Error While Processing", success = false, Items = new List<Inventory>() };
            }
        }

        [HttpPut("{id}")]
        public async Task<Response> PutInventory(int id, [FromBody] Inventory inventory)
        {
            try
            {
                if (id != inventory.Id)
                {
                    return new Response() { Message = "Id Does Not Match", success = false, Items = new List<Inventory>() { inventory } };
                }

                await _dbWriteRepository.Update(inventory);
                return new Response() { Message = "Item Updated Sucessfully", success = true, Items = new List<Inventory>() { inventory } };
            }
            catch
            {
                return new Response() { Message = "Error While Processing", success = false, Items = new List<Inventory>() };
            }
        }

        [HttpPost]
        public async Task<Response> PostInventory([FromBody] Inventory inventory)
        {
            try
            {
                Response resp = await _dbWriteRepository.InsertInDataBase(inventory);
                return resp;
            }
            catch
            {
                return new Response() { Message = "Error While Processing", success = false, Items = new List<Inventory>() };
            }
        }

        [HttpDelete("{id}")]
        public async Task<Response> DeleteInventory(int id)
        {
            try
            {
                await _dbWriteRepository.DeleteItemFromDb(id);
                return new Response() { Message = "Item Deleted", success = true, Items = new List<Inventory>() };
            }
            catch
            {
                return new Response() { Message = "Error While Processing", success = false, Items = new List<Inventory>() };
            }
        }
    }
}
