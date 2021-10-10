using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Repositorios
{
    public interface IUsuarioService
    {
        void Inserir(Usuario usuario);
        void Atualizar(int id, Usuario usuario);
        void Excluir(int id);
        IEnumerable<Usuario> ListarTodosUsuarios();
        Usuario ObterPorId(int id);
        Usuario ObterLogin(Usuario usuario);
    }

    public sealed class UsuarioService : IUsuarioService
    {
        private readonly BibliotecaContext _contexto;

        public UsuarioService(BibliotecaContext contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }
        public void Inserir(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();
        }

        public void Atualizar(int id, Usuario usuario)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            var usuarioAtual = ObterPorId(id);

            usuarioAtual.NomeUsuario = usuario.NomeUsuario;
            usuarioAtual.Email = usuario.Email;

            //a senha é alterada no banco de dados somente se uma nova senha for inserida na edição de usuário.
            if (!string.IsNullOrEmpty(usuario.Senha))
                usuarioAtual.Senha = usuario.Senha;

            _contexto.SaveChanges();
        }

        public void Excluir(int id)
        {
            var usuario = new Usuario() { Id = id };

            _contexto.Usuarios.Attach(usuario);
            _contexto.Usuarios.Remove(usuario);

            _contexto.SaveChanges();
        }

        public IEnumerable<Usuario> ListarTodosUsuarios()
        {
            return _contexto.Usuarios
                .OrderBy(u => u.NomeUsuario).ToList();
        }

        public Usuario ObterPorId(int id)
        {
            return _contexto.Usuarios
                .FirstOrDefault(u => u.Id == id);
        }

        public Usuario ObterLogin(Usuario usuario)
        {
            return _contexto.Usuarios
                .FirstOrDefault(u =>
                    u.NomeUsuario == usuario.NomeUsuario
                 && u.Senha == usuario.Senha);
        }
    }
}
