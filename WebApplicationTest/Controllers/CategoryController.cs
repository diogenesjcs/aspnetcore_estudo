using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Domain;
using WebApplicationTest.Services;

namespace WebApplicationTest.Controllers
{
    [Route("api/categories")]
    [Produces("application/json")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService { get; set; }

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public ActionResult AddCategory(Category cat)
        {
            if (cat == null)
                return BadRequest();

            var category = _categoryService.Create(cat);

            return CreatedAtAction(System.Reflection.MethodBase.GetCurrentMethod().Name, new { id = category.Id }, category);
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories(){
            return _categoryService.Read();
        }
    }
}
