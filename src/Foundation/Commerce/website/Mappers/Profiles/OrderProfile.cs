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

namespace HCA.Foundation.Commerce.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Models.Entities.Cart;
    using Models.Entities.Order;

    [ExcludeFromCodeCoverage]
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            this.CreateMap<Cart, Order>()
                .ForMember(dest => dest.OrderID, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.TrackingNumber, opt => opt.Ignore())
                .ForMember(dest => dest.IsOfflineOrder, opt => opt.Ignore());

            this.CreateMap<Sitecore.Commerce.Entities.Orders.Order, Order>()
                .ForMember(dest => dest.OrderID, opt => opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TrackingNumber, opt => opt.MapFrom(src => src.TrackingNumber))
                .ForMember(dest => dest.IsOfflineOrder, opt => opt.MapFrom(src => src.IsOfflineOrder))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}