using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data prevista da efetiva��o do emprestimo � obrigat�ria.")]
        public DateTime DataEmprestimo { get; set; }


        [Required(ErrorMessage = "A data prevista para a devolu��o � obrigat�ria.")]
        public DateTime DataDevolucao { get; set; }


        [Required(ErrorMessage = "O nome de usu�rio do livro � obrigat�rio.")]
        [MaxLength(150)]
        public string NomeUsuario { get; set;}


        [Required(ErrorMessage = "O telefone � obrigat�rio. Insira somente o DDD e o n�mero.")]
        [MaxLength(11)]
        public string Telefone { get; set; }


        [Required]
        public bool Devolvido { get; set; }

        [Required]
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
