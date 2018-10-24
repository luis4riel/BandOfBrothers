using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using BancoTabajara.Infra.Settings.Entities;

namespace BancoTabajara.Infra.Settings
{
	[ExcludeFromCodeCoverage]
	public static class ProjectSettings
	{
		#region private fields
		static AuthenticationSettings _authSettings;
		#endregion private fields

		public static AuthenticationSettings AuthenticationSettings
		{
			get
			{
				return _authSettings ?? ( (AuthenticationSettings) ConfigurationManager.GetSection( "BancoTabajara/AuthenticationSettings" ) );
			}
		}
	}
}
