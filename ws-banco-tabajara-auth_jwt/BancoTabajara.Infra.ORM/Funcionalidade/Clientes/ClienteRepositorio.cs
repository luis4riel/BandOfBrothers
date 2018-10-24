using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Infra.Contexto;
using System;
using System.Data.Entity;
using System.Linq;

namespace BancoTabajara.Infra.ORM.Funcionalidade.Clientes
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private BancoTabajaraDbContext _contexto;

        public ClienteRepositorio(BancoTabajaraDbContext contexto) => _contexto = contexto;

        public Cliente Inserir(Cliente cliente)
        {
            _contexto.Clientes.Attach(cliente);
            var newCliente = _contexto.Clientes.Add(cliente);
            _contexto.SaveChanges();
            return newCliente;
        }
        
        public IQueryable<Cliente> PegarTodos(int? limite = null)
        {
            if(limite != null)
                return _contexto.Clientes.Take(Convert.ToInt32(limite)).OrderBy(c => c.Nome);
            else
                return _contexto.Clientes.OrderBy(c => c.Nome);
        }

        public Cliente PegarPorId(int productId)
        {
            return _contexto.Clientes.Find(productId);
        }

        public bool Deletar(int clienteId)
        {
            var cliente = _contexto.Clientes.Where(o => o.Id == clienteId).FirstOrDefault();

            var conta = _contexto.Contas.Where(c => c.Titular.Id.Equals(clienteId)).FirstOrDefault();

            if (cliente == null)
                throw new NaoEncontradoException();

            if (conta != null)
                throw new ClienteVinculadoException();

            _contexto.Entry(cliente).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }

        public bool Atualizar(Cliente cliente)
        {
            _contexto.Entry(cliente).State = EntityState.Modified;
            return _contexto.SaveChanges() > 0;
        }
    }
}
