using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IGroup
    {
        Category Category { get; set; }
        List<ShoppingCartItem> Products { get;  set; }
        int Count { get; set; }
        double CategoryAmount { get; set; }
    }

    public class Group : IGroup
    {
        public Category Category { get; set; } 
        public List<ShoppingCartItem> Products { get; set; }
        public int Count { get; set; }
        public double CategoryAmount { get; set; }
        public double CategoryDiscount { get; set; }
    }
}
