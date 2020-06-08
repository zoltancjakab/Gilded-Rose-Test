using System.Collections.Generic;
using GildedRose.Web.Services.Products.ViewModels;

namespace GildedRose.Web.Models.Home
{
    public class ProductSearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<ProductModel> SearchResults { get; set; }
    }
}