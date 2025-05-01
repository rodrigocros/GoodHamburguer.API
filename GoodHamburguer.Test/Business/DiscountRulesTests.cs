using GoodHamburguer.Business.Models;
using GoodHamburguer.Business.Services.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburguer.Test.Business
{
    public class DiscountRulesTests
    {
        [Fact]
        public void ApplyDiscount_FullCombo_Returns20PercentOff()
        {
            var request = new Request { SandwichId = 1, FriesId = 4, SoftDrinkId = 5 };
            var total = DiscountRules.ApplyDiscount(request, 9.5m);
            Assert.Equal(7.6m, total);
        }

        [Fact]
        public void ApplyDiscount_SandwichAndDrink_Returns15PercentOff()
        {
            var request = new Request { SandwichId = 1, SoftDrinkId = 5 };
            var total = DiscountRules.ApplyDiscount(request, 7.5m);
            Assert.Equal(6.375m, total);
        }

        [Fact]
        public void ApplyDiscount_SandwichOnly_ReturnsNoDiscount()
        {
            var request = new Request { SandwichId = 1 };
            var total = DiscountRules.ApplyDiscount(request, 5.0m);
            Assert.Equal(5.0m, total);
        }



    }
}
