using BlogApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetAsync(string id);
        Task CreateAsync(Post newPost);
        Task UpdateAsync(string id, Post updatedPost);
        Task DeleteAsync(string id);
        Task AddCommentToPost(string postId, Comment newComment);
    }
}
