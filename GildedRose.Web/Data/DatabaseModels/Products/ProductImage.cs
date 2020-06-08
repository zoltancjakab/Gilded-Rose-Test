using System;
using System.ComponentModel.DataAnnotations.Schema;
using GildedRose.Web.Data.DatabaseModels.FileStore;

namespace GildedRose.Web.Data.DatabaseModels.Products
{
    public class ProductImage
    {
        /// <summary>
        /// Product Image Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The spot to display this image in (if multiple images present, otherwise, we'll default it to 1)
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// The Foreign Key Connection to Image Files
        /// </summary>
        [ForeignKey(nameof(ImageFile))]
        public Guid ImageFileId { get; set; }
        /// <summary>
        /// Lazy Loaded Image File
        /// </summary>
        public virtual ImageFile ImageFile { get; set; }
        /// <summary>
        /// The Foreign Key connection to Products
        /// </summary>
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Lazy Loaded Product
        /// </summary>
        public virtual Product Product { get; set; }
    }
}