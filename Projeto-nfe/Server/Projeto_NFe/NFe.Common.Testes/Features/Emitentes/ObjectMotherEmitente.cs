using NFe.Aplicacao.Features.Emitentes.Commands;
using NFe.Dominio.Features.Emitentes;

namespace NFe.Common.Testes.Features
{
    public static partial class ObjectMother
    {
        public static EmitenteRegisterCommand ObtemEmitenteValidoAdicionar()
        {
            return new EmitenteRegisterCommand()
            {
                Cnpj = "06255692000286",
                Cpf = "05919707917",
                EnderecoId = 1,
                InscricaoEstadual = "1234567890",
                Nome = "teste",
                RazaoSocial = "teste",
            };
        }

        public static EmitenteRemoveCommand ObtemEmitenteValidoRemover()
        {
            return new EmitenteRemoveCommand()
            {
                Id = 1,
            };
        }

        public static EmitenteUpdateCommand ObtemEmitenteValidoAtualizar()
        {
            return new EmitenteUpdateCommand()
            {
                Id = 1,
                Cnpj = "06255692000286",
                Cpf = "05919707917",
                EnderecoId = 1,
                InscricaoEstadual = "1234567890",
                Nome = "teste",
                RazaoSocial = "teste",
            };
        }
        public static Emitente ObtemEmitenteValido()
        {
            return new Emitente()
            {
                Id = 1,
                Cnpj = "06255692000103",
                Cpf = "",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteValidoNovoNome()
        {
            return new Emitente()
            {
                Id = 1,
                Cnpj = "09383100000119",
                Cpf = "",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "0987654321",
                InscricaoMunicipal = "0987654321",
                Nome = "Fulaninho",
                RazaoSocial = "Vendedorzinho"
            };
        }

        public static Emitente ObterEmitenteSemEndereco()
        {
            return new Emitente()
            {
                Cnpj = "06255692000103",
                Cpf = "",
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteComNomeVazio()
        {
            return new Emitente()
            {
                Cnpj = "06255692000103",
                Cpf = "05919707917",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteComCpnjECpfVazio()
        {
            return new Emitente()
            {
                Cnpj = "",
                Cpf = "",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteComCpfVazio()
        {
            return new Emitente()
            {
                Cnpj = "06255692000103",
                Cpf = "",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteComCnpjVazio()
        {
            return new Emitente()
            {
                Cnpj = "",
                Cpf = "05919707917",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteComInscricaoEstadualVazio()
        {
            return new Emitente()
            {
                Cnpj = "06255692000103",
                Cpf = "",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteComInscricaoMunicipalVazio()
        {
            return new Emitente()
            {
                Cnpj = "06255692000103",
                Cpf = "05919707917",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "",
                Nome = "Fulano LTDA",
                RazaoSocial = "Vendedor ETC"
            };
        }

        public static Emitente ObterEmitenteComRazaoSocialVazio()
        {
            return new Emitente()
            {
                Cnpj = "06255692000103",
                Cpf = "05919707917",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = ""
            };
        }


        public static Emitente ObterEmitenteComCnpjMenorQue14()
        {
            return new Emitente()
            {
                Cnpj = "0625569200010",
                Cpf = "",
                Endereco = ObterEnderecoValido(),
                InscricaoEstadual = "123456789",
                InscricaoMunicipal = "1234567890",
                Nome = "Fulano LTDA",
                RazaoSocial = "teste"
            };
        }
    }
}
