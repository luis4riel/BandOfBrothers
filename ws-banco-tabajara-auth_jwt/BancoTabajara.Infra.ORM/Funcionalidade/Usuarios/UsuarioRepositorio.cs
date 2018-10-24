using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Usuarios;
using BancoTabajara.Infra.Contexto;
using System;
using System.Data.Entity;
using System.Linq;

namespace BancoTabajara.Infra.ORM.Funcionalidade.Usuarios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private BancoTabajaraDbContext _contexto;

        public UsuarioRepositorio(BancoTabajaraDbContext contexto) => _contexto = contexto;

        public Usuario Inserir(Usuario usuario)
        {
            _contexto.Usuarios.Attach(usuario);
            var newUsuario = _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();
            return newUsuario;
        }
        
        public IQueryable<Usuario> PegarTodos(int? limite = null)
        {
            if(limite != null)
                return _contexto.Usuarios.Take(Convert.ToInt32(limite)).OrderBy(c => c.Username);
            else
                return _contexto.Usuarios.OrderBy(c => c.Username);
        }

        public Usuario PegarPorId(int productId)
        {
            return _contexto.Usuarios.Find(productId);
        }

		public Usuario PegarPorNomeESenha(string nome, string senha)
		{
			return _contexto.Usuarios.Where( c => c.Username.Equals(nome) && c.Password.Equals(senha)).FirstOrDefault();
		}

		public bool Deletar(int usuarioId)
        {
            var usuario = _contexto.Usuarios.Where(o => o.Id == usuarioId).FirstOrDefault();

            if (usuario == null)
                throw new NaoEncontradoException();

            _contexto.Entry(usuario).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }

        public bool Atualizar(Usuario usuario)
        {
            _contexto.Entry(usuario).State = EntityState.Modified;
            return _contexto.SaveChanges() > 0;
        }
    }
}
