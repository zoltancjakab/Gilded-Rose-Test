using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GildedRose.Web.Data.DatabaseModels.Products;
using GildedRose.Web.Data.DatabaseModels.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GildedRose.Web.Data.DatabaseModels.Transactions
{
    public class Transaction
    {
        /// <summary>
        /// The Transaction Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The Date of the Transaction
        ///
        /// NOTE:
        /// In a live app, I would implement a secondary date. The first is when this transaction was created, the second is when the transaction has been processed / completed.
        /// This would play into 'shopping card' functionality.
        /// </summary>
        public DateTime TransactionDate { get; set; }
        
        /// <summary>
        /// Foreign Key
        /// The product that was interacted with in the transaction.
        ///
        /// NOTE:
        /// In a live app, we would change this to a list of line items.
        /// Line items would have Id, Item Id, Date Added, Amount Added.  
        /// </summary>
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        /// <summary>
        /// EF Core Lazy Loading the actual Product Entity.
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// The user who created the transaction.
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

    }
}