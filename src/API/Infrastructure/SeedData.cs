using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.Infrastructure
{
    public class SeedData
    {
        public static async Task Seed(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            var users = new List<User>();
            for (var i = 1; i <= 5; i++)
            {
                var user = new User
                {
                    Id = i,
                    FirstName = $"User {i}",
                    LastName = $"User Last Name {i}",
                    Email = "user@email"
                };
                users.Add(user);
            }
            context.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}