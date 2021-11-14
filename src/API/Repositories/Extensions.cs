using API.Domain;
using API.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace API.Repositories
{
  static public class Extensions
  {
    private static string _sectionName = "MongoDbSettings";

    public static void AddMongoDb(this IServiceCollection services)
    {
      using var serviceProvider = services.BuildServiceProvider();
      var configuration = serviceProvider.GetService<IConfiguration>();

      var mongoSettings = new MongoDbSettings();
      configuration.GetSection(_sectionName).Bind(mongoSettings);

      services.AddSingleton(mongoSettings);

      services.AddSingleton<IMongoClient>(x =>
      {
        return new MongoClient(mongoSettings.ConnectionString);
      });

      BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
      BsonClassMap.RegisterClassMap<Post>(cm =>
      {
        cm.AutoMap();
      });

      BsonClassMap.RegisterClassMap<User>(cm =>
      {
        cm.AutoMap();
      });

      services.AddSingleton<IPostRepository, PostRepository>();
      services.AddSingleton<IUserRepository, UserRepository>();
    }
  }
}