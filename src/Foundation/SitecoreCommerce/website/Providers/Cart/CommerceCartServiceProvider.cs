using HCA.Foundation.ConnectBase.Entities;
using HCA.Foundation.ConnectBase.Pipelines.Arguments;
using HCA.Foundation.ConnectBase.Providers;
using HCA.Foundation.SitecoreCommerce.Mappers.Provider;
using Sitecore.Commerce.Engine.Connect;
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Services.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using AddShippingInfoRequest = HCA.Foundation.ConnectBase.Pipelines.Arguments.AddShippingInfoRequest;

namespace HCA.Foundation.SitecoreCommerce.Providers.Cart
{
    public class CommerceCartServiceProvider : CartServiceProviderBase
    {
        private readonly ProviderMapper mapper;

        public CommerceCartServiceProvider()
        {
            mapper = new ProviderMapper();
        }

        public override CartResult UpdateCartLines(UpdateCartLinesRequest request)
        {
            var req = new UpdateCartLinesRequest(
                request.Cart is CommerceCart cart ? cart.Convert() : request.Cart,
                request.Lines.Select(l => l is CommerceCartLine line ? line.Convert() : l));

            var res = base.UpdateCartLines(req);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override CartResult UpdateCart(UpdateCartRequest request)
        {
            var res = base.UpdateCart(request);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override RemoveShippingInfoResult RemoveShippingInfo(RemoveShippingInfoRequest request)
        {
            var res = base.RemoveShippingInfo(request);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override RemovePaymentInfoResult RemovePaymentInfo(RemovePaymentInfoRequest request)
        {
            var res = base.RemovePaymentInfo(request);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override RemovePartiesResult RemoveParties(RemovePartiesRequest request)
        {
            var res = base.RemoveParties(request);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override CartResult RemoveCartLines(RemoveCartLinesRequest request)
        {
            var res = base.RemoveCartLines(request);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override CartResult MergeCart(MergeCartRequest request)
        {
            var res = base.MergeCart(request);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override CartResult AddCartLines(AddCartLinesRequest request)
        {
            var req = new AddCartLinesRequest(
                request.Cart is CommerceCart cart ? cart.Convert() : request.Cart,
                request.Lines.Select(l => l is CommerceCartLine line ? line.Convert() : l));
            var res = base.AddCartLines(req);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override AddShippingInfoResult AddShippingInfo(Sitecore.Commerce.Services.Carts.AddShippingInfoRequest request)
        {
            var req = request is AddShippingInfoRequest ?
                new Sitecore.Commerce.Engine.Connect.Services.Carts.AddShippingInfoRequest(
                    request.Cart is CommerceCart cart ? cart.Convert() : request.Cart,
                    request.ShippingInfo.Select(s => s is CommerceShippingInfo info ? info.Convert() : s).ToList(),
                    (request as AddShippingInfoRequest).OrderShippingOptionType) :
                request;
            var res = base.AddShippingInfo(req);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            res.ShippingInfo = res.ShippingInfo.Select(s => s is Sitecore.Commerce.Engine.Connect.Entities.CommerceShippingInfo info ? info.Convert() : s).ToArray();
            return res;
        }

        public override AddPaymentInfoResult AddPaymentInfo(AddPaymentInfoRequest request)
        {
            var req = new AddPaymentInfoRequest(
                request.Cart is CommerceCart cart ? cart.Convert() : request.Cart,
                request.Payments.ToList());

            var res = base.AddPaymentInfo(req);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }

        public override CartResult LoadCart(LoadCartRequest request)
        {
            var req = request is LoadCartByNameRequest ?
                new Sitecore.Commerce.Engine.Connect.Pipelines.Arguments.LoadCartByNameRequest(request.Shop?.Name, request.CartId, request.UserId, request.Recalculate) :
                request;
            var res = base.LoadCart(req);
            res.Cart = res.Cart is Sitecore.Commerce.Engine.Connect.Entities.CommerceCart c ? c.Convert() : res.Cart;
            return res;
        }
    }
}