using System;
using log4net;
using System.IO;
using System.Reflection;
using System.Web.Http.ExceptionHandling;
using System.Runtime.Remoting.Contexts;

namespace BancoTabajara.WebApi.ExLogger
{
	public class ExceptionManagerApi : ExceptionLogger
	{
		ILog _logger = null;

		public ExceptionManagerApi()
		{
			var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

			var log4NetConfigFilePath = @"C:\academia\BandOfBrothers\ws-banco-tabajara-oAuth\BancoTabajara.WebApi\ExLogger\log4net.config";
			log4net.Config.XmlConfigurator.ConfigureAndWatch( new FileInfo( log4NetConfigFilePath ) );
		}

		public override void Log(ExceptionLoggerContext context)
		{
			_logger = log4net.LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
			_logger.Error( context.Exception.ToString() + Environment.NewLine );
		}

		public void Log(string ex)
		{
			_logger = log4net.LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
			_logger.Error( ex );
		}
	}
}