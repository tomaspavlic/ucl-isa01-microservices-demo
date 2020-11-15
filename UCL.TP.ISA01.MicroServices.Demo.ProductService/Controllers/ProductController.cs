using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UCL.TP.ISA01.MicroServices.Demo.ProductService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductRepository _productRepositoru;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            _productRepositoru = new ProductRepository();
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepositoru.GetAll();
        }
    }
}
