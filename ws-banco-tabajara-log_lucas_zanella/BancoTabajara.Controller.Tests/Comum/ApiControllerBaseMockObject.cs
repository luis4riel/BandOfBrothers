using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BancoTabajara.WebApi.Controllers.Common;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace BancoTabajara.Controller.Tests.Comum
{
	public class ApiControllerBaseFake : ApiControllerBase
	{
		public IHttpActionResult HandleCallback<TSuccess>(Func<TSuccess> func)
		{
			return base.HandleCallback( func );
		}

		public IHttpActionResult HandleQuery<OriginType, TResult>(IQueryable<OriginType> query, ODataQueryOptions<OriginType> queryOptions)
		{
			return base.HandleQuery<OriginType, TResult>( query, queryOptions );
		}

		public PageResult<RetType> HandlePageResult<OriginType, RetType>(IQueryable<OriginType> query, ODataQueryOptions<OriginType> queryOptions)
		{
			return base.HandlePageResult<OriginType, RetType>( query, queryOptions );
		}

		public IHttpActionResult HandleValidationFailure<T>(IList<T> validationFailure) where T : ValidationFailure
		{
			return base.HandleValidationFailure<T>( validationFailure );
		}
	}
	public class ApiControllerBaseDummy
	{
		public int Id { get; set; }
	}

	public class ApiControllerBaseDummyQuery
	{
		public int Id { get; set; }
	}
}
