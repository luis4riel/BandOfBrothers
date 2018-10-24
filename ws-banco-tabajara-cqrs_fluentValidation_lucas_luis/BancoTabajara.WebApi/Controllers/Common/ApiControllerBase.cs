﻿using BancoTabajara.Dominio.Excecoes;
using BancoTabajara.WebApi.Excessoes;
using BancoTabajara.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using FluentValidation.Results;
using System.Linq;
using BancoTabajara.Infra.Csv;

namespace BancoTabajara.WebApi.Controllers.Common
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

        protected IHttpActionResult HandleQuery<TResult>(IQueryable<TResult> query)
        {
            if (Request.Headers.Accept.Contains(MediaTypeWithQualityHeaderValue.Parse(MediaTypes.Csv)))
                return ResponseMessage(HandleCSVFile(query));

            return Ok(query.ToList());
        }

        protected IHttpActionResult HandleQueryable<TSource>(IQueryable<TSource> query)
        {
            if (Request.Headers.Accept.Contains(MediaTypeWithQualityHeaderValue.Parse(MediaTypes.Csv)))
                return ResponseMessage(HandleCSVFile(query));

            return Ok(query.ToList());
        }

        protected IHttpActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            var exceptionPayload = ExceptionPayload.New(exceptionToHandle);
            return exceptionToHandle is ExcecaoDeNegocio ?
                Content(HttpStatusCode.BadRequest, exceptionPayload) :
                Content(HttpStatusCode.InternalServerError, exceptionPayload);
        }

        protected IHttpActionResult HandleValidationFailure<T>(IList<T> validationFailure) where T : ValidationFailure
        {
            return Content(HttpStatusCode.BadRequest, validationFailure);
        }

        #endregion

        #region Utils
        private HttpResponseMessage HandleCSVFile<TResult>(IQueryable<TResult> query)
        {
            var csv = query.ToCsv<TResult>(";");
            var bytes = Encoding.UTF8.GetBytes(csv);
            var stream = new MemoryStream(bytes, 0, bytes.Length, false, true);

            var result = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(stream.GetBuffer()) };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("export{0}.csv", DateTime.UtcNow.ToString("yyyyMMddhhmmss"))
            };

            return result;
        }

        #endregion
    }
}
