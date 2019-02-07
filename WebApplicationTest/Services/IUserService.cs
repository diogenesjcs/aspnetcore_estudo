using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Domain;

namespace WebApplicationTest.Services
{
    public interface IUserService
    {
        IEnumerable<User> Read();
        User Authenticate(string username, string password);
    }
}
