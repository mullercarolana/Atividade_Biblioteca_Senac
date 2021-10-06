using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do livro é obrigatório.")]
        [MaxLength(200)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O autor do livro é obrigatório.")]
        [MaxLength(150)]
        public string Autor { get; set; }

        [Required(ErrorMessage = "O ano do livro é obrigatório.")]
        public int Ano { get; set; }
    }
}
