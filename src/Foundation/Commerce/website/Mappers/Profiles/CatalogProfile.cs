﻿//    Copyright 2020 EPAM Systems, Inc.
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

    using Connect.Mappers.Resolvers;
    using HCA.Foundation.ConnectBase.Entities;
    using Models.Entities.Catalog;
    using Models.Entities.Search;
    using Providers.Currency;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    using Connect = Connect.Models.Catalog;

    [ExcludeFromCodeCoverage]
    public class CatalogProfile : Profile
    {
        public CatalogProfile(ICurrencyProvider currencyProvider)
        {
            Assert.ArgumentNotNull(currencyProvider, nameof(currencyProvider));

            this.CreateMap<Connect.BaseProduct, BaseProduct>()
                .ForMember(
                    dest => dest.StockStatusName,
                    opt => opt.MapFrom(src => src.StockStatus != null ? src.StockStatus.Name : null))
                .ForMember(
                    dest => dest.CurrencySymbol,
                    opt => opt.MapFrom(src => currencyProvider.GetCurrencySymbolByCode(src.CurrencyCode)));

            this.CreateMap<Connect.Product, Product>()
                .IncludeBase<Connect.BaseProduct, BaseProduct>();

            this.CreateMap<Product, ProductSuggestion>()
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ImageUrls.FirstOrDefault()))
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.AdjustedPrice ?? src.ListPrice));

            this.CreateMap<Connect.Variant, Variant>()
                .IncludeBase<Connect.BaseProduct, BaseProduct>()
                .ForMember(dest => dest.VariantId, opt => opt.MapFrom(src => src.Id));

            this.CreateMap<Variant, CommerceCartProduct>()
                .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.VariantId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForAllMembers(opt => opt.Ignore());
                
             this.CreateMap<Item, Category>()
                .ForMember(dest => dest.SitecoreId, opt => opt.MapFrom(src => src["SitecoreId"]))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src["Name"]))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src["DisplayName"]))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src["Description"]))
                .ForMember(
                    dest => dest.ParentCatalogList,
                    opt => opt.ResolveUsing<MultilistFieldResolver, string>(src => src["ParentCatalogList"]))
                .ForMember(
                    dest => dest.ChildrenCategoryList,
                    opt => opt.ResolveUsing<MultilistFieldResolver, string>(src => src["ChildrenCategoryList"]));
        }
    }
}