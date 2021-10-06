using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usu�rio � obrigat�rio.")]
        [MaxLength(150)]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O e-mail de usu�rio � obrigat�rio.")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha de usu�rio � obrigat�rio.")]
        [MaxLength(6)]
        public string Senha { get; set; }
    }
}
