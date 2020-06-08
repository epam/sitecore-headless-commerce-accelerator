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

namespace HCA.Foundation.Base.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Extensions;

    using Models.Result;

    public class BaseController : Controller
    {
        [NonAction]
        public virtual ActionResult Execute<TData>(Func<Result<TData>> function) where TData : class
        {
            return this.Execute(
                function,
                result => result.Success
                    ? this.JsonOk(result.Data)
                    : this.JsonError(
                        result.Errors?.ToArray(),
                        HttpStatusCode.InternalServerError,
                        tempData: result.Data));
        }

        [NonAction]
        public virtual ActionResult Execute<TData>(
            Func<Result<TData>> function,
            Func<Result<TData>, ActionResult> resolve)
            where TData : class
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    var errorMessages =
                        this.ModelState.SelectMany(state => state.Value?.Errors.Select(error => error.ErrorMessage))
                            .ToArray();
                    return this.JsonError(errorMessages, HttpStatusCode.BadRequest);
                }

                var result = function.Invoke();

                return resolve(result);
            }
            catch (Exception exception)
            {
                return this.JsonError(exception.Message, HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}