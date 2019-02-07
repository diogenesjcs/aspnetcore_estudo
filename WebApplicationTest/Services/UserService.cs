using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplicationTest.Domain;
using WebApplicationTest.Helper;

namespace WebApplicationTest.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;

            return user;
        }

        public IEnumerable<User> Read()
        {
            return _context.Users.Select(user => new User()
            {
                FirstName = user.FirstName,
                Id = user.Id,
                Password = null,
                LastName = user.LastName,
                Token = user.Token,
                Username = user.Username
            }
            );
        }
    }
}
