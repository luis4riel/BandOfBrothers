namespace NFe.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<NFe.Infra.Data.Contexts.NFeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NFe.Infra.Data.Contexts.NFeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
