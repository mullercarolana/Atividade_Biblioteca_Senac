using System.Linq;
using System.Collections.Generic;
using System;

namespace Biblioteca.Models
{
    public interface ILivroService
    {
        void Inserir(Livro livro);
        void Atualizar(int id, Livro livro);
        ICollection<Livro> ListarTodos(FiltroLivros filtro = null);
        ICollection<Livro> ListarDisponiveis(int? livroAtualId = null);
        Livro ObterPorId(int id);
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

        public void Atualizar(int id, Livro livroDTO)
        {
            var livro = ObterPorId(id);

            livro.Autor = livroDTO.Autor;
            livro.Titulo = livroDTO.Titulo;
            livro.Ano = livroDTO.Ano;

            _contexto.SaveChanges();
        }

        public ICollection<Livro> ListarTodos(FiltroLivros filtro = null)
        {
            switch (filtro?.TipoFiltro)
            {
                case "Titulo":
                    return _contexto.Livros
                        .Where(l => l.Titulo.ToLower().Contains(filtro.Filtro.ToLower()))
                        .OrderBy(l => l.Titulo)
                        .ToList();

                case "Autor":
                    return _contexto.Livros
                    .Where(l => l.Autor.ToLower().Contains(filtro.Filtro.ToLower()))
                    .OrderBy(l => l.Titulo)
                    .ToList();
            }

            return _contexto.Livros
                   .OrderBy(l => l.Titulo)
                   .ToList();

        }

        public ICollection<Livro> ListarDisponiveis(int? livroAtualId = null)
        {
            var resultado = _contexto.Livros
                .Where(l => !_contexto.Emprestimos
                        .Where(e => !e.Devolvido)
                        .Select(e => e.LivroId)
                        .Contains(l.Id)
                )
                .ToList();

            if (livroAtualId != null)
                resultado.Add(_contexto.Livros.FirstOrDefault(l => l.Id == livroAtualId));

            return resultado.OrderBy(l => l.Titulo).ToList();
        }

        public Livro ObterPorId(int id)
        {
            return _contexto.Livros.FirstOrDefault(l => l.Id == id);
        }
    }
}
