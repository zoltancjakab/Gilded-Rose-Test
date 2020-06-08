using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Web.Data;
using GildedRose.Web.Data.DatabaseModels.Products;
using GildedRose.Web.Data.DatabaseModels.Users;
using GildedRose.Web.Services.Products;
using GildedRose.Web.Services.Transactions;
using GildedRose.Web.Services.Transactions.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Xunit;

namespace GildedRose.Tests
{
    
    public class TransactionTest
    {

        private readonly ProductTransactionService _productTransactionService;
        private readonly ProductService _productService;
        public TransactionTest()
        {

            var options = new DbContextOptionsBuilder<GildedRoseDbContext>()
                .UseInMemoryDatabase("DatabaseTest")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseModel(ModelGenerator.GenerateModel())
                .Options;
            var ctx = new GildedRoseDbContext(options);

            //ensures our database is seeded.
            ctx.Database.EnsureCreated();

            _productTransactionService = new ProductTransactionService(ctx);
            _productService = new ProductService(ctx);
        }

        [Fact]
        public async void Can_Purchase_In_Stock_Product()
        {
            var result = await _productTransactionService.PurchaseProductAsync(ModelGenerator.Product2.Id, ModelGenerator.User.Id);
            
            Assert.True(result.IsSuccessful, result.ErrorMessage);
            Assert.NotNull(result.EntityResults);
        }

        [Fact]
        public async void Cannot_Purchase_Out_Of_Stock_Product()
        {
            var result = await _productTransactionService.PurchaseProductAsync(ModelGenerator.Product1.Id, ModelGenerator.User.Id);

            Assert.False(result.IsSuccessful, result.ErrorMessage);
            Assert.Null(result.EntityResults); // no transaction
        }

        [Fact]
        public async void Unauthorized_Cannot_Purchase()
        {
            var result = await _productTransactionService.PurchaseProductAsync(ModelGenerator.Product1.Id, Guid.Empty);
            Assert.False(result.IsSuccessful, result.ErrorMessage);
            Assert.Null(result.EntityResults);
        }


    }



    public static class ModelGenerator
    {
        //some test data
        public static readonly User User = new User()
        {
            Id = new Guid("c851110e-80ca-43b4-b7ab-41d0b3f39193"),
        };
        public static readonly Product Product1 = new Product()
        {
            DateCreated = DateTime.UtcNow,
            Id = new Guid("a851110e-80ca-43b4-b7ab-41d0b3f39193"),
            StockCount = 0,
            ItemNumber = "1234",
            Name = "Out of Stock Product",
            UnitCost = 33.99,
            Description = "Out of Stock"
        };
        public static readonly Product Product2 = new Product()
        {
            DateCreated = DateTime.UtcNow,
            Id = new Guid("b851110e-80ca-43b4-b7ab-41d0b3f39193"),
            StockCount = 55,
            ItemNumber = "1234",
            Name = "In Stock Product",
            UnitCost = 33.99,
            Description = "In Stock"
        };

        public static IMutableModel GenerateModel()
        {
            var cnvtns = SqlServerConventionSetBuilder.Build();
            var modelBuilder = new ModelBuilder(cnvtns);

            modelBuilder.Entity<Product>().HasData(Product1, Product2);
            modelBuilder.Entity<User>().HasData(User);

            modelBuilder.FinalizeModel();

            return modelBuilder.Model;
        }
    }
}
