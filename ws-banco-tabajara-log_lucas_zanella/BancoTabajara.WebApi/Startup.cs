using BancoTabajara.Aplicacao.Funcionalidades.Usuarios;
using BancoTabajara.WebApi.IoC;
using Microsoft.Owin;
using Owin;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using BancoTabajara.WebApi.App_Start;
using BancoTabajara.WebApi.Models.Mapeador;
using BancoTabajara.WebApi.Logger;

[assembly: OwinStartup(typeof(BancoTabajara.WebApi.Startup))]
namespace BancoTabajara.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
		public IUsuarioServico _usuarioServico;

		public void Configuration(IAppBuilder app)
        {
			GlobalConfiguration.Configure( WebApiConfig.Register );
			SimpleInjectorContainer.Inicializador();
			InicializadorAutoMapper.Inicializador();

			HttpConfiguration config = new HttpConfiguration();
			OAuthConfig.ConfigureOAuth( app );
            config.MessageHandlers.Add(new CustomLogHandler());
            app.UseWebApi( config );
		}
    }
}