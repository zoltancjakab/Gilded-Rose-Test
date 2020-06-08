using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using GildedRose.Web.Data.DatabaseModels.Transactions;
using GildedRose.Web.Services.Products.ViewModels;
using LinqKit;

namespace GildedRose.Web.Services.Transactions.ViewModels
{
    /// <summary>
    /// A view model for the <see cref="Transaction"/> database model.
    /// This is a separation of class responsibilities. The database model exists to interact on the Database end, the View Model exists to interact with the UI.
    /// </summary>
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public ProductModel ProductPurchased { get; set; }

        public static Expression<Func<Transaction, TransactionModel>> Build()
        {
            var productBuilder = ProductModel.Build();

            return x => new TransactionModel()
            {
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                ProductPurchased = productBuilder.Invoke(x.Product)
            };
        }
    }
}