using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly IEmprestimoService _emprestimoRepositorio;
        private readonly ILivroService _livroRepositorio;

        public EmprestimoController(IEmprestimoService emprestimoRepositorio, ILivroService livroRepositorio)
        {
            _emprestimoRepositorio = emprestimoRepositorio ??
                throw new ArgumentNullException(nameof(emprestimoRepositorio));

            _livroRepositorio = livroRepositorio ??
                throw new ArgumentNullException(nameof(livroRepositorio));
        }

        public IActionResult Cadastro(CadastroEmprestimoViewModel viewModel)
        {
            viewModel.Livros = _livroRepositorio.ListarTodos();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(int emprestimoId, CadastroEmprestimoViewModel viewModel)
        {            
            if(viewModel.Emprestimo.Id == 0)
            {
                _emprestimoRepositorio.Inserir(viewModel.Emprestimo);
            }
            else
            {
                _emprestimoRepositorio.Atualizar(emprestimoId, viewModel.Emprestimo);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            FiltrosEmprestimos objFiltro = null;

            if(!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosEmprestimos();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }

            return View(_emprestimoRepositorio.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int emprestimoId, CadastroEmprestimoViewModel viewModel, Emprestimo emprestimo)
        {
            _emprestimoRepositorio.ObterPorId(emprestimoId, emprestimo);

            viewModel.Livros = _livroRepositorio.ListarTodos();
            viewModel.Emprestimo = emprestimo;
            
            return View(viewModel);
        }
    }
}