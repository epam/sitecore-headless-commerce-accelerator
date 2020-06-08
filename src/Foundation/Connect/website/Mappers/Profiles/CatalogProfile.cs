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

namespace HCA.Foundation.Connect.Mappers.Profiles
{
    using System.Diagnostics.CodeAnalysis;

    using AutoMapper;

    using Models.Catalog;

    using Sitecore.Commerce.Engine.Connect.Entities;

    using StockStatus = Sitecore.Commerce.Entities.Inventory.StockStatus;

    [ExcludeFromCodeCoverage]
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            this.CreateMap<StockStatus, Models.Catalog.StockStatus>();

            this.CreateMap<Product, CommerceInventoryProduct>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VariantId, opt => opt.Ignore());

            this.CreateMap<Variant, CommerceInventoryProduct>()
                .ForMember(dest => dest.VariantId, opt => opt.MapFrom(src => src.Id));
        }
    }
}