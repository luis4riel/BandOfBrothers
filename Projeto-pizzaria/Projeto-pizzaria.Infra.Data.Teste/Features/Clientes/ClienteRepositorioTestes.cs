using FluentAssertions;
using NUnit.Framework;
using Projeto_pizzaria.Commum.Base;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Clientes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Infra.Data.Teste.Features.Clientes
{
  public class ClienteRepositorioTestes
    {
        private PizzariaContext _contexto;
        private ClienteRepositorio _repositorio;
        private Cliente _cliente;

        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new BaseSqlTest());
            _contexto = new PizzariaContext();
            _repositorio = new ClienteRepositorio(_contexto);
            _contexto.Database.Initialize(true);
        }

        [Test]
        public void Cliente_InfraData_deve_inserir_registro_na_base()
        {
            _cliente = ObjectMother.ObtemClienteValidoFisico();

            Cliente NovoCliente = _repositorio.Salvar(_cliente);

            NovoCliente.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Cliente_InfraData_deve_atualizar_registro_na_base()
        {
            var idEsperado = 1;
            Cliente ClienteObtido = _repositorio.PegarPorId(idEsperado);
            ClienteObtido.Nome = "Jao da silva";

            Cliente ClienteEditado = _repositorio.Atualizar(ClienteObtido);

            ClienteEditado.Nome.Should().Be(ClienteObtido.Nome);
        }

        [Test]
        public void Cliente_InfraData_deve_deletar_registro_na_base()
        {
            _cliente = ObjectMother.ObtemClienteValidoJuridico();

            var clienteObtido = _repositorio.Salvar(_cliente);


            Cliente cliente = _repositorio.PegarPorId(clienteObtido.Id);

            _repositorio.Deletar(cliente);

            Cliente clienteEncontrado = _repositorio.PegarPorId(cliente.Id);
            clienteEncontrado.Should().BeNull();
        }

        [Test]
        public void Cliente_InfraData_deve_trazer_por_id_registro_na_base()
        {
            var idEsperado = 1;

            Cliente clienteObtido = _repositorio.PegarPorId(idEsperado);

            clienteObtido.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Cliente_InfraData_deve_trazer_todos_registros_da_base()
        {
            IEnumerable<Cliente> ClientesObtidos = _repositorio.PegarTodos();

            var qtdEsperado = 2;
            var idEsperado = 1;
            ClientesObtidos.Count().Should().Be(qtdEsperado);
            ClientesObtidos.First().Id.Should().Be(idEsperado);
        }
    }
}
