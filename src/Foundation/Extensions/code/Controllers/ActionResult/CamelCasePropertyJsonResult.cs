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
    using Utils;

    public class CamelCasePropertyJsonResult : JsonResult
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = Constants.JsonSerializerSettings;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public override void ExecuteResult(ControllerContext context)
        {
            Assert.ArgumentNotNull(context, nameof(context));

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("GET is not allowed.");

            if (Data == null) return;

            var response = context.HttpContext.Response;

            if (ContentEncoding != null) response.ContentEncoding = ContentEncoding;

            response.StatusCode = (int) StatusCode;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            var scriptSerializer = JsonSerializer.Create(JsonSerializerSettings);
            scriptSerializer.Serialize(response.Output, Data);
        }
    }
}