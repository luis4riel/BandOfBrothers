using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Migrations;

namespace BancoTabajara.Infra.Inicializador
{
	public class InicializadorDB : MigrateDatabaseToLatestVersion<BancoTabajaraDbContext, Configuration>
	{
	}
}
