﻿using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NFe.Controller.Testes.Common;
using NFe.Controller.Testes.Initializer;
using NFe.Dominio.Exceptions;
using NUnit.Framework;
using NFe.WebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace NFe.Controller.Tests.Common
{
    [TestFixture]
    public class ApiControllerBaseTests : TestControllerBase
    {
        private ApiControllerBaseFake _apiControllerBase;
        private Mock<ApiControllerBaseDummy> _dummy;


        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _apiControllerBase = new ApiControllerBaseFake()
            {
                Request = request
            };
            _dummy = new Mock<ApiControllerBaseDummy>();
        }

        #region HandleCallback

        [Test]
        public void Base_Controller_HandleCallback_ShouldHandleBusinessException()
        {
            //Arrange
            var message = "message error test";
            var exception = new BusinessException(ErrorCodes.AlreadyExists, message);
            // Action
            var callback = _apiControllerBase.HandleCallback<ApiControllerBaseDummy>(() => throw exception);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            httpResponse.Content.ErrorCode.Should().Be((int)ErrorCodes.AlreadyExists);
            httpResponse.Content.ErrorMessage.Should().Be(message);
        }

        [Test]
        public void Base_Controller_HandleCallback_ShouldHandleRuntimeException()
        {
            //Arrange
            var message = "message error test";
            var exception = new Exception(message);
            // Action
            var callback = _apiControllerBase.HandleCallback<ApiControllerBaseDummy>(() => throw exception);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            httpResponse.Content.ErrorCode.Should().Be((int)ErrorCodes.Unhandled);
            httpResponse.Content.ErrorMessage.Should().Be(message);
        }

        #endregion

        #region HandleQuery

        [Test]
        public void Base_Controller_HandleQuery_ShouldBeOk()
        {
            //Arrange
            var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();
            var odataOptions = GetOdataQueryOptions<ApiControllerBaseDummy>(_apiControllerBase);
            // Action
            var callback = _apiControllerBase.HandleQuery<ApiControllerBaseDummy, ApiControllerBaseDummyQuery>(query, odataOptions);
            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ApiControllerBaseDummyQuery>>>().Subject;
            httpResponse.Content.Should().NotBeNull();
        }

        #endregion

        #region HandlePageResult
        [Test]
        public void Base_Controller_HandlePageResult_ShouldBeOk()
        {
            //Arrange
            var query = new List<ApiControllerBaseDummy>() { _dummy.Object }.AsQueryable();
            var odataOptions = GetOdataQueryOptions<ApiControllerBaseDummy>(_apiControllerBase);
            // Action
            var callback = _apiControllerBase.HandlePageResult<ApiControllerBaseDummy, ApiControllerBaseDummyQuery>(query, odataOptions);
            //Assert
            var contentResponse = callback.Should().BeOfType<PageResult<ApiControllerBaseDummyQuery>>().Subject;
            contentResponse.Should().NotBeNull();
        }

        #endregion

        #region HandleValidationFailure

        [Test]
        public void Base_Controller_HandleValidationFailure_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var validationFailure = new ValidationFailure("", ((int)ErrorCodes.Unhandled).ToString());
            IList<ValidationFailure> errors = new List<ValidationFailure>() { validationFailure };
            // Action
            var callback = _apiControllerBase.HandleValidationFailure(errors);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.FirstOrDefault().Should().Be(validationFailure);
        }

        #endregion
    }
}
