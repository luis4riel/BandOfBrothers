using NFe.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Infra.Data.Testes.Context
{
    public class FakeDbContext : NFeDBContext
    {
        public FakeDbContext(DbConnection connection) : base(connection)
        {
            

        }
    }
}
