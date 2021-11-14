using System;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Mongo;
using MongoDB.Bson;
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

        public async Task AddFavourite(User user, Guid postId)
        {
            var favourites = user.Favourites;

            if (favourites.FirstOrDefault(x => x == postId) != Guid.Empty)
            {
                favourites.Remove(postId);
            }
            else
            {
                favourites.Add(postId);
            }


            var filter = Builders<User>.Filter.Eq("_id", user.Id);
            var update = Builders<User>.Update.Set("Favourites", favourites);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task AddReaction(User user, Guid postId, string reaction)
        {
            var reactions = user.Reactions;

            if (reactions.FirstOrDefault(x => x.PostId == postId) != null) throw new Exception("Already reacted.");

            reactions.Add(
                new Reaction
                {
                    PostId = postId,
                    UserReaction = reaction
                });

            var filter = Builders<User>.Filter.Eq("_id", user.Id);
            var update = Builders<User>.Update.Set("Reactions", reactions);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}