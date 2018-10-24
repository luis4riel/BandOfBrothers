using NFe.Dominio.Features.Enderecos;

namespace NFe.Common.Testes.Features
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
                Pais = "Brasil"
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
                Pais = "Brasil"
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
                Pais = "Brasil"
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
                Pais = "Brasil"
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
                Pais = "Brasil"
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
                Pais = "Brasil"
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
                Pais = "Brasil"
            };
        }

        public static Localizacao ObterEnderecoPaisVazio()
        {
            return new Localizacao
            {
                Bairro = "Sagrado Coração de Jesus",
                Estado = "Santa Catarina",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "547",
                Pais = ""
            };
        }

        public static Localizacao ObterEnderecoIdInvalido()
        {
            return new Localizacao
            {
                Bairro = "Sagrado Coração de Jesus",
                Estado = "Santa Catarina",
                Logradouro = "Coronel Zeca Athanasio",
                Municipio = "Lages",
                Numero = "547",
                Pais = "Brasil"
            };
        }
    }
}
