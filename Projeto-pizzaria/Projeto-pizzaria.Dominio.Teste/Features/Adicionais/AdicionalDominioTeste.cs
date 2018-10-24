using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Projeto_pizzaria.Commum.Features;
using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.Produtos;

namespace Projeto_pizzaria.Dominio.Teste.Features.Adicionais
{
	[TestFixture]
	public class AdicionalDominioTeste
	{
		[Test]
		public void Deve_calcular_preco_borda_catupiry_pizza_grande()
		{
			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCatupiry();

			double preco = bordaCatupiry.GetPreco( TamanhoEnum.Grande );

			preco.Should().Be( 2.50 );
		}


		[Test]
		public void Deve_calcular_preco_borda_catupiry_pizza_media()
		{
			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCatupiry();

			double preco = bordaCatupiry.GetPreco( TamanhoEnum.Media );

			preco.Should().Be( 1.75 );
		}

		[Test]
		public void Deve_calcular_preco_borda_catupiry_pizza_pequena()
		{
			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCatupiry();

			double preco = bordaCatupiry.GetPreco( TamanhoEnum.Pequena );

			preco.Should().Be( 1.25 );
		}

		[Test]
		public void Deve_calcular_preco_borda_cheddar_pizza_grande()
		{
			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCheddar();

			double preco = bordaCatupiry.GetPreco( TamanhoEnum.Grande );

			preco.Should().Be( 2.00 );
		}

		[Test]
		public void Deve_calcular_preco_borda_cheddar_pizza_media()
		{
			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCheddar();

			double preco = bordaCatupiry.GetPreco( TamanhoEnum.Media );

			preco.Should().Be( 1.50 );
		}

		[Test]
		public void Deve_calcular_preco_borda_cheddar_pizza_pequena()
		{
			Adicional bordaCatupiry = ObjectMother.adicionalValidoComCheddar();

			double preco = bordaCatupiry.GetPreco( TamanhoEnum.Pequena );

			preco.Should().Be( 1.00 );
		}
	}
}
