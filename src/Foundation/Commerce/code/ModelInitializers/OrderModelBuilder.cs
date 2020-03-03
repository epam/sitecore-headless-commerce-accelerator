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

namespace Wooli.Foundation.Commerce.ModelInitializers
{
    using DependencyInjection;
    using Models.Checkout;
    using Sitecore.Commerce.Entities.Orders;

    [Service(typeof(IOrderModelBuilder))]
    public class OrderModelBuilder : IOrderModelBuilder
    {
        private readonly ICartModelBuilder cartModelBuilder;

        public OrderModelBuilder(ICartModelBuilder cartModelBuilder)
        {
            this.cartModelBuilder = cartModelBuilder;
        }

        public OrderModel Initialize(Order model)
        {
            var result = cartModelBuilder.Initialize<OrderModel>(model);

            result.Status = model.Status;
            result.TrackingNumber = model.TrackingNumber;

            return result;
        }
    }
}