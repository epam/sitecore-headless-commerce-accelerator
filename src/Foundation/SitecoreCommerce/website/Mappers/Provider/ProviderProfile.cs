using AutoMapper;
using Sitecore.Commerce.Engine.Connect.Entities;
using Sitecore.Commerce.Entities.Carts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCA.Foundation.SitecoreCommerce.Mappers.Provider
{
    public class ProviderProfile: Profile
    {
        public ProviderProfile()
        {
            CreateMap<CartLine, ConnectBase.Entities.CommerceCartLine>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceParty, CommerceParty>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceCartProduct, CommerceCartProduct>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceCartLine, CommerceCartLine>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceInventoryProduct, CommerceInventoryProduct>().ReverseMap();
            CreateMap<CommerceOrder, ConnectBase.Entities.CommerceOrder>()
                .ForMember(dest => dest.OrderForms, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceOrderForm, CommerceOrderForm>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceOrderHeader, CommerceOrderHeader>().ReverseMap();            
            CreateMap<ConnectBase.Entities.CommercePrice, CommercePrice>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceTotal, CommerceTotal>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceCart, CommerceCart>()
                .ForMember(dest => dest.OrderForms, opt => opt.UseValue(new ReadOnlyCollection<CommerceOrderForm>(new List<CommerceOrderForm>{ new CommerceOrderForm() })))
                .ReverseMap()
                .ForMember(dest => dest.OrderForms, opt => opt.UseValue(new ReadOnlyCollection<ConnectBase.Entities.CommerceOrderForm>(new List<ConnectBase.Entities.CommerceOrderForm> { new ConnectBase.Entities.CommerceOrderForm() })));
            CreateMap<ConnectBase.Entities.CommerceShippingInfo, CommerceShippingInfo>().ReverseMap();
            CreateMap<ConnectBase.Entities.CommerceParty, CommerceParty>().ReverseMap();
        }
    }
}