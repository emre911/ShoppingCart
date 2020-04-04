using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IShoppingCart
    {
        double totalCartAmount();
        void applyDiscounts(List<Campaign> discounts);
        void applyCoupons(List<Coupon> coupons);

        double getTotalAmountAfterDiscounts();
        double getCouponDiscount();
        double getCampaignDiscount();
        double getDeliveryCost();
    }

    public class ShoppingCart : IShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public List<Group> ItemGroups { get; set; }
        public double totalAmountAfterDiscounts { get; set; }
        public double appliedCampaignAmount { get; set; }
        public double appliedCouponAmount { get; set; }
        public double deliveryCost { get; set; }
        public int totalCategoryCount { get; set; }

        public double totalCartAmount()
        {
            double amount = 0.00;

            foreach (ShoppingCartItem item in Items)
            {
                amount += item.Product.Price * item.Quantity;
            }

            return amount;
        }

        public double getTotalAmountAfterDiscounts()
        {
            return totalCartAmount() - appliedCampaignAmount - appliedCouponAmount;
        }

        public double getCouponDiscount()
        {
            return appliedCouponAmount;
        }

        public double getCampaignDiscount()
        {
            return appliedCampaignAmount;
        }

        public double getDeliveryCost()
        {
            return deliveryCost;
        }

        public void print()
        {
            Trace.WriteLine("Category Name - Product Name - Quantity - Unit Price - Total Price - Total Discount");
            Trace.WriteLine("-----------------------------------------------------------------------------------");

            foreach (var itemGroup in ItemGroups)
            {
                foreach (ShoppingCartItem item in itemGroup.Products)
                {
                    Trace.WriteLine(itemGroup.Category.CategoryTitle + "    " + item.Product.ProductTitle + "    " +
                                    item.Quantity + "    " + item.Product.Price + "    " + (item.Product.Price * item.Quantity) + 
                                    "    " + (itemGroup.CategoryDiscount * ((item.Product.Price * item.Quantity) / itemGroup.CategoryAmount)));
                }         
            }

            Trace.WriteLine("-----------------------------------------------------------------------------------");
            Trace.WriteLine("");

            Trace.WriteLine("Sepet Tutarı: " + totalCartAmount().ToString("C",
                                  CultureInfo.CreateSpecificCulture("tr-TR")));
            Trace.WriteLine("Toplam Kampanya İndirim Tutarı: " + getCampaignDiscount().ToString("C",
                                  CultureInfo.CreateSpecificCulture("tr-TR")));
            Trace.WriteLine("Toplam Kupon İndirim Tutarı: " + getCouponDiscount().ToString("C",
                                  CultureInfo.CreateSpecificCulture("tr-TR")));
            Trace.WriteLine("İndirimler Sonrası Toplam Tutar: " + totalAmountAfterDiscounts.ToString("C",
                                  CultureInfo.CreateSpecificCulture("tr-TR")));
            Trace.WriteLine("Toplam Kargo Bedeli: " + getDeliveryCost().ToString("C",
                                  CultureInfo.CreateSpecificCulture("tr-TR")));
            Trace.WriteLine("Ödenecek Toplam Tutar: " + (totalAmountAfterDiscounts + deliveryCost).ToString("C",
                      CultureInfo.CreateSpecificCulture("tr-TR")));
        }

        public void applyDiscounts(List<Campaign> discounts)
        {
            double totalAmount = totalCartAmount();

            ItemGroups = Items.OrderBy(x => x.Product.Category.CategoryTitle)
                                   .GroupBy(x => new { Category = x.Product.Category
                                   })
                                   .Select(x => new Group{
                                       Category = x.Key.Category,
                                       Products = x.Select(s => new ShoppingCartItem (s.Product, s.Quantity)).ToList(),
                                       Count = x.Sum(s => s.Quantity),
                                       CategoryAmount = x.Sum(s => s.Quantity * (s.Product.Price))
                                   })
                                   .ToList();

            totalCategoryCount = ItemGroups.Count;

            foreach (var itemGroup in ItemGroups)
            {
                itemGroup.CategoryDiscount = applySuitableDiscount(discounts, itemGroup);           
                totalAmount -= itemGroup.CategoryDiscount;
                appliedCampaignAmount += itemGroup.CategoryDiscount;
            }
        }

        public void applyCoupons(List<Coupon> coupons)
        {
            double campaignsAppliedAmount = totalCartAmount() - appliedCampaignAmount;
            double couponDiscountAmount = 0;

            foreach (var coupon in coupons)
            {
                if (campaignsAppliedAmount >= coupon.MinPurchaseAmount)
                {
                    switch (coupon.DiscountType)
                    {
                        case DiscountType.Rate:
                            couponDiscountAmount = (coupon.CampaignAmount / 100) * campaignsAppliedAmount;
                            break;
                        case DiscountType.Amount:
                            couponDiscountAmount = coupon.CampaignAmount;
                            break;
                        default:
                            break;
                    }

                    if (couponDiscountAmount < campaignsAppliedAmount)
                    {
                        appliedCouponAmount += couponDiscountAmount;
                    }
                }
            }

            foreach (var itemgroup in ItemGroups)
            {
                itemgroup.CategoryDiscount += appliedCouponAmount * (itemgroup.CategoryAmount / totalCartAmount());
            }

            totalAmountAfterDiscounts = totalCartAmount() - appliedCampaignAmount - appliedCouponAmount;
        }

        internal double applySuitableDiscount(List<Campaign> discounts, Group itemGroup)
        {
            double appliedDiscountAmount = 0;
            double currentDiscountAmount = 0;

            foreach (var discount in discounts)
            {
                if (itemGroup.Category.CategoryTitle == discount.Category.CategoryTitle && itemGroup.Count >= discount.ProductQuantity)
                {
                    switch (discount.DiscountType)
                    {
                        case DiscountType.Rate:
                            currentDiscountAmount = (discount.CampaignAmount / 100) * itemGroup.CategoryAmount;
                            break;
                        case DiscountType.Amount:
                            currentDiscountAmount = discount.CampaignAmount;
                            break;
                        default:
                            break;
                    }

                    if (currentDiscountAmount < itemGroup.CategoryAmount && currentDiscountAmount > appliedDiscountAmount)
                    {
                        appliedDiscountAmount = currentDiscountAmount;
                    }
                }
            }
            return appliedDiscountAmount;
        }

    }

    public interface IShoppingCartItem
    {
        int Quantity { get; set; }
    }

    public class ShoppingCartItem : IShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public ShoppingCartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
