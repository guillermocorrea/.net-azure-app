using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API.Services.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        private async Task SaveChangesAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if (!success) throw new Exception("Problem saving changes");
        }

        public async Task<Model.User> CreateUser(Model.User user)
        {
            if (user == null)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new { user = "User cannot be null" });
            }
            var newUser = new Model.User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            await _context.Users.AddAsync(newUser);
            await SaveChangesAsync();
            return newUser;
        }

        public async Task DeleteUser(int id)
        {
            var user = await GetUserByIdAsync(id);
            _context.Remove(user);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Model.User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Model.User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { user = "Could not find user" });
            }
            return user;
        }

        public async Task<Model.User> UpdateUser(int id, Model.User user)
        {
            var dbUser = await GetUserByIdAsync(id);
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            _context.Update(dbUser);
            await SaveChangesAsync();
            return dbUser;
        }
    }
}