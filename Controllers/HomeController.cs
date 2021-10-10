using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Biblioteca.Repositorios;
using System;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarioService _usuarioRepositorio;


        public HomeController(IUsuarioService usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio ??
                throw new ArgumentNullException(nameof(usuarioRepositorio));
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            var loginUsuario = _usuarioRepositorio.ObterLogin(new Usuario(login, senha));

            if (loginUsuario == null)
            {
                ViewData["Erro"] = "Usuário ou senha inválidos.";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("user", loginUsuario.NomeUsuario);
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
