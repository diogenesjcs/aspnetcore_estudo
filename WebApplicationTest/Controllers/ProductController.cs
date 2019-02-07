using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationTest.Domain;
using WebApplicationTest.Services;

namespace WebApplicationTest.Controllers
{
    [Route("api/products")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService { get; set; }

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.Read();
        }
        [HttpGet]
        [Route("filter/byName")]
        public IActionResult GetProductByName([FromQuery]string name)
        {
            var product = _productService.FindByName(name);
            if (product == null)
            {
                return NoContent();
            }
            return Ok(product);
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            return _productService.Read(id);
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (product == null)
                return BadRequest();

            _productService.Create(product);

            return CreatedAtAction("AddProduct", new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProductById(int id, [FromBody] Product prd)
        {

            if (prd == null || prd.Id != id)
                return BadRequest();

            _productService.Update(prd);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductByID(int id)
        {
            _productService.Delete(id);
            return new NoContentResult();
        }


    }
}