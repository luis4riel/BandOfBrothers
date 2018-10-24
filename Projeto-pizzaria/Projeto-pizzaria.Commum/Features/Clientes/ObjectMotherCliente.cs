using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.Clientes;

namespace Projeto_pizzaria.Commum.Features
{
    public static partial class ObjectMother
    {
        public static Cliente ObtemClienteValidoFisico()
        {
            return new Cliente()
            {
                Id = 1,
                Nome = "Jose João",
                Telefone = "49999161707",
                TipoCliente = TipoClienteEnum.Fisica,
                Cpf = "05919707917",
                Endereco = ObterEnderecoValido()
            };
        }

		public static Cliente ObtemClienteValidoJuridico()
		{
			return new Cliente()
			{
				Id = 1,
				Nome = "Jose João",
                Telefone = "49999161707",
                TipoCliente = TipoClienteEnum.Juridica,
				Cnpj = "06255692000103",
				Endereco = ObterEnderecoValido()
			};
		}

        public static Cliente ObtemClienteSemEndereco()
        {
            return new Cliente()
            {
                Nome = "Jose João",
                Telefone = "49999161707",
                TipoCliente = TipoClienteEnum.Juridica,
				Cnpj = "06255692000103",
			};
        }

        public static Cliente ObtemClienteSemTelefone()
        {
            return new Cliente()
            {
                Nome = "Jose João",
                TipoCliente = TipoClienteEnum.Juridica,
                Cnpj = "06255692000103",
            };
        }

        public static Cliente ObtemClienteNomeVazio()
        {
            return new Cliente()
            {
                Nome = "",
                Telefone = "49999161707",
                TipoCliente = TipoClienteEnum.Fisica,
				Cpf = "05919707917",
				Endereco = ObterEnderecoValido()
            };
        }

        public static Cliente ObtemClienteCnpjVazio()
        {
            return new Cliente()
            {
                Nome = "Jose João",
                Telefone = "49999161707",
                TipoCliente = TipoClienteEnum.Juridica,
                Cnpj = "",
                Endereco = ObterEnderecoValido() // fazer exceção do cnpj incorreto porem preenchido
            };
        }

        public static Cliente ObtemClienteCpfVazio()
        {
			return new Cliente()
			{
				Nome = "Jose João",
                Telefone = "49999161707",
                TipoCliente = TipoClienteEnum.Fisica,
				Cpf = "01253027978",
				Endereco = ObterEnderecoValido() // fazer exceção do cnpj incorreto porem preenchido
			};
		}

 
        public static Cliente ObtemClienteCnpjInvalidoUltimoDigitoECpfVazio()
        {
            return new Cliente()
            {
                Nome = "Jose João",
                Telefone = "49999161707",
                Cnpj = "0625569200010",
                Cpf = "",
                Endereco = ObterEnderecoValido()
            };
        }
        public static Cliente ObtemClienteCnpjVazioECpfInvalidoUltimoDigito()
        {
            return new Cliente()
            {
                Nome = "Jose João",
                Telefone = "49999161707",
                Cnpj = "",
                Cpf = "0591970791",
                Endereco = ObterEnderecoValido()
            };
        }

        public static Cliente ObtemClienteCnpjECpfVazio()
        {
            return new Cliente()
            {
				Id = 1,
				Nome = "Jose João",
                Telefone = "49999161707",
                TipoCliente = TipoClienteEnum.Fisica,
				Cpf = "",
				Cnpj = "",
				Endereco = ObterEnderecoValido()
			};
        }
    }
}
