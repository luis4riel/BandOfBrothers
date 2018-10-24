using BancoTabajara.Infra.Contexto;
using System.Data.Common;

namespace BancoTabajara.Infra.ORM.Tests.Contexto
{
    public class FakeDbContext : BancoTabajaraDbContext
    {
        public FakeDbContext(DbConnection connection) : base(connection)
        {
        }
    }
}
