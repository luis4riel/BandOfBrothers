using Projeto_pizzaria.Dominio.Features.Adicionais;
using Projeto_pizzaria.Dominio.Features.Clientes;
using Projeto_pizzaria.Dominio.Features.Enderecos;
using Projeto_pizzaria.Dominio.Features.Pedidos;
using Projeto_pizzaria.Dominio.Features.Produtos;
using Projeto_pizzaria.Infra.Features.Cnpj;
using Projeto_pizzaria.Infra.Features.Cpf;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_pizzaria.Infra.Data.Context
{
    [ExcludeFromCodeCoverage]
    public class PizzariaContext : DbContext
    {
        public PizzariaContext() : base("PizzariaBD_BandOfBrothers")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<Cpf>().Property(c => c.ValorFormatado).HasColumnName("Cpf");

            modelBuilder.ComplexType<Cnpj>().Property(c => c.ValorFormatado).HasColumnName("Cnpj");

            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(150));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try	{ return base.SaveChanges(); }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                }
                throw;
            }
        }
    }
}
