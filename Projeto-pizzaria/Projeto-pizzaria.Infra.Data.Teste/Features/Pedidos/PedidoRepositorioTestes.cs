using FluentAssertions;
using NUnit.Framework;
using Projeto_pizzaria.Commum.Base;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Infra.Data.Context;
using Projeto_pizzaria.Infra.Data.Features.Pedidos;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Projeto_pizzaria.Infra.Data.Teste.Features.Pedidos
{
    [TestFixture]
    public class PedidoRepositorioTestes
    {
        private PizzariaContext _contexto;
        private PedidoRepositorio _repositorio;
        private Pedido _pedido;

        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new BaseSqlTest());
            _contexto = new PizzariaContext();
            _repositorio = new PedidoRepositorio( _contexto );
            _contexto.Database.Initialize(true);
        }

        [Test]
        public void Pedido_InfraData_deve_inserir_registro_na_base()
        {
            _pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

            Pedido pedidoInserido = _repositorio.Salvar(_pedido);

            pedidoInserido.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Pedido_InfraData_deve_atualizar_registro_na_base()
        {
            var idEsperado = 1;
            Pedido pedidoObtido = _repositorio.PegarPorId(idEsperado);
            pedidoObtido.StatusPedido = StatusPedidoEnum.AguardandoMontagem;

            Pedido pedidoEditado = _repositorio.Atualizar(pedidoObtido);

            pedidoEditado.StatusPedido.Should().Be(StatusPedidoEnum.AguardandoMontagem);
        }

        [Test]
        public void Pedido_InfraData_deve_deletar_registro_na_base()
        {
            _pedido = ObjectMother.ObterPedidoValidoPessoaFisica();

            var pedidoInserido = _repositorio.Salvar(_pedido);


            Pedido pedido = _repositorio.PegarPorId(pedidoInserido.Id);

            _repositorio.Deletar(pedido);

            Pedido pedidoEncontrado = _repositorio.PegarPorId(pedido.Id);
            pedidoEncontrado.Should().BeNull();
        }

        [Test]
        public void Pedido_InfraData_deve_trazer_por_id_registro_na_base()
        {
            var idEsperado = 1;

            Pedido pedidoObtido = _repositorio.PegarPorId(idEsperado);

            pedidoObtido.Id.Should().Be(idEsperado);
        }

        [Test]
        public void Pedido_InfraData_deve_trazer_todos_registros_da_base()
        {
            IEnumerable<Pedido> pedidosObtidos = _repositorio.PegarTodos();

            var qtdEsperado = 1;
            var idEsperado = 1;
            pedidosObtidos.Count().Should().Be(qtdEsperado);
            pedidosObtidos.First().Id.Should().Be(idEsperado);
        }
    }
}
