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

namespace Wooli.Foundation.Commerce.Mappers.Order
{
    using Base.Mappers;

    using Models.Entities.Order;

    using SitecoreOrder = Sitecore.Commerce.Entities.Orders.Order;

    public interface IOrderMapper : IMapper
    {
        /// <summary>
        /// Map Sitecore Order entity to local Order entity
        /// </summary>
        /// <param name="order">Sitecore Order entity</param>
        /// <returns>Order entity</returns>
        Order Map(SitecoreOrder order);
    }
}