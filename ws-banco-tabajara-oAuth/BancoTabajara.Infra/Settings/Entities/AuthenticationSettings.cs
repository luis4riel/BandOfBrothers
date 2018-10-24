using System.Diagnostics.CodeAnalysis;

namespace BancoTabajara.Infra.Settings.Entities
{
	[ExcludeFromCodeCoverage]
	public class AuthenticationSettings
	{
		public int TokenExpiration { get; set; }
		public string AudienceId { get; set; }
		public string AudienceSecret { get; set; }
		public string Issuer { get; set; }
	}
}