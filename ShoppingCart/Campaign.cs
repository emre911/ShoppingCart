using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface ICampaign
    {
        Category Category { get; set; }
        double CampaignAmount { get; set; }
        int ProductQuantity { get; set; }
        DiscountType DiscountType { get; set; }
    }

    public class Campaign : ICampaign
    {
        public Category Category { get; set; }
        public double CampaignAmount { get; set; }
        public int ProductQuantity { get; set; }
        public DiscountType DiscountType { get; set; }

        public Campaign(Category category, double campaignAmount, int productQuantity, DiscountType discountType)
        {
            Category = category;
            CampaignAmount = campaignAmount;
            ProductQuantity = productQuantity;
            DiscountType = discountType;
        }
    }
}
