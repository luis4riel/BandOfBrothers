using System;
using BancoTabajara.Infra.Settings;
using BancoTabajara.WebApi.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace BancoTabajara.WebApi.App_Start
{
	public static class OAuthConfig
	{
		public static void ConfigureOAuth(IAppBuilder app)
		{
			ConfigureOAuthTokenGeneration( app );
		}

		private static void ConfigureOAuthTokenGeneration(IAppBuilder app)
		{
			OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions() {
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString( "/token" ),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays( ProjectSettings.AuthenticationSettings.TokenExpiration ),
				Provider = new OAuthProvider(),

			};

			app.UseOAuthAuthorizationServer( OAuthServerOptions );

			app.UseOAuthBearerAuthentication( new OAuthBearerAuthenticationOptions() );
		}
	}
}