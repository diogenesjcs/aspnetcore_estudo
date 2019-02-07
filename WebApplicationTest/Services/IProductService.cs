using System.Collections.Generic;
using WebApplicationTest.Domain;

namespace WebApplicationTest.Services
{
    public interface IProductService
    {
        Product Create(Product product);
        void Update(Product product);
        void Delete(long id);
        IEnumerable<Product> Read();
        Product Read(long id);
        Product FindByName(string name);
    }
}
