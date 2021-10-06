using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public interface IEmprestimoService
    {
        void Inserir(Emprestimo emprestimo);
        void Atualizar(int emprestimoId, Emprestimo emprestimo);
        ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro);
        Emprestimo ObterPorId(int emprestimoId, Emprestimo emprestimo);
    }

    public sealed class EmprestimoService : IEmprestimoService
    {
        private readonly BibliotecaContext _contexto;

        public EmprestimoService(BibliotecaContext contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public void Inserir(Emprestimo emprestimo)
        {
            if (emprestimo == null)
                throw new ArgumentNullException(nameof(emprestimo));

            _contexto.Emprestimos.Add(emprestimo);
            _contexto.SaveChanges();
        }

        public void Atualizar(int emprestimoId, Emprestimo emprestimo)
        {
            if (emprestimoId == emprestimo.Id)
                throw new ArgumentNullException(nameof(emprestimoId));

            _contexto.Emprestimos.Any(e => e.Id == emprestimoId);
            _contexto.SaveChanges();
        }

        public ICollection<Emprestimo> ListarTodos(FiltrosEmprestimos filtro)
        {
            IQueryable<Emprestimo> consulta;

            if (filtro != null)
            {
                switch (filtro.TipoFiltro)
                {
                    case "Usuario":
                        consulta = _contexto.Emprestimos.Where(e => e.NomeUsuario.Contains(filtro.Filtro));
                        break;

                    case "Livro":
                        consulta = _contexto.Emprestimos.Where(e => e.Livro.Titulo.Contains(filtro.Filtro));
                        break;
                    default:
                        consulta = _contexto.Emprestimos;
                        break;
                }
            }
            else
            {
                consulta = _contexto.Emprestimos;
            }

            return _contexto.Emprestimos.OrderByDescending(e => e.Livro).ToList();
        }

        public Emprestimo ObterPorId(int emprestimoId, Emprestimo emprestimo)
        {
            if (emprestimoId == emprestimo.Id)
                throw new ArgumentNullException(nameof(emprestimoId));

            return _contexto.Emprestimos.FirstOrDefault(e => e.Id == emprestimoId);
        }
    }
}