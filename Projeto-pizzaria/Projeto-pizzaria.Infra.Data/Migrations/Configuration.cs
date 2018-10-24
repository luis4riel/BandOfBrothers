namespace Projeto_pizzaria.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Projeto_pizzaria.Infra.Data.Context.PizzariaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Projeto_pizzaria.Infra.Data.Context.PizzariaContext";
        }

        protected override void Seed(Projeto_pizzaria.Infra.Data.Context.PizzariaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
