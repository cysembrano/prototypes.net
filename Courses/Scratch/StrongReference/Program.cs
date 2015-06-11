using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrongReference
{
    class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product("iPhone");
            Order order = new Order("ORD123");

            order.Product = product;
            order.Product.Order = order;

            order = null; //order is dead.
            Console.WriteLine(product.Order.Name); //but order in product is still alive.  this is strong reference.          
        }
    }

    public class Product
    {
        public readonly string Name;
        public Order Order { get; set; }

        public Product(string name)
        {
            Name = name;
        }

        
    }
    public class Order
    {
        public readonly string Name;
        public Product Product { get; set; }

        public Order(string name)
        {
            Name = name;
        }
    }
}
