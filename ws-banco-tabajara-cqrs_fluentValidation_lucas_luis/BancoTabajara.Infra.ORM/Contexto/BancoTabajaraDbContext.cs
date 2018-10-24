using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Movimentacoes;

namespace BancoTabajara.Infra.Contexto
{
	[ExcludeFromCodeCoverage]
	public class BancoTabajaraDbContext : DbContext
	{
		public BancoTabajaraDbContext( string connection = "Name=BancoTabajaraDbContext" ) : base( connection )
		{
            Configuration.LazyLoadingEnabled = true;
		}

		protected BancoTabajaraDbContext( DbConnection connection ) : base( connection, true ) { }

		public DbSet<Cliente> Clientes { get; set; }

		public DbSet<Conta> Contas { get; set; }

		public DbSet<Movimentacao> Movimentacoes { get; set; }

		protected override void OnModelCreating( DbModelBuilder modelBuilder )
		{
			modelBuilder.Configurations.AddFromAssembly( System.Reflection.Assembly.GetExecutingAssembly() );
			base.OnModelCreating( modelBuilder );
		}

        public override int SaveChanges()
        {
            try { return base.SaveChanges(); }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                        Console.WriteLine("- Propriedade: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                }
                throw;
            }
        }
    }
}
