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

namespace Wooli.Foundation.Commerce.Services.Catalog
{
    using Base.Models;

    using DependencyInjection;

    using Models;
    using Models.Entities.Catalog;

    [Service(typeof(ICatalogService), Lifetime = Lifetime.Transient)]
    public class CatalogService : ICatalogService
    {
        public Result<Product> GetProduct(string productId)
        {
            throw new System.NotImplementedException();
        }

        public Result<Product> GetCurrentProduct()
        {
            throw new System.NotImplementedException();
        }

        public Result<Category> GetCurrentCategory()
        {
            throw new System.NotImplementedException();
        }
    }
}