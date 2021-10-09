using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public interface IEmprestimoService
    {
        void Inserir(Emprestimo emprestimo);
        void Atualizar(int id, Emprestimo emprestimo);
        ICollection<Emprestimo> ListarTodos(FiltroEmprestimos filtro);
        Emprestimo ObterPorId(int id);
    }

    public sealed class EmprestimoService : IEmprestimoService
    {
        private readonly BibliotecaContext _contexto;
        private readonly ILivroService _livroService;

        public EmprestimoService(BibliotecaContext contexto, ILivroService livroService)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _livroService = livroService ?? throw new ArgumentNullException(nameof(livroService));
        }

        public void Inserir(Emprestimo emprestimo)
        {
            if (emprestimo == null)
                throw new ArgumentNullException(nameof(emprestimo));

            _contexto.Emprestimos.Add(emprestimo);
            _contexto.SaveChanges();
        }

        public void Atualizar(int id, Emprestimo emprestimo)
        {
            if (id == emprestimo.Id)
                throw new ArgumentNullException(nameof(id));

            var emprestimoAtual = ObterPorId(id);

            emprestimoAtual.NomeUsuario = emprestimo.NomeUsuario;
            emprestimoAtual.Telefone = emprestimo.Telefone;
            emprestimoAtual.Devolvido = emprestimo.Devolvido;
            emprestimoAtual.DataDevolucao = emprestimo.DataDevolucao;
            emprestimoAtual.DataEmprestimo = emprestimo.DataEmprestimo;

            var novoLivro = ObterLivroPeloId(emprestimo.LivroId);
            emprestimoAtual.Livro = novoLivro;

            _contexto.SaveChanges();
        }

        public ICollection<Emprestimo> ListarTodos(FiltroEmprestimos filtro = null)
        {
            var resultado = _contexto.Emprestimos.AsQueryable();

            switch (filtro?.TipoFiltro)
            {
                case "Usuario":
                    resultado = resultado.Where(e => e.NomeUsuario.ToLower().Contains(filtro.Filtro.ToLower()));
                    break;
                case "Livro":
                    resultado = resultado.Where(e => e.Livro.Titulo.ToLower().Contains(filtro.Filtro.ToLower()));
                    break;
            }

            return resultado
                   .Include(x => x.Livro)
                   .OrderByDescending(e => e.DataDevolucao)
                   .ToList();
        }

        public Emprestimo ObterPorId(int id)
        {
            return _contexto.Emprestimos
                .Include(e => e.Livro)
                .Where(e => e.Id == id)
                .First();
        }

        private Livro ObterLivroPeloId(int livroId) => _livroService.ObterPorId(livroId);
    }
}