using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public Usuario()
        {

        }

        public Usuario(string nomeUsuario, string senha)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [MaxLength(150)]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "O e-mail de usuário é obrigatório.")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha de usuário é obrigatório.")]
        [MaxLength(6)]
        public string Senha { get; set; }
    }
}
