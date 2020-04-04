using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart;

namespace ShoppingCartTest
{
    [TestClass]
    public class ShoppingCartUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ShoppingCartClass mainClass = new ShoppingCartClass();

            Category food = new Category("food");
            Category electronics = new Category("electronics");

            Product apple = new Product("Apple", 10.25, food);
            Product almonds = new Product("Almonds", 15.40, food);
            Product macbook = new Product("MacBook", 4990.50, electronics);
            Product iPad = new Product("iPad", 2999.90, electronics);

            ShoppingCartItem item1 = new ShoppingCartItem(apple, 2);
            ShoppingCartItem item2 = new ShoppingCartItem(almonds, 3);
            ShoppingCartItem item3 = new ShoppingCartItem(macbook, 1);
            ShoppingCartItem item4 = new ShoppingCartItem(iPad, 1);
            ShoppingCart.ShoppingCart cart = new ShoppingCart.ShoppingCart();

            cart.Items = new List<ShoppingCartItem>();
            cart.Items.Add(item1);
            cart.Items.Add(item2);
            cart.Items.Add(item3);
            cart.Items.Add(item4);

            Campaign discount1 = new Campaign(food, 20.00, 3, DiscountType.Rate);
            Campaign discount2 = new Campaign(food, 50.00, 5, DiscountType.Rate);
            Campaign discount3 = new Campaign(electronics, 50.00, 2, DiscountType.Amount);

            Coupon coupon1 = new Coupon(100.00, 10, DiscountType.Rate);

            List<Campaign> discounts = new List<Campaign>();
            discounts.Add(discount1);
            discounts.Add(discount2);
            discounts.Add(discount3);

            List<Coupon> coupons = new List<Coupon>();
            coupons.Add(coupon1);

            mainClass.Shopping(ref cart,ref discounts,ref coupons);
            
        }
    }
}
