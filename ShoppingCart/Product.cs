using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IProduct
    {
        string ProductTitle { get; set; }
        double Price { get; set; }
    }

    public class Product : IProduct
    {
        public string ProductTitle { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }

        public Product(string productTitle, double price, Category category)
        {
            ProductTitle = productTitle;
            Price = price;
            Category = category;
        }
    }
}
