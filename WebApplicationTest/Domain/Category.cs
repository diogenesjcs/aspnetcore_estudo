using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTest.Domain
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products{ get; set; }
    }
}
