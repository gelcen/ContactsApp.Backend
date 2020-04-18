using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        void Register(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        { 
            new User { Id = 1, FirstName = "Admin", 
                LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
            new User { Id = 2, FirstName = "Normal", 
                LastName = "User", Username = "user", Password = "user", Role = Role.User } 
        };

        private readonly AppSettings _appSettings;
        private readonly ContactsAppContext _context;

        public UserService(IOptions<AppSettings> appSettings, 
                           ContactsAppContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users
                .SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public void Register(string username, string password)
        {
            User user = new User
            {
                Username = username,
                FirstName = "firstName",
                LastName = "lastName",
                Role = Role.User,
                Password = password
            };

            if (_context.Users.Any(x => x.Username == user.Username))
            {
                throw new ArgumentException($"Username {user.Username} is already taken");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public User GetById(int id) 
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }
    }
}