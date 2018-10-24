using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BancoTabajara.Infra.Settings
{
	[ExcludeFromCodeCoverage]
	public class ConfigurationSectionHandler : IConfigurationSectionHandler
	{
		private readonly string Namespace = "BancoTabajara.Infra.Settings.Entities.{0}";

		public object Create(object parent, object configContext, XmlNode section)
		{
			MemoryStream stm = new MemoryStream();

			StreamWriter stw = new StreamWriter( stm );
			stw.Write( section.OuterXml );
			stw.Flush();

			stm.Position = 0;
			var type = System.Type.GetType( string.Format( Namespace, section.Name ) );

			XmlSerializer ser = new XmlSerializer( type );
			var result = ( ser.Deserialize( stm ) );

			return result;
		}
	}
}

