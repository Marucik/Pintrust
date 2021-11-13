using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMongoCollection<User> _userCollection;
        private readonly MongoDbSettings _mongoDbSettings;
        public PostRepository(IMongoClient client, MongoDbSettings mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings;
            var database = client.GetDatabase(_mongoDbSettings.DatabaseName);
            _collection = database.GetCollection<Post>("posts");
        }

        public async Task AddReaction(Guid postId, string reaction)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);

            var update = reaction switch
            {
                "like" => Builders<Post>.Update.Inc("Likes", 1),
                "dislike" => Builders<Post>.Update.Inc("Dislikes", 1),
                _ => Builders<Post>.Update.Inc("Likes", 1)
            };

            await _collection.UpdateOneAsync(filter, update);
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