using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure;
using AutoMapper;
using MongoDB.Driver;

namespace API.Services.User.MongoDB
{
    public class MongoDBUserService : IUserService
    {
        private readonly IMongoClient _client;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<UserDb> _users;

        public MongoDBUserService(IMongoClient client, IDatabaseSettings settings, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;

            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<UserDb>("User");
        }

        public async Task<Model.User> CreateUser(Model.User user)
        {
            var newUser = _mapper.Map<UserDb>(user);
            newUser.Id = null;
            await _users.InsertOneAsync(newUser);
            return _mapper.Map<Model.User>(newUser);
        }

        public async Task DeleteUser(string id)
        {
            var user = GetUserByIdAsync(id);
            await _users.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Model.User>> GetAllUsersAsync()
        {
            var result = await _users.Find(x => true).ToListAsync();
            return _mapper.Map<List<Model.User>>(result);
        }

        public async Task<Model.User> GetUserByIdAsync(string id)
        {
            var result = await _GetUserByIdAsync(id);
            return _mapper.Map<Model.User>(result);
        }

        private async Task<UserDb> _GetUserByIdAsync(string id)
        {
            var result = await _users.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, "Not Found");
            }
            return result;
        }

        public async Task<Model.User> UpdateUser(string id, Model.User user)
        {
            var userDb = await _GetUserByIdAsync(id);
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            await _users.ReplaceOneAsync(x => x.Id == id, userDb);

            return _mapper.Map<Model.User>(userDb);
        }
    }
}