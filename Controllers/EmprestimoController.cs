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

        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            Autenticacao.CheckLogin(this);

            viewModel.LivrosDisponiveis = _livroRepositorio.ListarDisponiveis();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(int Id, CadEmprestimoViewModel viewModel)
        {
            Autenticacao.CheckLogin(this);

            if (Id == 0)
                _emprestimoRepositorio.Inserir(viewModel.Emprestimo);
            else
                _emprestimoRepositorio.Atualizar(Id, viewModel.Emprestimo);

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            Autenticacao.CheckLogin(this);

            FiltroEmprestimos objFiltro = new FiltroEmprestimos();

            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltroEmprestimos
                {
                    Filtro = filtro,
                    TipoFiltro = tipoFiltro
                };
            }

            var listagem = _emprestimoRepositorio.ListarTodos(objFiltro);
            return View(listagem);
        }

        public IActionResult Edicao(int id)
        {
            var emprestimo = _emprestimoRepositorio.ObterPorId(id);
            var livrosDisponiveis = _livroRepositorio.ListarDisponiveis(emprestimo.LivroId);

            var vm = new CadEmprestimoViewModel()
            {
                Emprestimo = emprestimo,
                LivrosDisponiveis = livrosDisponiveis
            };

            return View(vm);
        }
    }
}