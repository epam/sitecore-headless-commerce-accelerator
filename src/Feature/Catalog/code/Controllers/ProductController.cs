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

namespace Wooli.Feature.Catalog.Controllers
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Web;
    using System.Web.Http;

    using Foundation.Commerce.Context;
    using Foundation.Commerce.Models;
    using Foundation.Commerce.Repositories;
    using Foundation.Commerce.Utils;
    using Foundation.Extensions.Extensions;

    [RoutePrefix(Constants.CommerceRoutePrefix + "/product")]
    public class ProductController : ApiController
    {
        private readonly ICatalogRepository catalogRepository;

        private readonly IProductListRepository productListRepository;

        private readonly IStorefrontContext storefrontContext;

        private readonly IVisitorContext visitorContext;

        public ProductController(
            IStorefrontContext storefrontContext,
            IVisitorContext visitorContext,
            ICatalogRepository catalogRepository,
            IProductListRepository productListRepository)
        {
            this.storefrontContext = storefrontContext;
            this.visitorContext = visitorContext;
            this.catalogRepository = catalogRepository;
            this.productListRepository = productListRepository;
        }

        [Route("get/{id}")]
        public IHttpActionResult Get(string id)
        {
            var productModel = this.catalogRepository.GetProduct(id);
            if (productModel == null)
            {
                return this.JsonError("Not Found", HttpStatusCode.NotFound);
            }

            return this.JsonOk(productModel);
        }

        [Route("search")]
        public IHttpActionResult GetProductList(
            [FromUri(Name = "q")] string searchKeyword = null,
            [FromUri(Name = "pg")] int? page = null,
            [FromUri(Name = "f")] string facetValues = null,
            [FromUri(Name = "s")] string sortField = null,
            [FromUri(Name = "ps")] int? pageSize = null,
            [FromUri(Name = "sd")] SortDirection? sortDirection = null,
            [FromUri(Name = "cci")] string currentCatalogItemId = null,
            [FromUri(Name = "ci")] string currentItemId = null)
        {
            var facetValuesCollection = !string.IsNullOrEmpty(facetValues)
                ? HttpUtility.ParseQueryString(facetValues)
                : new NameValueCollection();

            var model = this.productListRepository.GetProductList(
                this.visitorContext,
                currentItemId,
                currentCatalogItemId,
                searchKeyword,
                page,
                facetValuesCollection,
                sortField,
                pageSize,
                sortDirection);

            return this.JsonOk(model);
        }
    }
}