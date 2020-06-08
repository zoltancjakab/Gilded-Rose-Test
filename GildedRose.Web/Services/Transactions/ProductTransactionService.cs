using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRose.Web.Data;
using GildedRose.Web.Data.DatabaseModels.Transactions;
using GildedRose.Web.Services.Products.ViewModels;
using GildedRose.Web.Services.Transactions.ViewModels;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace GildedRose.Web.Services.Transactions
{
    public class ProductTransactionService : IProductTransactionService
    {
        private readonly GildedRoseDbContext _db;

        public ProductTransactionService(GildedRoseDbContext db)
        {
            _db = db;
        }


        public async Task<ServiceResult<TransactionModel>> PurchaseProductAsync(Guid productId, Guid userId)
        {
            await using var transaction = _db.Database.BeginTransaction();
            try
            {
                //get the user, ensure the user can purchase.
                var user = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    return ServiceResult.Failure<TransactionModel>("Unable to purchase product. User Id is null. Please log in.");
                }

                //check if product stock is above 0. If below 0, we do not allow it to be modified.
                var product = await _db.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
                

                if (product == null)
                {
                    return ServiceResult.Failure<TransactionModel>("Unable to purchase. Product does not exist or is out of stock.");
                }
                if (product.StockCount == 0)
                {
                    return ServiceResult.Failure<TransactionModel>("Unable to purchase. Product is out of stock.");
                }

                var productBuilder = ProductModel.Build();
                var productResult = await _db.Products.AsExpandable().Where(x => x.Id == productId).Select(x=> productBuilder.Invoke(x)).FirstOrDefaultAsync();
                //create a transaction object to track the user purchasing an object.
                //track UTC time, because its a universal format - we convert times on the front end using this standard because it uses the local time of the client browser. 
                //We'll use moment.js because it's MIT licensed and can convert our format really easily.
                var productTransaction = new Transaction()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProductId = productId,
                    TransactionDate = DateTime.UtcNow
                };

                _db.Transactions.Add(productTransaction);
                    
                //decrement stock count.
                product.StockCount -= 1;

                //save our database modifications
                await _db.SaveChangesAsync();

                //commit our database transaction
                await transaction.CommitAsync();

                //return a copy of the transaction for user display. This object will also be used to update the stock count locally.
                return ServiceResult<TransactionModel>.Success(new TransactionModel()
                {
                    Id = productTransaction.Id,
                    ProductPurchased = productResult,
                    TransactionDate = productTransaction.TransactionDate
                });
            }
            catch (Exception e)
            {
                //something went wrong. We shouldn't reach this point, but we will rollback and then display the error message.
                await transaction.RollbackAsync();
                //We can change the error message in production to read something more user friendly.
                return ServiceResult.Failure<TransactionModel>(e.Message); 
            }
            

        }

        public async Task<ServiceResult<List<TransactionModel>>> GetUserTransactionsHistoryAsync(Guid userId, DateTime? dateStart = null, DateTime? dateEnd = null)
        {
            if (userId == Guid.Empty) 
                return ServiceResult.Failure<List<TransactionModel>>("Unable to fetch user data.");
            if (dateStart != null && dateEnd != null && dateStart > dateEnd)
                return ServiceResult.Failure<List<TransactionModel>>("Please ensure the start date is earlier than your end date");
            
            var transactionBuilder = TransactionModel.Build();

            //query our transactions for the user
            //AsExpandable allows us to use LinqKit, a flexible and powerful package that helps with model building and expression invocation.
            var queryBuilder = _db.Transactions
                .AsExpandable()
                .Where(x => x.UserId == userId);

            //apply our date ranges if present
            if (dateStart != null)
                queryBuilder = queryBuilder.Where(x => x.TransactionDate > dateStart);
            
            if (dateEnd != null)
                queryBuilder = queryBuilder.Where(x => x.TransactionDate < dateEnd);

            //execute our query.
            var data = await queryBuilder
                .Select(x => transactionBuilder.Invoke(x))
                .ToListAsync();

            return ServiceResult<List<TransactionModel>>.Success(data);
            
        }
    }
}