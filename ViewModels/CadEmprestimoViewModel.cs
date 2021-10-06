using System.Collections.Generic;

namespace Biblioteca.Models
{
    public sealed class CadastroEmprestimoViewModel
    {
        public ICollection<Livro> Livros { get; set; }
        public Emprestimo Emprestimo { get; set; }
    }
}