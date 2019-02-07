using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Domain;

namespace WebApplicationTest.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Category FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Read()
        {
            return _context.Categories.ToList();
        }

        public Category Read(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
