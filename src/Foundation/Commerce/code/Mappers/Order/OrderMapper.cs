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

namespace HCA.Foundation.Commerce.Mappers.Order
{
    using Base.Mappers;

    using DependencyInjection;

    using Models.Entities.Cart;
    using Models.Entities.Order;

    using Profiles;

    using Sitecore = Sitecore.Commerce.Entities.Orders;

    [Service(typeof(IOrderMapper), Lifetime = Lifetime.Singleton)]
    public class OrderMapper : ProfileMapper<OrderProfile>, IOrderMapper
    {
        public Order Map(Cart cart)
        {
            return this.InnerMapper.Map<Cart, Order>(cart);
        }

        public Order Map(Sitecore.Order order, Order result)
        {
            return this.InnerMapper.Map(order, result);
        }
    }
}