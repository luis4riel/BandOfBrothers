using FluentAssertions;
using NUnit.Framework;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Infra.Features.Cnpj;
using Projeto_pizzaria.Infra.Features.Cpf;
using System;

namespace Projeto_pizzaria.Dominio.Teste.Features.Clientes
{
    [TestFixture]
	public class ClienteDominioTeste
    {
        Cliente _cliente;

        [SetUp]
        public void ClienteDominio()
        {
            _cliente = new Cliente();
        }

        [Test]
        public void Cliente_Dominio_Deve_Validar_Com_Sucesso_Cliente_Fisico()
        {
            _cliente = ObjectMother.ObtemClienteValidoFisico();
            Action act = _cliente.Validar;
            act.Should().NotThrow();
        }

        [Test]
        public void Cliente_Dominio_Deve_Validar_Com_Sucesso_Cliente_Juridico()
        {
            _cliente = ObjectMother.ObtemClienteValidoJuridico();
            Action act = _cliente.Validar;
            act.Should().NotThrow();
        }

        [Test]
        public void Cliente_Dominio_Deve_Validar_Com_Falha_Cliente_CpfInvalido()
        {
            _cliente = ObjectMother.ObtemClienteCnpjVazioECpfInvalidoUltimoDigito();
            Action act = _cliente.ValidarCnpjCpf;
            act.Should().Throw<CpfInvalidoException>();
        }

        [Test]
        public void Cliente_Dominio_Deve_Validar_Com_Falha_Cliente_CnpjInvalido()
        {
            _cliente = ObjectMother.ObtemClienteCnpjInvalidoUltimoDigitoECpfVazio();
            Action act = _cliente.ValidarCnpjCpf;
            act.Should().Throw<CnpjInvalidoException>();
        }

        [Test]
        public void Cliente_Dominio_Deve_Validar_Com_Falha_Cliente_Com_Cpf_Cnpj_vazio()
        {
            _cliente = ObjectMother.ObtemClienteCnpjECpfVazio();
            Action act = _cliente.ValidarCnpjCpf;
            act.Should().Throw<ClienteEmptyCpfCnpjException>();
        }

        [Test]
        public void Cliente_Dominio_Deve_Validar_Com_Falha_Cliente_Com_Nome_Vazio()
        {
            _cliente = ObjectMother.ObtemClienteNomeVazio();
            Action act = _cliente.Validar;
            act.Should().Throw<ClienteComNomeVazioException>();
        }

        [Test]
        public void Cliente_Dominio_Deve_Validar_Com_Falha_Cliente_Com_Telefone_Vazio()
        {
            _cliente = ObjectMother.ObtemClienteSemTelefone();
            Action act = _cliente.Validar;
            act.Should().Throw<ClienteComTelefoneVazioException>();
        }
    }
}
