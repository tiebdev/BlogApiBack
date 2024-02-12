using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/comments")]
    public class CommentController : ControllerBase
    {
        private readonly IPostService _postService;

        public CommentController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(string postId, [FromBody] CommentDto commentDto)
        {
            // Puedes validar si el postId es válido y si el post existe

            // Crear una instancia de Comment a partir del DTO
            var newComment = new Comment
            {
                AuthorId = User.Identity.Name, // Asumiendo que obtienes el ID del autor del contexto de autenticación
                Content = commentDto.Content,
                PublishDate = DateTime.UtcNow, // Establece la fecha de publicación al momento actual
                                               // No necesitas establecer PostId aquí si estás añadiendo el comentario a la lista de comentarios del post directamente
            };

            // Añadir el comentario al post
            await _postService.AddCommentToPost(postId, newComment);

            // Si llega hasta aquí, asume que la operación fue exitosa
            return Ok(newComment); // Devuelve el comentario que fue añadido
        }


    }

}
