using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O t�tulo do livro � obrigat�rio.")]
        [MaxLength(200)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O autor do livro � obrigat�rio.")]
        [MaxLength(150)]
        public string Autor { get; set; }

        [Required(ErrorMessage = "O ano do livro � obrigat�rio.")]
        public int Ano { get; set; }
    }
}
