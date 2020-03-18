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
    using System.Web.Mvc;

    using Foundation.Commerce.Controllers;
    using Foundation.Commerce.Models.Entities;
    using Foundation.Commerce.Models.Entities.Search;
    using Foundation.Commerce.Services.Catalog;

    using Mappers;

    using Models.Requests;

    using Sitecore.Diagnostics;

    public class SearchController : BaseController
    {
        private readonly IProductSearchService productSearchService;

        private readonly ICatalogEntityMapper catalogEntityMapper;

        public SearchController(IProductSearchService productSearchService, ICatalogEntityMapper catalogEntityMapper)
        {
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(catalogEntityMapper, nameof(catalogEntityMapper));

            this.productSearchService = productSearchService;
            this.catalogEntityMapper = catalogEntityMapper;
        }

        [HttpGet]
        [ActionName("products")]
        public ActionResult GetProducts(ProductsSearchRequest searchRequest)
        {
            var searchOptions = this.catalogEntityMapper.Map<ProductsSearchRequest, ProductsSearchOptions>(searchRequest);

            return this.Execute(() => this.productSearchService.GetProducts(searchOptions));
        }
    }
}