using BancoTabajara.Dominio.Base;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace BancoTabajara.Dominio.Funcionalidades.Movimentacoes
{
	public class Movimentacao : Entidade
	{
		public decimal Valor { get; private set; }
		public TipoMovimentacao Tipo { get; private set; }
		public DateTime? Data { get; private set; }

        [XmlIgnore]
        public virtual Conta Conta { get; set; }
		public int ContaId { get; set; }

        [ExcludeFromCodeCoverage]
		public Movimentacao() { }

		public Movimentacao( TipoMovimentacao tipo, decimal valor, int id )
		{
			Valor = valor;
			Tipo = tipo;
			Data = DateTime.Now;
			ContaId = id;
		}
	}
}
