using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GildedRose.Web.Data.DatabaseModels.Products;
using LinqKit;
using LinqKit.Utilities;

namespace GildedRose.Web.Services.Products.ViewModels
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string ItemNumber { get; set; }
        public double UnitCost { get; set; }
        public int StockCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductImageModel> Images { get; set; } = new List<ProductImageModel>();

        /// <summary>
        /// Our expression builder for products.
        /// Automatically cascades and builds Product Images as well.
        /// </summary>
        /// <returns>A linq expression to construct a Product.</returns>
        public static Expression<Func<Product, ProductModel>> Build()
        {
            var imageBuilder = ProductImageModel.Build();

            return x => new ProductModel
            {
                Id = x.Id,
                StockCount = x.StockCount,
                Description = x.Description,
                Name = x.Name,
                ItemNumber = x.ItemNumber,
                UnitCost = x.UnitCost,
                Images = x.ProductImages //Order by the order of the images.
                    .OrderBy(z=>z.Order)
                    .Select(z=> imageBuilder.Invoke(z))
                    .ToList()
            };
        }
    }
}