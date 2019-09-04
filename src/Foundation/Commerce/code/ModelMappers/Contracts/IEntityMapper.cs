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

namespace Wooli.Foundation.Commerce.ModelMappers
{
    using System.Collections.Generic;

    using Sitecore.Commerce.Entities;
    using Sitecore.Commerce.Entities.Carts;
    using Sitecore.Commerce.Entities.Customers;
    using Sitecore.Commerce.Entities.Shipping;

    using Wooli.Foundation.Commerce.Models;
    using Wooli.Foundation.Connect.Models;

    public interface IEntityMapper
    {
        PartyEntity MapToPartyEntity(AddressModel item);

        List<PartyEntity> MapToPartyEntityList(IEnumerable<AddressModel> items);

        Party MapToParty(AddressModel item);

        ShippingInfoArgument MapToShippingInfoArgument(ShippingMethodModel item);

        List<ShippingInfoArgument> MapToShippingInfoArgumentList(IEnumerable<ShippingMethodModel> items);

        AddressModel MapToAddress(Party item);

        FederatedPaymentArgs MapToFederatedPaymentArgs(FederatedPaymentModel model);

        ShippingMethodModel MapToShippingMethodModel(ShippingMethod shippingMethod);

        ShippingOptionModel MapToShippingOptionModel(ShippingOption shipppingOption);

        FederatedPaymentModel MapToFederatedPayment(PaymentInfo x);

        ShippingMethodModel MapToShippingMethodModel(ShippingInfo x);

        CommerceUserModel MapToCommerceUserModel(CommerceUser x);

        IEnumerable<Models.CountryRegionModel> MapToCountryRegionModel(IEnumerable<ICountryRegionModel> x);
    }
}
