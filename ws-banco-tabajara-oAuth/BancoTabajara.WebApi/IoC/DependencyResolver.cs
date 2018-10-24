using System;
using System.Collections.Generic;
using SimpleInjector;

namespace BancoTabajara.WebApi.IoC
{
	public class DependencyResolver
	{
		public static Container Container { get; private set; }

		public static void SetupContainer(Container container)
		{
			Container = container;
		}

		public static T Get<T>() where T : class
		{
			if (Container == null) throw new InvalidOperationException( "Não foi possivel resolver as dependencias." );
			return Container.GetInstance<T>();
		}
	}
}