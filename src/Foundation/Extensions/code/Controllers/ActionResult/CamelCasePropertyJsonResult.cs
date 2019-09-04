//    Copyright 2019 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Extensions.Controllers.ActionResult
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    using Sitecore.Diagnostics;

    using Wooli.Foundation.Extensions.Utils;

    public class CamelCasePropertyJsonResult : JsonResult
    {
        private static readonly JsonSerializerSettings JsonSerialiserSettings = Constants.JsonSerialiserSettings;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public override void ExecuteResult(ControllerContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));

            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("GET is not allowed.");
            }

            if (this.Data == null)
            {
                return;
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            response.StatusCode = (int)this.StatusCode;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            JsonSerializer scriptSerializer = JsonSerializer.Create(JsonSerialiserSettings);
            scriptSerializer.Serialize(response.Output, this.Data);
        }
    }
}
