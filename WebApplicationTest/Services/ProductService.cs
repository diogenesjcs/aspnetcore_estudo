using System.Collections.Generic;
using System.Linq;
using WebApplicationTest.Domain;

namespace WebApplicationTest.Services
{
    public class ProductService : IProductService
    {
        private ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Delete(long id)
        {
            var product = _context.Products.SingleOrDefault(i => i.Id == id);

            _context.Products.Remove(product);
            _context.SaveChanges();

        }

        public Product FindByName(string name)
        {
            if (name!=null)
            {
                var nameSane = name.ToLower();
                var product = _context.Products.Where(p => p.Name.ToLower().Contains(nameSane)).FirstOrDefault();
                return product;
            }
            return null;
           
        }

        public IEnumerable<Product> Read()
        {
            return _context.Products.ToList();
        }

        public Product Read(long id)
        {
            var product = _context.Products.SingleOrDefault(i => i.Id == id);
            return product;
        }

        public void Update(Product prd)
        {
            var product = _context.Products.SingleOrDefault(i => i.Id == prd.Id);
            product.Name = prd.Name;
            product.Price = prd.Price;

            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
