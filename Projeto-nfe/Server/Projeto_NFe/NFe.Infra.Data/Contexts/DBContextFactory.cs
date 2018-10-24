using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Contexts
{
    [ExcludeFromCodeCoverage]
    public class DBContextFactory : IDbContextFactory<NFeDBContext>
    {
        public NFeDBContext Create()
        {
            return new NFeDBContext();
        }
    }
}
