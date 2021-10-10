using Biblioteca.Models;
using Biblioteca.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioRepositorio;

        public UsuarioController(IUsuarioService usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio ??
                throw new ArgumentNullException(nameof(usuarioRepositorio));
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(int id, Usuario usuario)
        {
            if (usuario.Id == 0)
                _usuarioRepositorio.Inserir(usuario);
            else
                _usuarioRepositorio.Atualizar(id, usuario);

            return RedirectToAction("Listagem");
        }

        public IActionResult Atualizar(int id, Usuario usuario)
        {
            if (usuario is null)
                throw new ArgumentNullException(nameof(usuario));

            usuario = _usuarioRepositorio.ObterPorId(id);

            return View(usuario);
        }

        public IActionResult Listagem()
        {
            var usuarios = _usuarioRepositorio.ListarTodosUsuarios();
            return View(usuarios);
        }

        public IActionResult Excluir(int id)
        {
            _usuarioRepositorio.Excluir(id);

            return RedirectToAction("Listagem");
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            var loginUsuario = _usuarioRepositorio.ObterLogin(usuario);
            if (loginUsuario != null)
            {
                ViewBag.Mensagem = "Você está Logado!";
                HttpContext.Session.SetInt32("id", loginUsuario.Id);
                HttpContext.Session.SetString("NomeUsuario", loginUsuario.NomeUsuario);
                return Redirect("Listar");
            }
            else
            {
                ViewBag.Mensagem = "Falha no Login!";
                return View();
            }
        }
    }
}
