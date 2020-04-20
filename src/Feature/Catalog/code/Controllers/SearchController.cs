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

namespace HCA.Feature.Catalog.Controllers
{
    using System.Web.Mvc;

    using Foundation.Base.Controllers;
    using Foundation.Commerce.Models.Entities.Search;
    using Foundation.Commerce.Services.Search;

    using Mappers;

    using Models.Requests.Search;

    using Sitecore.Diagnostics;

    public class SearchController : BaseController
    {
        private readonly IProductSearchService productSearchService;

        private readonly ISearchMapper searchMapper;

        public SearchController(IProductSearchService productSearchService, ISearchMapper searchMapper)
        {
            Assert.ArgumentNotNull(productSearchService, nameof(productSearchService));
            Assert.ArgumentNotNull(searchMapper, nameof(searchMapper));

            this.productSearchService = productSearchService;
            this.searchMapper = searchMapper;
        }

        [HttpPost]
        [ActionName("products")]
        public ActionResult SearchProducts(ProductsSearchRequest searchRequest)
        {
            return this.Execute(
                () =>
                {
                    var searchOptions =
                        this.searchMapper.Map<ProductsSearchRequest, ProductSearchOptions>(searchRequest);
                    return this.productSearchService.GetProducts(searchOptions);
                });
        }
    }
}