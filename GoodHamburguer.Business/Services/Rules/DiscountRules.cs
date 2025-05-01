using GoodHamburguer.Business.Models;

namespace GoodHamburguer.Business.Services.Rules
{
    public class DiscountRules
    {
        public static decimal ApplyDiscount(Request request, decimal baseTotal)
        {
            bool hasFries = request.FriesId != null;
            bool hasDrink = request.SoftDrinkId != null;

            if (hasFries && hasDrink)
                return baseTotal * PricingConstants.DiscountComboFull;
            else if (hasDrink)
                return baseTotal * PricingConstants.DiscountComboDrink;
            else if (hasFries)
                return baseTotal * PricingConstants.DiscountComboFries;
            else
                return baseTotal;
        }

        public static class PricingConstants
        {
            public const decimal DiscountComboFull = 0.80m;
            public const decimal DiscountComboDrink = 0.85m;
            public const decimal DiscountComboFries = 0.90m;
        }
    }
}
