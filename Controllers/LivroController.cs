using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroService _livroRepositorio;

        public LivroController(ILivroService livroRepositorio)
        {
            _livroRepositorio = livroRepositorio ??
                throw new ArgumentNullException(nameof(livroRepositorio));
        }

        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(int id, Livro livro)
        {
            if (livro.Id == 0)
                _livroRepositorio.Inserir(livro);
            else
                _livroRepositorio.Atualizar(id, livro);

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            Autenticacao.CheckLogin(this);

            FiltroLivros objFiltro = new FiltroLivros();

            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltroLivros
                {
                    Filtro = filtro,
                    TipoFiltro = tipoFiltro
                };
            }

            var listagem = _livroRepositorio.ListarTodos(objFiltro);

            return View(listagem);
        }

        public IActionResult Edicao(int id, Livro livro)
        {
            if (livro is null)
                throw new ArgumentNullException(nameof(livro));

            Autenticacao.CheckLogin(this);
            livro = _livroRepositorio.ObterPorId(id);
            return View(livro);
        }
    }
}