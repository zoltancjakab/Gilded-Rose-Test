using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GildedRose.Web.Data.DatabaseModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GildedRose.Web.Models;
using GildedRose.Web.Models.Home;
using GildedRose.Web.Services;
using GildedRose.Web.Services.Products;
using GildedRose.Web.Services.Transactions;
using GildedRose.Web.Services.Transactions.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GildedRose.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IProductTransactionService _productTransactionService;


        public HomeController(ILogger<HomeController> logger, IProductService productService, IProductTransactionService productTransactionService, UserManager<User> userManager)
        {
            _logger = logger;
            _productService = productService;
            _productTransactionService = productTransactionService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.SearchProductsAsync();

            var model = new ProductSearchViewModel();

            if (result.IsSuccessful)
            {
    
                model.SearchResults = result.EntityResults;
                
                return View(model);
            }
            
            return View(model);
        }

        public async Task<IActionResult> SearchProducts(string searchTerm)
        {
            var result = await _productService.SearchProductsAsync(searchTerm);
            
            return PartialView("_ProductList", result.EntityResults);
        }

        [Route("product/{productId:guid}")]
        public async Task<IActionResult> ProductInformation(Guid productId)
        {
            if (productId == Guid.Empty) return await Index();

            var result = await _productService.GetProductAsync(productId);

            if (result.IsSuccessful)
            {
                return View(result.EntityResults);
            }
            return await Index();
        }

        [Route("product/{productId:guid}/purchase")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PurchaseProduct(Guid productId)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null)
            {
                return PartialView("TransactionResultModal", new ServiceResult<TransactionModel>()
                {
                    IsSuccessful = false,
                });
            }
            else
            {
                var transactionResult = await _productTransactionService.PurchaseProductAsync(productId, new Guid(idClaim.Value));

                return PartialView("TransactionResultModal", transactionResult);

            }
            
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
