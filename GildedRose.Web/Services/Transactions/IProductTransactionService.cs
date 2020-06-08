using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GildedRose.Web.Services.Transactions.ViewModels;

namespace GildedRose.Web.Services.Transactions
{
    public interface IProductTransactionService
    {
        /// <summary>
        /// A method to call wherein a user purchases a product, creating a transaction result as a byproduct (which tracks the 'sale')
        /// Async Method 
        /// </summary>
        /// <param name="productId">The product being purchased</param>
        /// <param name="userId">The user purchasing the product</param>
        /// <returns>Returns a <see cref="TransactionModel"/> wrapped in a <see cref="ServiceResult"/>. The Transaction result contains whether or not the purchase was successful.</returns>
        Task<ServiceResult<TransactionModel>> PurchaseProductAsync(Guid productId, Guid userId);

        /// <summary>
        /// Retrieves the transaction history for the user, between the appropriate starting and ending dates (if any)
        /// </summary>
        /// <param name="userId">The ID of the user we are retrieving the data for.</param>
        /// <param name="dateStart">(Optional) The starting date, in the retrieval range of the transaction records</param>
        /// <param name="dateEnd">(Optional) The ending date, in the retrieval range of the transaction records</param>
        /// <returns>Returns a list of type <see cref="TransactionModel"/>, wrapped in a <see cref="ServiceResult"/>. </returns>
        Task<ServiceResult<List<TransactionModel>>> GetUserTransactionsHistoryAsync(Guid userId, DateTime? dateStart = null, DateTime? dateEnd = null);
    }
}