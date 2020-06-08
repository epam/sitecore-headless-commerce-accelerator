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
    using System.Linq;

    using AutoMapper;

    using Models.Entities.Cart;

    using Providers;

    using Sitecore.Commerce.Engine.Connect.Entities;
    using Sitecore.Commerce.Entities.Prices;
    using Sitecore.Diagnostics;

    using Connect = Sitecore.Commerce.Entities.Carts;

    [ExcludeFromCodeCoverage]
    public class CartProfile : Profile
    {
        public CartProfile(ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(currencyProvider, nameof(currencyProvider));

            this.CreateMap<Connect.Cart, Cart>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.CartLines, opt => opt.Ignore())
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Parties))
                .ForMember(
                    dest => dest.Adjustments,
                    opt => opt.MapFrom(src => src.Adjustments.Select(a => a.Description).ToList()))
                .ReverseMap();

            this.CreateMap<CommerceCart, Cart>()
                .IncludeBase<Connect.Cart, Cart>()
                .ReverseMap();

            this.CreateMap<Connect.CartLine, CartLine>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalCartLineId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Variant, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<CommerceCartLine, CartLine>()
                .IncludeBase<Connect.CartLine, CartLine>()
                .ReverseMap();

            this.CreateMap<TotalPrice, Total>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Total))
                .ForMember(
                    dest => dest.TaxTotal,
                    opt => opt.MapFrom(
                        src => new TaxTotal
                        {
                            Amount = src.TaxTotal
                        }))
                .ForMember(src => src.Description, opt => opt.Ignore());

            this.CreateMap<Total, TotalPrice>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.TaxTotal, opt => opt.MapFrom(src => src.TaxTotal.Amount))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyCode))
                .ForMember(
                    dest => dest.CurrencySymbol,
                    opt => opt.MapFrom(src => currencyProvider.GetCurrencySymbolByCode(src.CurrencyCode)))
                .ForAllOtherMembers(opt => opt.Ignore());

            this.CreateMap<CommerceTotal, TotalPrice>()
                .ForMember(
                    dest => dest.TotalSavings,
                    opt => opt.MapFrom(src => src.LineItemDiscountAmount + src.OrderLevelDiscountAmount))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal))
                .ForMember(dest => dest.HandlingTotal, opt => opt.MapFrom(src => src.HandlingTotal))
                .ForMember(dest => dest.ShippingTotal, opt => opt.MapFrom(src => src.ShippingTotal))
                .IncludeBase<Total, TotalPrice>();
        }
    }
}