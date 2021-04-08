using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issuing_MicrosService_API.Model;
using System.Transactions;
using Issuing_MicrosService_API.Repository;
using Microsoft.AspNetCore.DataProtection;

namespace Issuing_MicrosService_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IDataProtector _protector;

        // GET: ProductController

        public ProductController(IProductRepository productRepository, IDataProtectionProvider provider)
        {
            _productRepository = productRepository;
            _protector = provider.CreateProtector(GetType().FullName);
        }

        [HttpGet]

        public IActionResult Get()
        {
            Product p = new Product();

            var products = _productRepository.GetProducts();
            var protectedData = _protector.Protect("Protected Data");
            p.Name = protectedData;
            p.Description = protectedData;
            p.Price = Convert.ToDecimal(protectedData);
            p.CategoryId = Convert.ToInt32(protectedData);

            return new OkObjectResult(products);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.GetProductByID(id);
            return new OkObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }
    }
}
