using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Aplicacao.Funcionalidades.Usuarios;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Usuarios;
using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Funcionalidade.Clientes;
using BancoTabajara.Infra.ORM.Funcionalidade.Contas;
using BancoTabajara.Infra.ORM.Funcionalidade.Usuarios;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Data.Entity;
using System.Web.Http;

namespace BancoTabajara.WebApi.IoC
{
    public static class SimpleInjectorContainer
    {
		public static Container ContainerInstance { get; private set; }

		public static void Inicializador()
		{
			ContainerInstance = new Container();

			ContainerInstance.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

			Registros();

			ContainerInstance.RegisterWebApiControllers( GlobalConfiguration.Configuration );

			ContainerInstance.Verify();

			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver( ContainerInstance );
		}

		public static void Registros()
        {
			ContainerInstance.Register<IClienteRepositorio, ClienteRepositorio>();
			ContainerInstance.Register<IContaRepositorio, ContaRepositorio>();
			ContainerInstance.Register<IUsuarioRepositorio, UsuarioRepositorio>();
			ContainerInstance.Register<IClienteServico, ClienteServico>();
			ContainerInstance.Register<IContaServico, ContaServico>();
			ContainerInstance.Register<IUsuarioServico, UsuarioServico>();

			ContainerInstance.Register(() => new BancoTabajaraDbContext(), Lifestyle.Scoped );
        }
    }
}