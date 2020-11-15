using System;

namespace UCL.TP.ISA01.MicroServices.Demo.ProductService
{
    public class Product
    {

        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
