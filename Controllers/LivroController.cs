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
                throw new ArgumentNullException(nameof(livroRepositorio)); ;
        }

        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(int livroId, Livro livro)
        {

            if(livro.Id == 0)
            {
                _livroRepositorio.Inserir(livro);
            }
            else
            {
                _livroRepositorio.Atualizar(livroId, livro);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
            Autenticacao.CheckLogin(this);
            FiltrosLivros objFiltro = null;

            if(!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosLivros();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }

            return View(_livroRepositorio.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int livroId, Livro livro)
        {
            Autenticacao.CheckLogin(this);
            livro = _livroRepositorio.ObterPorId(livroId, livro);
            return View(livro);
        }
    }
}