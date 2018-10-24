using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using BancoTabajara.Aplicacao.Base;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Dominio.Base;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;

namespace BancoTabajara.WebApi.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class HttpGetAllExtensions<T> where T : Entidade
	{
		public static IQueryable<Cliente> GeraQueryParaGetAllCliente( IClienteServico servico, IEnumerable<KeyValuePair<string, string>> chaveValor )
		{
			IQueryable<Cliente> query;
			if (chaveValor.Count() > 0)
			{
				KeyValuePair<string, string> limite = chaveValor.First( x => x.Key.Equals( "limite" ) );
				query = servico.PegarTodos( int.Parse( limite.Value ) );
			} else
				query = servico.PegarTodos();
			return query;
		}
        public static IQueryable<Conta> GeraQueryParaGetAllConta(IContaServico servico, IEnumerable<KeyValuePair<string, string>> chaveValor)
        {
            IQueryable<Conta> query;
            if (chaveValor.Count() > 0)
            {
                KeyValuePair<string, string> limite = chaveValor.First(x => x.Key.Equals("limite"));
                query = servico.PegarTodos(int.Parse(limite.Value));
            }
            else
                query = servico.PegarTodos();
            return query;
        }
    }

}