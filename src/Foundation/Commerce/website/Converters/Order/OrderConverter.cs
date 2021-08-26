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

namespace HCA.Foundation.Commerce.Converters.Order
{
    using Cart;

    using DependencyInjection;

    using Mappers.Order;

    using Models.Entities.Order;

    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Diagnostics;

    [Service(typeof(IOrderConverter), Lifetime = Lifetime.Singleton)]
    public class OrderConverter : IOrderConverter
    {
        private readonly ICartConverter<Cart> cartConverter;

        private readonly IOrderMapper orderMapper;

        public OrderConverter(ICartConverter<Cart> cartConverter, IOrderMapper orderMapper)
        {
            Assert.ArgumentNotNull(orderMapper, nameof(orderMapper));
            Assert.ArgumentNotNull(cartConverter, nameof(cartConverter));

            this.orderMapper = orderMapper;
            this.cartConverter = cartConverter;
        }

        public Order Build(Sitecore.Commerce.Entities.Orders.Order source)
        {
            return this.orderMapper.Map(
                source,
                this.orderMapper.Map(
                    this.cartConverter.Convert(source)));
        }
    }
}