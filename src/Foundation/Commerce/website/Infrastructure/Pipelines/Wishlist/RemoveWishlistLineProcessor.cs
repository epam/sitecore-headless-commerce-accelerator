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

namespace HCA.Foundation.Commerce.Infrastructure.Pipelines.Wishlist
{
    using Microsoft.Extensions.DependencyInjection;

    using Repositories.Wishlist;

    using Sitecore.Commerce.Engine.Connect.Pipelines;
    using Sitecore.Commerce.Pipelines;
    using Sitecore.Commerce.Services.WishLists;
    using Sitecore.DependencyInjection;
    using Sitecore.Diagnostics;

    public class RemoveWishlistLineProcessor : PipelineProcessor
    {
        private readonly IWishlistRepository repository;

        public RemoveWishlistLineProcessor()
        {
            this.repository = ServiceLocator.ServiceProvider.GetService<IWishlistRepository>();
        }

        public override void Process(ServicePipelineArgs args)
        {
            Assert.ArgumentNotNull(args, nameof(ServicePipelineArgs));

            var request = args.Request as RemoveWishListLinesRequest;
            var result = args.Result as RemoveWishListLinesResult;

            Assert.ArgumentNotNull(request, nameof(RemoveWishListLinesRequest));
            Assert.ArgumentNotNull(result, nameof(RemoveWishListLinesResult));

            result.WishList = this.repository.RemoveWishlistLines(request.WishList, request.LineIds);
        }
    }
}