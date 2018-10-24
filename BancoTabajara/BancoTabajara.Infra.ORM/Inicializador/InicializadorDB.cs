using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Migrations;
using System.Data.Entity;

namespace BancoTabajara.Infra.Inicializador
{
    public class InicializadorDB : MigrateDatabaseToLatestVersion<BancoTabajaraDbContext, Configuration>
	{
	}
}
