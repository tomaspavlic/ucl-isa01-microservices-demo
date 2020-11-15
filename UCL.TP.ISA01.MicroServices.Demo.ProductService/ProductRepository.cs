
namespace UCL.TP.ISA01.MicroServices.Demo.ProductService
{
    public class ProductRepository
    {
        public Product[] GetAll()
        {
            return new[]
            {
                new Product(1, "Apple iPhone 11 128GB", 19990.0),
                new Product(2, "Apple iPhone SE 64GB", 12990.0),
                new Product(3, "Apple iPhone 12 256GB", 29490.0),
                new Product(4, "Apple iPhone Xs 64GB", 18490.0)
            };
        }
    }
}