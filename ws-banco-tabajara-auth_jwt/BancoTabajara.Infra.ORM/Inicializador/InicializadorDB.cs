using System;
using System.Data.Entity;
using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Migrations;

namespace BancoTabajara.Infra.Inicializador
{
	public class InicializadorDB : MigrateDatabaseToLatestVersion<BancoTabajaraDbContext, Configuration>
	{
	}
}
