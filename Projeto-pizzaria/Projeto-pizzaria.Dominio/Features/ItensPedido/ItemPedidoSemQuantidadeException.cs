using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_pizzaria.Dominio.Exceptions;

namespace Projeto_pizzaria.Dominio.Features.ItensPedido
{
	public class ItemPedidoSemQuantidadeException : BusinessException
	{
		public ItemPedidoSemQuantidadeException():base("Não pode existir itemPedido sem quantidade")
		{

		}
	}
}
