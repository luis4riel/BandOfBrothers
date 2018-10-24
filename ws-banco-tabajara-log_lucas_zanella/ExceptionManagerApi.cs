using System;
using  System.Collections.Generic;  
using  System.Linq;  
using  System.Text;  
using  System.Threading.Tasks;  
using  System.Web.Mvc;
using log4net;  
using  System.IO;  
using  System.Reflection;  
using  System.Web.Http.ExceptionHandling;  
  
namespace CrxApi.ExLogger
{
	public class  ExceptionManagerApi: ExceptionLogger   
    {  
        ILog _logger = nulo;
	public ExceptionManagerApi()
	{
		// Obtém o caminho do diretório do aplicativo de chamada  
		// RelativeSearchPath é nulo se o assembly em execução, ou seja, o assembly de chamada for um  
		// stand alone arquivo exe (Console, WinForm, etc).   
		// RelativeSearchPath não é nulo se o assembly de chamada for um aplicativo hospedado na Web, ou seja, um site da Web  
		var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

		// var log4NetConfigFilePath = Path.Combine (log4NetConfigDirectory, "log4net.config");  
		var log4NetConfigFilePath = "c: \\ usuários \\ usuário \\ documentos \\ visual studio 2012 \\ Projetos \\ ErrorLogingDummy \\ ErrorLogingDummy \\ ExLogger \\ log4net.config";
		log4net.Config.XmlConfigurator.ConfigureAndWatch( new  FileInfo( log4NetConfigFilePath ) );
	}
	public override void Log(contexto ExceptionLoggerContext)
	{
		_logger = log4net.LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
		_logger.Error( context.Exception.ToString() + Environment.NewLine );
		//_logger.Error(Environment.NewLine + "Excetion Time:" + System.DateTime.Now + Environment.NewLine  
		// + "Mensagem de exceção:" + context.Exception.Message.ToString () + Environment.NewLine  
		// + "Caminho do Arquivo de Exceção:" + context.ExceptionContext.ControllerContext.Controller.ToString () + "/" + context.ExceptionContext.ControllerContext.RouteData.Values ​​["action"] + Environment.NewLine);   
	}
	public void Log(string ex)
	{
		_logger = log4net.LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
		_logger.Error( ex );
		//_logger.Error(Environment.NewLine + "Excetion Time:" + System.DateTime.Now + Environment.NewLine  
		// + "Mensagem de exceção:" + context.Exception.Message.ToString () + Environment.NewLine  
		// + "Caminho do Arquivo de Exceção:" + context.ExceptionContext.ControllerContext.Controller.ToString () + "/" + context.ExceptionContext.ControllerContext.RouteData.Values ​​["action"] + Environment.NewLine);   
	}

}  
  
  
  
}  