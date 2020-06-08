using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GildedRose.Web.Services.Transactions;
using GildedRose.Web.Services.Transactions.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GildedRose.Web.Controllers
{
    public class AccountController : Controller
    {
        private IProductTransactionService _productTransactionService { get; set; }

        public AccountController(IProductTransactionService productTransactionService)
        {
            _productTransactionService = productTransactionService;
        }

        [Authorize]
        public async Task<IActionResult> TransactionHistory()
        {
            var results =
                await _productTransactionService.GetUserTransactionsHistoryAsync(
                    new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            var model = new List<TransactionModel>();

            if (results.IsSuccessful)
            {
                model = results.EntityResults;
            }

            return View(model);
        }
    }
}