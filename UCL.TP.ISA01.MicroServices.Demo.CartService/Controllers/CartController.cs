using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using UCL.TP.ISA01.MicroServices.Demo.ProductService;

namespace UCL.TP.ISA01.MicroServices.Demo.CartService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly IDistributedCache _distributedCache;

        private readonly ProductRepository _productRepository;

        public CartController(ILogger<CartController> logger, IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
            _productRepository = new ProductRepository();
        }

        [HttpGet("add/{productId}/quantity/{quantity}")]
        public IActionResult Add(int productId, int quantity)
        {
            var cart = LoadCartFromCache();
            cart.Add(productId, quantity);
            SaveCartToCache(cart);
            return Ok(cart);
        }

        [HttpGet]
        public Cart Get()
        {
            return LoadCartFromCache();
        }

        private void SaveCartToCache(Cart cart)
        {
            List<string> result = new List<string>();
            foreach(var c in cart.Items)
            {
                result.Add(c.ToString());
            }

            var cacheValue = string.Join(";", result);
            var cacheKey = "cart";

            _distributedCache.SetString(cacheKey, cacheValue);
        }

        private Cart LoadCartFromCache()
        {
            var cacheKey = "cart";
            var productsInCart = _distributedCache.GetString(cacheKey);

            if (string.IsNullOrEmpty(productsInCart))
                return new Cart();

            var products = _productRepository.GetAll();
            var cart = new Cart();

            var productsRaw = productsInCart.Split(';')
                .Select(x => x.Split(','));

            foreach (var product in productsRaw)
            {
                var id = int.Parse(product[0]);
                var quantity = int.Parse(product[1]);
                // var p = products.First(x => x.Id == id);

                cart.Add(id, quantity);
            }

            return cart;
        }
    }
}
