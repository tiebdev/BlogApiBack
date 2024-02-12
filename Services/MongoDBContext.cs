using MongoDB.Driver;
using BlogApi.Models;
using BlogApi.Configurations;
using Microsoft.Extensions.Options;

namespace BlogApi.Services
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Post");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comment");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Category");
        public IMongoCollection<User> Users => _database.GetCollection<User>("User");
    }
}
