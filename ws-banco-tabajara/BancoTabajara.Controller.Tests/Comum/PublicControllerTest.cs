﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using BancoTabajara.WebApi.Controllers.Common;
using FluentAssertions;
using NUnit.Framework;

namespace BancoTabajara.Controller.Tests.Comum
{
	[TestFixture]
	public class PublicControllerTests
	{
		private PublicController _publicController;

		[SetUp]
		public void Initialize()
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.SetConfiguration( new HttpConfiguration() );
			_publicController = new PublicController()
			{
				Request = request
			};
		}

		#region GET

		[Test]
		public void Public_Controller_Get_ShouldOk()
		{
			var callback = _publicController.IsAlive();
			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
			httpResponse.Content.Should().BeTrue();
		}

		#endregion
	}
}
