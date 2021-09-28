using System;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace API.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly IMongoCollection<User> _collection;
    private readonly MongoDbSettings _mongoDbSettings;

    public UserRepository(IMongoClient client, MongoDbSettings mongoDbSettings)
    {
      _mongoDbSettings = mongoDbSettings;
      var database = client.GetDatabase(_mongoDbSettings.DatabaseName);
      _collection = database.GetCollection<User>("users");
    }

    public async Task InsertAsync(User entity)
    {
      await _collection.InsertOneAsync(entity);
    }

    public async Task<User> GetByLogin(string login)
    {
      return await _collection.AsQueryable().Where(x => x.Login == login).FirstAsync();
    }

    public async Task<User> GetById(Guid id)
    {
      return await _collection.AsQueryable().Where(x => x.Id == id).FirstAsync();
    }
  }
}