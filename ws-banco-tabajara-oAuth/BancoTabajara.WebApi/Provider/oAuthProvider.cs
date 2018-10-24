using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BancoTabajara.Aplicacao.Funcionalidades.Usuarios;
using BancoTabajara.Dominio.Funcionalidades.Usuarios;
using BancoTabajara.WebApi.IoC;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SimpleInjector.Lifestyles;

namespace BancoTabajara.WebApi.Provider
{
	public class OAuthProvider : OAuthAuthorizationServerProvider
	{
		public OAuthProvider() : base()
		{
		}

		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();

			return Task.FromResult<object>( null );
		}

		public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			context.OwinContext.Response.Headers.Add( "Access-Control-Allow-Origin", new[] { "*" } );

			var user = default( Usuario );

			try {
				using (AsyncScopedLifestyle.BeginScope( SimpleInjectorContainer.ContainerInstance )) {
					var authService = SimpleInjectorContainer.ContainerInstance.GetInstance<IUsuarioServico>();
					user = authService.Login( context.UserName, context.Password );
				}
			} catch (Exception ex) {
				context.SetError( "invalid_grant", ex.Message );
				return Task.FromResult<object>( null );
			}
			var identity = new ClaimsIdentity( "JWT" );
			identity.AddClaim( new Claim( "Id", user.Id.ToString() ) );
			identity.AddClaim( new Claim( ClaimTypes.Email, user.Email ) );
			identity.AddClaim( new Claim( ClaimTypes.Name, user.Name ) );
			var ticket = new AuthenticationTicket( identity, null );
			context.Validated( ticket );

			return Task.FromResult<object>( null );
		}
	}
}