using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using BancoTabajara.Aplicacao.Base;
using BancoTabajara.Dominio.Base;

namespace BancoTabajara.WebApi.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class HttpGetAllExtensions<T> where T : Entidade
	{
		public static IQueryable<T> GeraQueryParaGetAll( IServico<T> servico, IEnumerable<KeyValuePair<string, string>> chaveValor )
		{
			IQueryable<T> query;
			if (chaveValor.Count() > 0)
			{
				KeyValuePair<string, string> limite = chaveValor.First( x => x.Key.Equals( "limite" ) );
				query = servico.PegarTodos( int.Parse( limite.Value ) );
			} else
				query = servico.PegarTodos();
			return query;
		}
	}

}