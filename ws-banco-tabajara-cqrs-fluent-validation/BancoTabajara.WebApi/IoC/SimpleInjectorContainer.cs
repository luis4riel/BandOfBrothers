using BancoTabajara.Aplicacao.Funcionalidades.Clientes;
using BancoTabajara.Aplicacao.Funcionalidades.Contas;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.Infra.Contexto;
using BancoTabajara.Infra.ORM.Funcionalidade.Clientes;
using BancoTabajara.Infra.ORM.Funcionalidade.Contas;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Data.Entity;
using System.Web.Http;

namespace BancoTabajara.WebApi.IoC
{
    public static class SimpleInjectorContainer
    {
        public static void Inicializador()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            Registros(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = 
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static void Registros(Container container)
        {
            container.Register<IClienteRepositorio, ClienteRepositorio>();
            container.Register<IContaRepositorio, ContaRepositorio>();
            container.Register<IClienteServico, ClienteServico>();
            container.Register<IContaServico, ContaServico>();

            container.Register(() => new BancoTabajaraDbContext(), Lifestyle.Scoped);
        }
    }
}