using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using WebApplicationTest.Domain;
using WebApplicationTest.Services;

namespace WebApplicationTest.UnitTests.Services
{
    [TestFixture]
    public class WebApplicationTest_ProductService
    {
        private readonly ProductService _productService;
        private readonly ApplicationDbContext _context;

        public WebApplicationTest_ProductService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Ecommerce");
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _productService = new ProductService(_context);
        }

        [Test]
        public void ReturnIfProductWasCreated()
        {
            var productCreated = _productService.Create(new Product() { Name = "teste" ,Price=15.5});
            Assert.IsNotNull(productCreated.Id);
            CollectionAssert.Contains(_context.Products, productCreated);
        }

        [Test]
        public void ReturnIfProductWasRemoved()
        {
            var productCreated = _productService.Create(new Product() { Name = "teste" ,Price=15.5});
            _productService.Delete(productCreated.Id);
            CollectionAssert.DoesNotContain(_context.Products, productCreated);
        }

        [Test]
        public void ReturnIfProductWasUpdated()
        {
            var productCreated = _productService.Create(new Product() { Name = "teste", Price = 15.5 });
            productCreated.Price = 16.5;
            productCreated.Name = "diferente";
            _productService.Update(productCreated);
            var productUpdated = _context.Products.FirstOrDefault(p => p.Id==productCreated.Id );
            Assert.AreEqual(16.5, productUpdated.Price);
            Assert.AreEqual("diferente", productUpdated.Name);
        }

        [Test]
        public void ReturnIfProductExists()
        {
            var productCreated = _productService.Create(new Product() { Name = "teste", Price = 15.5 });
            var productsInserted = _productService.Read();
            CollectionAssert.Contains(productsInserted, productCreated);
        }

    }
}