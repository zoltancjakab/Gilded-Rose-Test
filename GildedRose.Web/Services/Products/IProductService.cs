using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GildedRose.Web.Services.Products.ViewModels;
using Microsoft.AspNetCore.Http.Features;

namespace GildedRose.Web.Services.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Adjusts the stock of a product by the adjusted amount.
        /// For the purposes of this test, I wrote this in so I could manually adjust a product for testing purposes.
        /// </summary>
        /// <param name="productId">The product being adjusted</param>
        /// <param name="adjustAmount">The adjusted amount, represented as an integer. This value can be negative if the product was stolen or miscounted.</param>
        /// <param name="reason">
        ///     (Optional) The reason why the adjustment was made.
        ///     Normally, this method would point to an adjustmentTable that keeps record of what adjustments were made to a product's stock. 
        /// </param>
        /// <returns>A Service Result indicating success or failure.</returns>
        Task<ServiceResult> AdjustProductAsync(Guid productId, int adjustAmount, string reason = null);

        /// <summary>
        /// Queries the product table for products that contain the search term. Searches in description and name.
        /// </summary>
        /// <param name="searchTerm">The search term. Can be partial or complete.</param>
        /// <param name="page">Which page to view. This way the database isn't hit with a 'query everything' command</param>
        /// <returns>A list of product models that match the search criteria, wrapped in a service result.</returns>
        
        Task<ServiceResult<List<ProductModel>>> SearchProductsAsync(string searchTerm = null);

        //alt method, with paging.
        //Task<ServiceResult<List<ProductModel>>> SearchProductsAsync(string searchTerm = null, int page = 1, int itemsPerPage = 10);

        /// <summary>
        /// Queries for a single product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>A product model wrapped in a service result.</returns>
        Task<ServiceResult<ProductModel>> GetProductAsync(Guid productId);
    }
}