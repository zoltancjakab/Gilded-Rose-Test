using System;
using System.Collections.Generic;
using GildedRose.Web.Data.DatabaseModels.Transactions;
using Microsoft.AspNetCore.Identity;

namespace GildedRose.Web.Data.DatabaseModels.Users
{
    public class User : IdentityUser<Guid>
    {
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}