using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BancoTabajara.Infra.ORM.Migrations
{
	[ExcludeFromCodeCoverage]
	public class Configuration : DbMigrationsConfiguration<BancoTabajara.Infra.Contexto.BancoTabajaraDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BancoTabajara.Infra.Contexto.BancoTabajaraDbContext context)
        {
        }
    }
}
