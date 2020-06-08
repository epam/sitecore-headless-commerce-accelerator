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
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Models.Result;

    public static class ControllerExtensions
    {
        public static ActionResult JsonError(
            this Controller controller,
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

            controller.Response.TrySkipIisCustomErrors = true;

            return new CamelCasePropertyJsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                StatusCode = statusCode
            };
        }

        public static ActionResult JsonError(
            this Controller controller,
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

            controller.Response.TrySkipIisCustomErrors = true;

            return new CamelCasePropertyJsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                StatusCode = statusCode
            };
        }

        public static ActionResult JsonOk<TData>(this Controller controller, TData data = null)
            where TData : class
        {
            var result = new OkJsonResultModel<TData>
            {
                Status = "ok",
                Data = data
            };

            return new CamelCasePropertyJsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}