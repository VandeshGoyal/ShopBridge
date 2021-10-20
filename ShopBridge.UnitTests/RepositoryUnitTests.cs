using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using ShopBridge.Context;
using ShopBridge.Helper;
using ShopBridge.Models;
using ShopBridge.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class RepositoryUnitTests
    {
        [Fact]
        public async Task ReadFromDB_TestAsync()
        {
            var mocksetting = new DbContextOptionsBuilder<SqlDbContext>().UseInMemoryDatabase(databaseName: "ShopBridgeTestDB").Options;
            Inventory inventoryDto = new Inventory
            {
                Name = "TestItem",
                Discription = "TestDiscription",
                Price = 100
            };

            using (var context = new SqlDbContext(mocksetting))
            {
                context.Inventories.Add(inventoryDto);
                context.SaveChanges();
            }
            using (var context = new SqlDbContext(mocksetting))
            {
                DbReadRepository movieRepository = new DbReadRepository(context);
                Response resp = await movieRepository.ReadAllItemsFromDB();
                Assert.True(resp.Items.Count > 0);
                Assert.True(resp.success);
                Assert.True(resp.Message == "Operation Successfull");
                Assert.True(resp.Items[0].Name == "TestItem");
                Assert.True(resp.Items[0].Discription == "TestDiscription");
                Assert.True(resp.Items[0].Price == 100);
            }
            using (var context = new SqlDbContext(mocksetting))
            {
                DbReadRepository movieRepository = new DbReadRepository(context);
                Response resp = await movieRepository.ReadItemFromDb(1);
                Assert.True(resp.Items.Count > 0);
                Assert.True(resp.success);
                Assert.True(resp.Message == "Operation Successfull");
                Assert.True(resp.Items[0].Name == "TestItem");
                Assert.True(resp.Items[0].Discription == "TestDiscription");
                Assert.True(resp.Items[0].Price == 100);
            }
        }

        [Fact]
        public async Task WriteToDB_TestAsync()
        {
            var mocksetting = new DbContextOptionsBuilder<SqlDbContext>().UseInMemoryDatabase(databaseName: "ShopBridgeTestDB").Options;
            var context = new SqlDbContext(mocksetting);
            Inventory inventoryDto = new Inventory
            {
                Name = "TestItem",
                Discription = "TestDiscription",
                Price = 100
            };
            DbWriteRepository writeRepo = new DbWriteRepository(context);
            Response resp = await writeRepo.InsertInDataBase(inventoryDto);
            Assert.True(resp.Items.Count > 0);
            Assert.True(resp.success);
            Assert.True(resp.Message == "Operation Successfull");
            Assert.True(resp.Items[0].Name == "TestItem");
            Assert.True(resp.Items[0].Discription == "TestDiscription");
            Assert.True(resp.Items[0].Price == 100);
        }
        [Fact]
        public async Task DeleteFromDB_TestAsync()
        {
            var mocksetting = new DbContextOptionsBuilder<SqlDbContext>().UseInMemoryDatabase(databaseName: "ShopBridgeTestDB").Options;
            var context = new SqlDbContext(mocksetting);
            Inventory inventoryDto = new Inventory
            {
                Name = "TestItem",
                Discription = "TestDiscription",
                Price = 100
            };
            DbWriteRepository writeRepo = new DbWriteRepository(context);
            Response resp = await writeRepo.InsertInDataBase(inventoryDto);

            await writeRepo.DeleteItemFromDb(1);
            using (var context1 = new SqlDbContext(mocksetting))
            {
                DbReadRepository movieRepository = new DbReadRepository(context1);
                Response idResp = await movieRepository.ReadItemFromDb(1);
                Assert.True(idResp.Items.Count == 0);
                Assert.True(resp.success);
            }
        }
    }
}
