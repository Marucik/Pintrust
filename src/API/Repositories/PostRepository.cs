using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using API.Mongo;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace API.Repositories
{
  public class PostRepository : IPostRepository
  {
    private readonly IMongoCollection<Post> _collection;
    private readonly MongoDbSettings _mongoDbSettings;
    public PostRepository(IMongoClient client, MongoDbSettings mongoDbSettings)
    {
      _mongoDbSettings = mongoDbSettings;
      var database = client.GetDatabase(_mongoDbSettings.DatabaseName);
      _collection = database.GetCollection<Post>("posts");
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
      return await _collection.AsQueryable().ToListAsync();
    }

    public async Task InsertAsync(Post entity)
    {
      await _collection.InsertOneAsync(entity);
    }
  }
}