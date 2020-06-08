using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.Web.Data;
using GildedRose.Web.Services.Products.ViewModels;
using GildedRose.Web.Services.Transactions.ViewModels;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GildedRose.Web.Services.Products
{
    public class ProductService : IProductService
    {

        private readonly GildedRoseDbContext _db;

        public ProductService(GildedRoseDbContext db)
        {
            _db = db;
        }


        public async Task<ServiceResult> AdjustProductAsync(Guid productId, int adjustAmount, string reason = null)
        {
            await using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    //check if product exists
                    var product = await _db.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
                    if (product == null)
                    {
                        return ServiceResult.Failure("No Such Product Exists.");
                    }
                    //decrement stock count.
                    product.StockCount += adjustAmount;

                    //save our database modifications
                    await _db.SaveChangesAsync();

                    //commit our database transaction
                    await transaction.CommitAsync();

                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.Failure(e.Message);
                }
            }
            return ServiceResult.Failure("Unable to purchase product.");
        }

        public async Task<ServiceResult<List<ProductModel>>> SearchProductsAsync(string searchTerm = null)
        {
            var productBuilder = ProductModel.Build();
            var query = _db.Products.AsExpandable();

            if (searchTerm != null)
                query = query.Where(x => x.Name.Contains(searchTerm) || x.Description.Contains(searchTerm));
            //if we implement paging, we would want a property for pages and property for items per page.
            //query.Skip(itemsPerPage * (page - 1)).Take(itemsPerPage);

            var data = await query
                .Select(x => productBuilder.Invoke(x))
                .ToListAsync();

            return ServiceResult<List<ProductModel>>.Success(data);
        }

        public async Task<ServiceResult<ProductModel>> GetProductAsync(Guid productId)
        {
            //if the product id is null, it doesn't exist. 
            if (productId == Guid.Empty) return ServiceResult.Failure<ProductModel>("No such product exists.");

            var productBuilder = ProductModel.Build();

            var data = await _db.Products.AsExpandable()
                .Where(x=> x.Id == productId)
                .Select(x => productBuilder.Invoke(x))
                .FirstOrDefaultAsync();

            //if there is no product returned, we return a service result failure.
            if (data == null)
            {
                ServiceResult.Failure<ProductModel>("No such product exists.");
            }
            return ServiceResult<ProductModel>.Success(data);
        }
    }
}