using System;
using System.Collections.Generic;

namespace GildedRose.Web.Data.DatabaseModels.Products
{
    public class Product
    {
        /// <summary>
        /// Internal Product ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Item Number / Barcode Number - can be a string sometimes.
        /// </summary>
        public string ItemNumber { get; set; }

        /// <summary>
        /// Item Name: I.E Broccoli, Tiki Doll, etc
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A Description of the product itself, with features or specifications.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The count of this stock.
        /// </summary>
        public int StockCount { get; set; }

        /// <summary>
        /// The cost of the product.
        /// </summary>
        public double UnitCost { get; set; }

        /// <summary>
        /// A list of images describing the product history.
        /// </summary>
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        /// <summary>
        /// A creation date for the product, so that we can potentially order by the most recent products if necessary. 
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

    }
}