using System.Linq;
using System.Collections.Generic;
using System;

namespace Biblioteca.Models
{
    public interface ILivroService
    {
        void Inserir(Livro livro);
        void Atualizar(int livroId, Livro livro);
        ICollection<Livro> ListarTodos(FiltrosLivros filtro = null);
        ICollection<Livro> ListarDisponiveis();
        Livro ObterPorId(int livroId, Livro livro);
    }

    public sealed class LivroService : ILivroService
    {
        private readonly BibliotecaContext _contexto;

        public LivroService(BibliotecaContext contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public void Inserir(Livro livro)
        {
            if (livro == null)
                throw new ArgumentNullException(nameof(livro));

            _contexto.Livros.Add(livro);
            _contexto.SaveChanges();
        }

        public void Atualizar(int livroId, Livro livro)
        {
            if (livroId == livro.Id)
                throw new ArgumentNullException(nameof(livroId));

            _contexto.Livros.Any(l => l.Id == livroId);
            _contexto.SaveChanges();
        }

        public ICollection<Livro> ListarTodos(FiltrosLivros filtro)
        {
            IQueryable<Livro> consulta;

            if (filtro != null)
            {
                switch (filtro.TipoFiltro)
                {
                    case "Autor":
                        consulta = _contexto.Livros.Where(l => l.Autor.Contains(filtro.Filtro));
                        break;

                    case "Titulo":
                        consulta = _contexto.Livros.Where(l => l.Titulo.Contains(filtro.Filtro));
                        break;

                    default:
                        consulta = _contexto.Livros;
                        break;
                }
            }
            else
            {
                consulta = _contexto.Livros;
            }

            return consulta.OrderByDescending(l => l.Titulo).ToList();

        }

        public ICollection<Livro> ListarDisponiveis()
        {
            //busca os livros onde o id não está entre os ids de livro em empréstimo
            // utiliza uma subconsulta
            return
                _contexto.Livros
                .Where(l => !(_contexto.Emprestimos.Where(e => e.Devolvido == true).Select(e => e.LivroId).Contains(l.Id)))
                .ToList();
        }

        public Livro ObterPorId(int livroId, Livro livro)
        {
            if (livroId == livro.Id)
                throw new ArgumentNullException(nameof(livroId));

            return _contexto.Livros.FirstOrDefault(l => l.Id == livroId);
        }
    }
}
