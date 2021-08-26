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

namespace HCA.Foundation.Commerce.Services.Search
{
    using Base.Models.Result;

    using Converters.Search;

    using DependencyInjection;

    using Foundation.Search.Services.Product;

    using Models.Entities.Search;

    using Providers.Search;

    using Sitecore.Diagnostics;

    [Service(typeof(IProductSuggestionService), Lifetime = Lifetime.Singleton)]
    public class ProductSuggestionService : IProductSuggestionService
    {
        private readonly ISuggestionService suggestionService;

        private readonly ISuggestionOptionsConverter suggestionOptionsConverter;

        private readonly IProductSuggestionProvider productSuggestionProvider;

        public ProductSuggestionService(
            ISuggestionService suggestionService,
            ISuggestionOptionsConverter suggestionOptionsConverter,
            IProductSuggestionProvider productSuggestionProvider)
        {
            Assert.ArgumentNotNull(suggestionService, nameof(suggestionService));
            Assert.ArgumentNotNull(suggestionOptionsConverter, nameof(suggestionOptionsConverter));
            Assert.ArgumentNotNull(productSuggestionProvider, nameof(productSuggestionProvider));

            this.suggestionService = suggestionService;
            this.suggestionOptionsConverter = suggestionOptionsConverter;
            this.productSuggestionProvider = productSuggestionProvider;
        }

        public Result<ProductSuggestionResults> GetSuggestedProducts(string term)
        {
            Assert.ArgumentNotNull(term, nameof(term));

            var suggestionOptions = this.suggestionOptionsConverter.Convert(term);
            var suggestionResults = this.suggestionService.GetSuggestions(suggestionOptions);
            var suggestionProductsResult = this.productSuggestionProvider.GetProductSuggestion(suggestionResults);

            return new Result<ProductSuggestionResults>(suggestionProductsResult);
        }
    }
}