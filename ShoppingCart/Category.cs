using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface ICategory
    {
        string CategoryTitle { get; set; }
    }

    public class Category : ICategory
    {
        public string CategoryTitle { get; set; }

        public Category(string categoryTitle) {
            CategoryTitle = categoryTitle;
        }
    }
}
