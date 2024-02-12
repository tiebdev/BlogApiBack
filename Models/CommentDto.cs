using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class CommentDto
    {
        [Required(ErrorMessage = "El autor del comentario es obligatorio.")]
        public string? AuthorId { get; set; } // Considera obtener esto del contexto de autenticación en lugar de la solicitud

        [Required(ErrorMessage = "El contenido del comentario es obligatorio.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "El comentario debe tener entre 1 y 1000 caracteres.")]
        public string? Content { get; set; }
    }

}
