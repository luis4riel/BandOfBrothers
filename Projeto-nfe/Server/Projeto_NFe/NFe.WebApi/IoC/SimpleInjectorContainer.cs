using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Diagnostics.CodeAnalysis;
using NFe.Aplicacao.Features.Produtos;
using NFe.Aplicacao.Features.Transportadores;
using NFe.Dominio.Features.Transportadores;
using NFe.Infra.Data.Features.Transportadores;
using NFe.Dominio.Features.Produtos;
using NFe.Infra.Data.Features.Produtos;
using NFe.Infra.Data.Contexts;
using NFe.Dominio.Features.Emitentes;
using NFe.Aplicacao.Features.Emitentes;
using NFe.Infra.Data.Features.Emitentes;
using NFe.Aplicacao.Features.Destinatarios;
using NFe.Infra.Data.Features.Destinatarios;
using NFe.Dominio.Features.Destinatarios;
using NFe.Aplicacao.Features.Notas_Fiscais;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Infra.Data.Features.Notas_Fiscais;

namespace NFe.WebApi.IoC
{
    [ExcludeFromCodeCoverage]
    public static class SimpleInjectorContainer
    {
        public static void Initialize()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterServicos(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }


        public static void RegisterServicos(Container container)
        {
            container.Register<IProdutoServico, ProdutoServico>();
            container.Register<IProdutoRepositorio, ProdutoRepositorio>();
            container.Register<ITransportadorServico, TransportadorServico>();
            container.Register<ITransportadorRepositorio, TransportadorRepositorio>();
            container.Register<IEmitenteServico, EmitenteServico>();
            container.Register<IEmitenteRepositorio, EmitenteRepositorio>();
            container.Register<IDestinatarioServico, DestinatarioServico>();
            container.Register<IDestinatarioRepositorio, DestinatarioRepositorio>();
            container.Register<INotaFiscalServico, NotaFiscalServico>();
            container.Register<INotaFiscalRepositorio, NotaFIscalRepositorio>();
            // TODO: Por enquanto estaremos criando o contexto do EF por aqui. Precisaremos trocar por uma Factory
            container.Register<NFeDBContext>(() => new NFeDBContext(), Lifestyle.Scoped);
        }
    }
}