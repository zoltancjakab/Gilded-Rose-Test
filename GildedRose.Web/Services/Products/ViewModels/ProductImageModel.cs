using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using GildedRose.Web.Data.DatabaseModels.Products;
using LinqKit.Utilities;

namespace GildedRose.Web.Services.Products.ViewModels
{
    public class ProductImageModel
    {
        

        public Guid Id { get; set; }

        public int Order { get; set; }

        public string ImageContent { get; set; }

        //Old code - we would use byte arrays to store our image, or store them directly on the file system. Since this is proof of concept, we will just use base64 encoded images.
        //public string ImageContent
        //{
        //    get { return "data:image/png;base64," + Convert.ToBase64String(_imageBytes, 0, _imageBytes.Length); }
        //    set { _imageBytes = Convert.FromBase64String(value.Replace("data:image/png;base64,", "")); }
        //}

        //private byte[] _imageBytes;

        public static Expression<Func<ProductImage, ProductImageModel>> Build()
        {
            return x => new ProductImageModel()
            {
                Id = x.Id,
                ImageContent = x.ImageFile.Base64ImageContent,
                Order = x.Order
            };
        }

    }
}