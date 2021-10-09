using System.Collections.Generic;

namespace Biblioteca.Models
{
    public sealed class CadEmprestimoViewModel
    {
        public Livro Livro { get; set; }
        public Emprestimo Emprestimo { get; set; }
        public IEnumerable<Livro> LivrosDisponiveis { get; set; }        
    }
}