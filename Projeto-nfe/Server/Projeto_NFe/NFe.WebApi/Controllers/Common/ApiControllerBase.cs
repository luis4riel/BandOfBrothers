using AutoMapper.QueryableExtensions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using NFe.Dominio.Exceptions;
using NFe.WebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace NFe.WebApi.Controllers.Common
{
    public class ApiControllerBase : ApiController
    {

        #region Handlers
        protected IHttpActionResult HandleCallback<TSuccess>(Func<TSuccess> func)
        {
            try
            {
                return Ok(func());
            }
            catch (Exception e)
            {
                return HandleFailure(e);
            }
        }

        protected IHttpActionResult HandleQuery<TQueryOptions, TResult>(IQueryable<TQueryOptions> query, ODataQueryOptions<TQueryOptions> queryOptions)
        {
            return Ok(HandlePageResult<TQueryOptions, TResult>(query, queryOptions));
        }

        protected PageResult<RetType> HandlePageResult<OriginType, RetType>(IQueryable<OriginType> query, ODataQueryOptions<OriginType> queryOptions)
        {
            var queryResults = queryOptions.ApplyTo(query).Cast<OriginType>().ToList().AsQueryable();
            var pageResult = new PageResult<RetType>(queryResults.ProjectTo<RetType>(),
                                                    Request.ODataProperties().NextLink,
                                                    Request.ODataProperties().TotalCount);

            return pageResult;
        }
        #endregion


        #region HandlersFailure
        protected IHttpActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            var exceptionPayload = ExceptionPayload.New(exceptionToHandle);
            return exceptionToHandle is BusinessException ?
                Content(HttpStatusCode.BadRequest, exceptionPayload) :
                Content(HttpStatusCode.InternalServerError, exceptionPayload);
        }

        protected IHttpActionResult HandleValidationFailure<T>(IList<T> validationFailure) where T : ValidationFailure
        {
            return Content(HttpStatusCode.BadRequest, validationFailure);
        }

        #endregion    
    }
}