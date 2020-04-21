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

namespace HCA.Foundation.Commerce.Builders.Order
{
    using Models.Entities.Order;

    /// <summary>
    /// Builds Order entities
    /// </summary>
    public interface IOrderBuilder
    {
        /// <summary>
        /// Builds Order entity
        /// </summary>
        /// <param name="source">Source object</param>
        /// <returns>Instance of Order type</returns>
        Order Build(Sitecore.Commerce.Entities.Orders.Order source);
    }
}