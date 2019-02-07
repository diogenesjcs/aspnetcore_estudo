using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Domain;

namespace WebApplicationTest.Services
{
    public interface ICategoryService
    {
        Category Create(Category category);
        void Update(Category category);
        void Delete(long id);
        IEnumerable<Category> Read();
        Category Read(long id);
        Category FindByName(string name);
    }
}
