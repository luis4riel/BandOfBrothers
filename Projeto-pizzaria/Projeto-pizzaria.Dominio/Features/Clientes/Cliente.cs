using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Projeto_pizzaria.Dominio.Base;
using Projeto_pizzaria.Dominio.Features.Enderecos;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Infra.Features.Cnpj;
using Projeto_pizzaria.Infra.Features.Cpf;

namespace Projeto_pizzaria.Dominio.Features.Clientes
{
    public class Cliente : Entidade
    {
		public string Nome { get; set; }
        public string Telefone { get; set; }
        public virtual Cpf Cpf { get; set; }
		public virtual Cnpj Cnpj { get; set; }
		public Localizacao Endereco { get; set; }
        public virtual List<Pedido> Pedidos { get; set; } 
		public TipoClienteEnum TipoCliente { get; set; }	

		public Cliente()
		{
			Cpf = new Cpf("");
			Cnpj = new Cnpj("");
			Endereco = new Localizacao();
			Pedidos = new List<Pedido>();	
		}

        public override void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                throw new ClienteComNomeVazioException();
            if (string.IsNullOrEmpty(Telefone))
                throw new ClienteComTelefoneVazioException();

            Endereco.Validar();
        }

        public void ValidarCnpjCpf()
        {
            if (!string.IsNullOrEmpty(Cnpj.ValorFormatado))
            {
                Cnpj = Cnpj.ValorFormatado;
                if (Cnpj.EhValido == false)
                    throw new CnpjInvalidoException();
            }
            if (!string.IsNullOrEmpty(Cpf.ValorFormatado))
            {
                Cpf = Cpf.ValorFormatado;
                if (Cpf.EhValido == false)
                    throw new CpfInvalidoException();
            }
            if (string.IsNullOrEmpty(Cnpj.ValorFormatado) && string.IsNullOrEmpty(Cpf.ValorFormatado))
                throw new ClienteEmptyCpfCnpjException();
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return "Nome: " + Nome + " - Telefone: " + Telefone;
        }
    }
}
