using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data prevista da efetivação do emprestimo é obrigatória.")]
        public DateTime DataEmprestimo { get; set; }


        [Required(ErrorMessage = "A data prevista para a devolução é obrigatória.")]
        public DateTime DataDevolucao { get; set; }


        [Required(ErrorMessage = "O nome de usuário do livro é obrigatório.")]
        [MaxLength(150)]
        public string NomeUsuario { get; set;}


        [Required(ErrorMessage = "O telefone é obrigatório. Insira somente o DDD e o número.")]
        [MaxLength(11)]
        public string Telefone { get; set; }


        [Required]
        public bool Devolvido { get; set; }

        [Required]
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
