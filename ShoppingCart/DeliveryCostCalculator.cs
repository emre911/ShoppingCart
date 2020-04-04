using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IDeliveryCostCalculator
    {
        double calculateFor(ShoppingCart cart);
    }

    public class DeliveryCostCalculator : IDeliveryCostCalculator
    {
        public double CostPerDelivery { get; set; }
        public double CostPerProduct { get; set; }
        public double FixedCost { get; set; }
        public double DeliveryCost { get; set; }

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }

        public double calculateFor(ShoppingCart cart)
        {
            DeliveryCost = (CostPerDelivery * cart.totalCategoryCount) + (CostPerProduct * cart.Items.Count) + FixedCost;
            cart.deliveryCost = DeliveryCost;
            return DeliveryCost;           
        }      

    }
}
