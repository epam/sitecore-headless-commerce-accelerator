//    Copyright 2020 EPAM Systems, Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace HCA.Foundation.Base.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Models.Result;

    using Sitecore.Diagnostics;

    public static class ApiControllerExtensions
    {
        private static readonly JsonMediaTypeFormatter JsonMediaTypeFormatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = Constants.JsonSerializerSettings
        };

        public static IHttpActionResult JsonError(
            this ApiController controller,
            string[] errorMessages,
            HttpStatusCode statusCode,
            Exception e = null,
            object tempData = null)
        {
            var result = new ErrorsJsonResultModel
            {
                Status = "error",
                Error = errorMessages.FirstOrDefault(),
                Errors = errorMessages,
                ExceptionMessage = e?.Message,
                TempData = tempData
            };

            return ResolveDependencies(
                controller,
                (negotiator, request, formatters) => new NegotiatedContentResult<ErrorsJsonResultModel>(
                    statusCode,
                    result,
                    negotiator,
                    request,
                    formatters));
        }

        public static IHttpActionResult JsonError(
            this ApiController controller,
            string errorMessage,
            HttpStatusCode statusCode,
            Exception e = null,
            object tempData = null)
        {
            var result = new ErrorJsonResultModel
            {
                Status = "error",
                Error = errorMessage,
                ExceptionMessage = e?.Message,
                TempData = tempData
            };

            return ResolveDependencies(
                controller,
                (negotiator, request, formatters) => new NegotiatedContentResult<ErrorJsonResultModel>(
                    statusCode,
                    result,
                    negotiator,
                    request,
                    formatters));
        }

        public static IHttpActionResult JsonOk<TData>(this ApiController controller, TData data = null)
            where TData : class
        {
            var result = new OkJsonResultModel<TData>
            {
                Status = "ok",
                Data = data
            };

            return ResolveDependencies(
                controller,
                (negotiator, request, formatters) =>
                    new OkNegotiatedContentResult<OkJsonResultModel<TData>>(result, negotiator, request, formatters));
        }

        private static IHttpActionResult ResolveDependencies(
            ApiController controller,
            Func<IContentNegotiator, HttpRequestMessage, IEnumerable<MediaTypeFormatter>, IHttpActionResult> resultFunc)
        {
            Assert.ArgumentNotNull(controller, nameof(controller));
            Assert.ArgumentNotNull(resultFunc, nameof(resultFunc));

            // Extracting default configuration from controller
            var configuration = controller.Configuration;
            if (configuration == null)
            {
                throw new InvalidOperationException(
                    $"The controller {controller.GetType().FullName} configuration must not be null.");
            }

            var contentNegotiator = configuration.Services.GetContentNegotiator();
            if (contentNegotiator == null)
            {
                throw new InvalidOperationException(
                    $"The controller {controller.GetType().FullName} do not have a content configuration.");
            }

            var request = controller.Request;
            if (request == null)
            {
                throw new InvalidOperationException(
                    $"The controller {controller.GetType().FullName} request must not be null.");
            }

            // Modifying custom configuration
            var defaultJsonMediaTypeFormatter = configuration.Formatters.JsonFormatter;
            IList<MediaTypeFormatter> formatters = configuration.Formatters.ToList();
            var indexOfJsonMediaTypeFormatter = formatters.IndexOf(defaultJsonMediaTypeFormatter);
            formatters[indexOfJsonMediaTypeFormatter] = JsonMediaTypeFormatter;

            return resultFunc(contentNegotiator, request, formatters);
        }
    }
}