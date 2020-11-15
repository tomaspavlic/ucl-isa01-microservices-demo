using System;
using System.Collections.Generic;
using System.Linq;
using UCL.TP.ISA01.MicroServices.Demo.ProductService;

namespace UCL.TP.ISA01.MicroServices.Demo.CartService
{
    public class CartItem
    {
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Product.Id},{Quantity}";
        }
    }

    public class Cart
    {
        private ProductRepository _productRepository = new ProductRepository();
        public List<CartItem> Items => GetItems();
        private Dictionary<int, int> items = new Dictionary<int, int>();

        public List<CartItem> GetItems()
        {
            var products = _productRepository.GetAll();

            return items.Select(x => {
                var product = products.First(y => y.Id == x.Key);
                return new CartItem(product, x.Value);
            }).ToList();
        }

        public void Add(int product, int quantity)
        {
            if (items.TryGetValue(product, out var existingQuantity))
            {
                items[product] += quantity;
            }
            else
            {
                items[product] = quantity;
            }
        }
    }
}
