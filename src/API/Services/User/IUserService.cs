using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<API.Model.User>> GetAllUsersAsync();
        Task<API.Model.User> GetUserByIdAsync(string id);
        Task<API.Model.User> UpdateUser(string id, Model.User user);
        Task<API.Model.User> CreateUser(Model.User user);
        Task DeleteUser(string id);
    }
}