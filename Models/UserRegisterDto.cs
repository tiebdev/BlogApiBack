using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Utilice nombre y apellido.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Utilice nombre y apellido.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El nombre de usuario debe tener entre 4 y 20 caracteres.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string? Password { get; set; }

        
    }
}
