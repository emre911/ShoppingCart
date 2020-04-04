using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface ICoupon
    {
        double MinPurchaseAmount { get; set; }
        double CampaignAmount { get; set; }        
        DiscountType DiscountType { get; set; }
    }

    public class Coupon : ICoupon
    {
        public double MinPurchaseAmount { get; set; }
        public double CampaignAmount { get; set; }
        public DiscountType DiscountType { get; set; }

        public Coupon(double minPurchaseAmount, double campaignAmount, DiscountType discountType)
        {
            MinPurchaseAmount = minPurchaseAmount;
            CampaignAmount = campaignAmount;
            DiscountType = discountType;
        }
    }
}
