using BancoTabajara.Common.Tests.Funcionalidades;
using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Clientes.Excecoes;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BancoTabajara.Dominio.Tests.Funcionalidades.Clientes
{
    [TestFixture]
    public class ClienteDominioTestes
    {
        private Cliente _cliente;

        [Test]
        public void Cliente_dominio_validar_deve_passar()
        {
            _cliente = ObjectMother.ClienteValido();

            Action action = () => _cliente.Validar();
            action.Should().NotThrow<ExcecaoDeNegocio>();
        }
        [Test]
        public void Cliente_dominio_rg_em_branco_deve_falhar()
        {
            _cliente = ObjectMother.ClienteRgEmBranco();

            Action action = () => _cliente.Validar();
            action.Should().Throw<ClienteRgEmBrancoException>();
        }
        [Test]
        public void Cliente_dominio_nome_em_branco_deve_falhar()
        {
            _cliente = ObjectMother.ClienteNomeEmBranco();

            Action action = () => _cliente.Validar();
            action.Should().Throw<ClienteNomeEmBrancoException>();
        }
        [Test]
        public void Cliente_dominio_cpf_em_branco_deve_falhar()
        {
            _cliente = ObjectMother.ClienteCpfEmBranco();

            Action action = () => _cliente.Validar();
            action.Should().Throw<ClienteCpfEmBrancoException>();
        }


        [Test]
        public void Cliente_dominio_Data_Maior_Que_Atual_deve_falhar()
        {
            _cliente = ObjectMother.ClienteValido();
            _cliente.DataNascimento = DateTime.Now.AddDays(+1);

            Action action = () => _cliente.Validar();
            action.Should().Throw<ClienteDataNascimentoMaiorQueAgoraException>();
        }
    }
}
