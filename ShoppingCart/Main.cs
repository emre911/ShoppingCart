using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Common;

namespace ShoppingCart
{
    public class ShoppingCartClass
    {
        public static void Main(string[] args)
        {
        }

        public void Shopping(ref ShoppingCart cart, ref List<Campaign> discounts, ref List<Coupon> coupons)
        {
            cart.applyDiscounts(discounts);
            cart.applyCoupons(coupons);
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(Constants.costPerDelivery, Constants.costPerProduct, Constants.fixedCost);
            deliveryCostCalculator.calculateFor(cart);
            cart.print();
        }
    }
}
