using BlogApi.Models;
using BlogApi.Configurations;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoCollection<Post> _postsCollection;

        public PostService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _postsCollection = mongoDatabase.GetCollection<Post>("Post");
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _postsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Post?> GetAsync(string id)
        {
            return await _postsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Post newPost)
        {
            newPost.PublishDate = DateTime.UtcNow;
            newPost.Categories = newPost.Categories != null && newPost.Categories.Any() ? newPost.Categories : new List<string>();          newPost.Tags = newPost.Tags != null && newPost.Tags.Any() ? newPost.Tags : new List<string>();
            newPost.ImageUrls = newPost.ImageUrls != null && newPost.ImageUrls.Any() ? newPost.ImageUrls : new List<string>();
            newPost.VideoUrls = newPost.VideoUrls != null && newPost.VideoUrls.Any() ? newPost.VideoUrls : new List<string>();
            newPost.FileUrls = newPost.FileUrls != null && newPost.FileUrls.Any() ? newPost.FileUrls : new List<string>();

            await _postsCollection.InsertOneAsync(newPost);
        }

        public async Task UpdateAsync(string id, Post updatedPost)
        {
            await _postsCollection.ReplaceOneAsync(x => x.Id == id, updatedPost);
        }

        public async Task DeleteAsync(string id)
        {
            await _postsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task AddCommentToPost(string postId, Comment newComment)
        {
            // Encuentra el post al que se agregará el comentario
            var post = await _postsCollection.Find<Post>(p => p.Id == postId).FirstOrDefaultAsync();
            if (post == null)
            {
                // Manejar el caso en que el post no se encuentra
                return;
            }

            // Establece la fecha de publicación del comentario en el momento actual
            newComment.PublishDate = DateTime.UtcNow; // Usar UtcNow para la hora universal
            newComment.PostId = postId; // Asegúrate de que el comentario esté vinculado al post correcto

            // Añade el nuevo comentario a la lista de comentarios del post
            post.Comments.Add(newComment);

            // Actualiza el post en la base de datos con el nuevo comentario añadido
            await _postsCollection.ReplaceOneAsync(p => p.Id == postId, post);
        }
    }
}
