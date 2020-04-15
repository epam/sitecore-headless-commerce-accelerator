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

namespace Wooli.Foundation.Commerce.Mappers.Cart
{
    using AutoMapper;

    using DependencyInjection;

    using Profiles;

    using Providers;

    using Sitecore.Diagnostics;

    using Mapper = Base.Mappers.Mapper;

    [Service(typeof(ICartMapper), Lifetime = Lifetime.Singleton)]
    public class CartMapper : Mapper, ICartMapper
    {
        public CartMapper(ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(currencyProvider, nameof(currencyProvider));

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CartProfile(currencyProvider));
                cfg.AddProfile<AddressProfile>();
                cfg.AddProfile<ShippingProfile>();
                cfg.AddProfile<PaymentProfile>();
            });
            this.InnerMapper = new AutoMapper.Mapper(mapperConfiguration);
        }
    }
}