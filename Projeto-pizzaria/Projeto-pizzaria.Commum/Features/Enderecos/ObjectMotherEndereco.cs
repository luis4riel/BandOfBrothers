using Projeto_pizzaria.Dominio.Features.Enderecos;

namespace Projeto_pizzaria.Commum.Features
{
    public static partial class ObjectMother
    {
        public static Localizacao ObterEnderecoValido()
        {
            return new Localizacao
            {
                Bairro = "Sagrado Coração de Jesus",
                Estado = "SC",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "547",
                Cep = "88508-180",
				Complemento = "Prox. a Igreja do Sagrado"
            };
        }

        public static Localizacao ObterEnderecoValidoSemVinculo()
        {
            return new Localizacao
            {	                   
                Bairro = "Sagrado Coração de Jesus",
                Estado = "SC",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "547",
                Cep = "88508-180",
				Complemento = "Prox. a Igreja do Sagrado"
			};
        }

        public static Localizacao ObterEnderecoBairroVazio()
        {
            return new Localizacao
            {                  
                Bairro = "",
                Estado = "SC",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "547",
                Cep = "88508-180",
				Complemento = "Prox. a Igreja do Sagrado"
			};
        }

        public static Localizacao ObterEnderecoEstadoVazio()
        {
            return new Localizacao
            {		                
                Bairro = "Sagrado Coração de Jesus",
                Estado = "",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "547",
                Cep = "88508-180",
				Complemento = "Prox. a Igreja do Sagrado"
			};
        }

        public static Localizacao ObterEnderecoLogradouroVazio()
        {
            return new Localizacao
            {	                   
                Bairro = "Sagrado Coração de Jesus",
                Estado = "SC",
                Logradouro = "",
                Municipio = "Lages",
                Numero = "547",
                Cep = "88508-180",
				Complemento = "Prox. a Igreja do Sagrado"
			};
        }

        public static Localizacao ObterEnderecoMunicipioVazio()
        {
            return new Localizacao
            {	                 
                Bairro = "Sagrado Coração de Jesus",
                Estado = "Santa Catarina",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "",
                Numero = "547",
                Cep = "88508-180",
				Complemento = "Prox. a Igreja do Sagrado"
			};
        }

        public static Localizacao ObterEnderecoNumeroVazio()
        {
            return new Localizacao
            {	                
                Bairro = "Sagrado Coração de Jesus",
                Estado = "Santa Catarina",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "",
                Cep = "88508-180",
				Complemento = "Prox. a Igreja do Sagrado"
			};
        }

        public static Localizacao ObterEnderecoCepVazio()
        {
            return new Localizacao
            {	                  
                Bairro = "Sagrado Coração de Jesus",
                Estado = "Santa Catarina",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "547",
                Cep = "",
				Complemento = "Prox. a Igreja do Sagrado"
			};
        }
    }
}
