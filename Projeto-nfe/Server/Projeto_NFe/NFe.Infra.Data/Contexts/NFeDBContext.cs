using NFe.Dominio.Features.Destinatarios;
using NFe.Dominio.Features.Emitentes;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Dominio.Features.Produtos;
using NFe.Dominio.Features.Transportadores;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Infra.Data.Contexts
{
    [ExcludeFromCodeCoverage]
    public class NFeDBContext : DbContext
    {
        public NFeDBContext(string connection = "Name=NFeDBContext") : base(connection)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected NFeDBContext(DbConnection connection) : base(connection, true) { }

        public DbSet<Destinatario> Destinatarios { get; set; }
        public DbSet<Emitente> Emitentes { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Transportador> Transportadores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
