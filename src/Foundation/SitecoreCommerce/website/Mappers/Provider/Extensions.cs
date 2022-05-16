﻿using HCA.Foundation.ConnectBase.Entities;
using Sitecore.Commerce.Entities.Carts;
using System.Linq;

namespace HCA.Foundation.SitecoreCommerce.Mappers.Provider
{
    public static class Extensions
    {
        public static CommercePrice Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommercePrice price) => price == null ? null : new CommercePrice {
            Amount = price.Amount,
            Conditions = price.Conditions,
            CurrencyCode = price.CurrencyCode,
            Description = price.Description,
            HighestPricedVariant = price.HighestPricedVariant,
            ListPrice = price.ListPrice,
            LowestPricedVariant = price.LowestPricedVariant,
            LowestPricedVariantListPrice = price.LowestPricedVariantListPrice,
            PriceType = price.PriceType,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommercePrice Convert(this CommercePrice price) => price == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommercePrice
        {
            Amount = price.Amount,
            Conditions = price.Conditions,
            CurrencyCode = price.CurrencyCode,
            Description = price.Description,
            HighestPricedVariant = price.HighestPricedVariant,
            ListPrice = price.ListPrice,
            LowestPricedVariant = price.LowestPricedVariant,
            LowestPricedVariantListPrice = price.LowestPricedVariantListPrice,
            PriceType = price.PriceType,
        };
        public static CommerceCartProduct Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceCartProduct product) => product == null ? null : new CommerceCartProduct { 
            Adjustments = product.Adjustments,
            Description = product.Description,
            DisplayName = product.DisplayName,
            InStockDate = product.InStockDate,
            LineNumber = product.LineNumber,
            Options = product.Options,
            Price = Convert(product.Price as Sitecore.Commerce.Engine.Connect.Entities.CommercePrice),
            ProductCatalog = product.ProductCatalog,
            ProductCategory = product.ProductCategory,
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            ProductVariantId = product.ProductVariantId,
            ShippingDate = product.ShippingDate,
            SitecoreProductItemId = product.SitecoreProductItemId,
            StockStatus = product.StockStatus,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceCartProduct Convert(this CommerceCartProduct product) => product == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceCartProduct
        {
            Adjustments = product.Adjustments,
            Description = product.Description,
            DisplayName = product.DisplayName,
            InStockDate = product.InStockDate,
            LineNumber = product.LineNumber,
            Options = product.Options,
            Price = Convert(product.Price as CommercePrice),
            ProductCatalog = product.ProductCatalog,
            ProductCategory = product.ProductCategory,
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            ProductVariantId = product.ProductVariantId,
            ShippingDate = product.ShippingDate,
            SitecoreProductItemId = product.SitecoreProductItemId,
            StockStatus = product.StockStatus,
        };
        public static CommerceTotal Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceTotal total) => total == null ? null : new CommerceTotal {
            Amount = total.Amount,
            CurrencyCode = total.CurrencyCode,
            Description = total.Description,
            HandlingTotal = total.HandlingTotal,
            LineItemDiscountAmount = total.LineItemDiscountAmount,
            OrderLevelDiscountAmount = total.OrderLevelDiscountAmount,
            ShippingTotal = total.ShippingTotal,
            Subtotal = total.Subtotal,
            TaxTotal = total.TaxTotal,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceTotal Convert(this CommerceTotal total) => total == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceTotal
        {
            Amount = total.Amount,
            CurrencyCode = total.CurrencyCode,
            Description = total.Description,
            HandlingTotal = total.HandlingTotal,
            LineItemDiscountAmount = total.LineItemDiscountAmount,
            OrderLevelDiscountAmount = total.OrderLevelDiscountAmount,
            ShippingTotal = total.ShippingTotal,
            Subtotal = total.Subtotal,
            TaxTotal = total.TaxTotal,
        };
        public static CommerceCartLine Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceCartLine line) => line == null ? null : new CommerceCartLine {
            Adjustments = line.Adjustments,
            AllowBackordersAndPreorders = line.AllowBackordersAndPreorders,
            BackorderQuantity = line.BackorderQuantity,
            Created = line.Created,
            ExternalCartLineId = line.ExternalCartLineId,
            FreeGiftListPrice = line.FreeGiftListPrice,
            FreeGiftSellPrice = line.FreeGiftSellPrice,
            Index = line.Index,
            InStockQuantity = line.InStockQuantity,
            InventoryCondition = line.InventoryCondition,
            IsFreeGift = line.IsFreeGift,
            LastModified = line.LastModified,
            LineNumber = line.LineNumber,
            ModifiedBy = line.ModifiedBy,
            OrderFormId = line.OrderFormId,
            PreorderQuantity = line.PreorderQuantity,
            Product = Convert(line.Product as Sitecore.Commerce.Engine.Connect.Entities.CommerceCartProduct),
            PromotionId = line.PromotionId,
            Quantity = line.Quantity,
            ShippingAddressId = line.ShippingAddressId,
            ShippingMethodId = line.ShippingMethodId,
            ShippingMethodName = line.ShippingMethodName,
            Status = line.Status,
            SubLines = line.SubLines.Select(l => Convert(l as Sitecore.Commerce.Engine.Connect.Entities.CommerceCartLine)).ToList<CartLine>(),
            Total = Convert(line.Total as Sitecore.Commerce.Engine.Connect.Entities.CommerceTotal),
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceCartLine Convert(this CommerceCartLine line) => line == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceCartLine
        {
            Adjustments = line.Adjustments,
            AllowBackordersAndPreorders = line.AllowBackordersAndPreorders,
            BackorderQuantity = line.BackorderQuantity,
            Created = line.Created,
            ExternalCartLineId = line.ExternalCartLineId,
            FreeGiftListPrice = line.FreeGiftListPrice,
            FreeGiftSellPrice = line.FreeGiftSellPrice,
            Index = line.Index,
            InStockQuantity = line.InStockQuantity,
            InventoryCondition = line.InventoryCondition,
            IsFreeGift = line.IsFreeGift,
            LastModified = line.LastModified,
            LineNumber = line.LineNumber,
            ModifiedBy = line.ModifiedBy,
            OrderFormId = line.OrderFormId,
            PreorderQuantity = line.PreorderQuantity,
            Product = Convert(line.Product as CommerceCartProduct),
            PromotionId = line.PromotionId,
            Quantity = line.Quantity,
            ShippingAddressId = line.ShippingAddressId,
            ShippingMethodId = line.ShippingMethodId,
            ShippingMethodName = line.ShippingMethodName,
            Status = line.Status,
            SubLines = line.SubLines.Select(l => Convert(l as CommerceCartLine)).ToList<CartLine>(),
            Total = Convert(line.Total as CommerceTotal),
        };
        public static CommerceParty Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceParty party) => party == null ? null : new CommerceParty {
            Address1 = party.Address1,
            Address2 = party.Address2,
            City = party.City,
            Country = party.Country,
            Company = party.Company,
            CountryCode = party.CountryCode,
            Email = party.Email,
            EveningPhoneNumber = party.EveningPhoneNumber,
            ExternalId = party.ExternalId,
            Facet = party.Facet,
            FaxNumber = party.FaxNumber,
            FirstName = party.FirstName,
            LastName = party.LastName,
            IsPrimary = party.IsPrimary,
            Name = party.Name,
            PartyId = party.PartyId,
            PhoneNumber = party.PhoneNumber,
            RegionCode = party.RegionCode,
            RegionName = party.RegionName,
            State = party.State,
            UserProfileAddressId = party.UserProfileAddressId,
            ZipPostalCode = party.ZipPostalCode,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceParty Convert(this CommerceParty party) => party == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceParty
        {
            Address1 = party.Address1,
            Address2 = party.Address2,
            City = party.City,
            Country = party.Country,
            Company = party.Company,
            CountryCode = party.CountryCode,
            Email = party.Email,
            EveningPhoneNumber = party.EveningPhoneNumber,
            ExternalId = party.ExternalId,
            Facet = party.Facet,
            FaxNumber = party.FaxNumber,
            FirstName = party.FirstName,
            LastName = party.LastName,
            IsPrimary = party.IsPrimary,
            Name = party.Name,
            PartyId = party.PartyId,
            PhoneNumber = party.PhoneNumber,
            RegionCode = party.RegionCode,
            RegionName = party.RegionName,
            State = party.State,
            UserProfileAddressId = party.UserProfileAddressId,
            ZipPostalCode = party.ZipPostalCode,
        };
        public static CommerceOrderForm Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceOrderForm form) => form == null ? null : new CommerceOrderForm {
            BillingAddressId = form.BillingAddressId,
            Created = form.Created,
            LastModified = form.LastModified,
            ModifiedBy = form.ModifiedBy,
            Name = form.Name,
            OrderFormId = form.OrderFormId,
            PromoUserIdentity = form.PromoUserIdentity,
            Status = form.Status,
            Total = Convert(form.Total)
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceOrderForm Convert(this CommerceOrderForm form) => form == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceOrderForm
        {
            BillingAddressId = form.BillingAddressId,
            Created = form.Created,
            LastModified = form.LastModified,
            ModifiedBy = form.ModifiedBy,
            Name = form.Name,
            OrderFormId = form.OrderFormId,
            PromoUserIdentity = form.PromoUserIdentity,
            Status = form.Status,
            Total = Convert(form.Total)
        };
        public static CommerceShippingInfo Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceShippingInfo info) => info == null ? null : new CommerceShippingInfo {
            ElectronicDeliveryEmail = info.ElectronicDeliveryEmail,
            ElectronicDeliveryEmailContent = info.ElectronicDeliveryEmailContent,
            ExternalId = info.ExternalId,
            LineIDs = info.LineIDs,
            PartyID = info.PartyID,
            ShippingMethodID = info.ShippingMethodID,
            ShippingMethodName = info.ShippingMethodName,
            ShippingOptionType = info.ShippingOptionType,
            ShippingProviderID = info.ShippingProviderID,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceShippingInfo Convert(this CommerceShippingInfo info) => info == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceShippingInfo
        {
            ElectronicDeliveryEmail = info.ElectronicDeliveryEmail,
            ElectronicDeliveryEmailContent = info.ElectronicDeliveryEmailContent,
            ExternalId = info.ExternalId,
            LineIDs = info.LineIDs,
            PartyID = info.PartyID,
            ShippingMethodID = info.ShippingMethodID,
            ShippingMethodName = info.ShippingMethodName,
            ShippingOptionType = info.ShippingOptionType,
            ShippingProviderID = info.ShippingProviderID,
        };
        public static CommerceOrder Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceOrder order) => order == null ? null : new CommerceOrder {
            AccountingCustomerParty = order.AccountingCustomerParty,
            Adjustments = order.Adjustments,
            BuyerCustomerParty = order.BuyerCustomerParty,
            CartType = order.CartType,
            Created = order.Created,
            CurrencyCode = order.CurrencyCode,
            CustomerId = order.CustomerId,
            CustomerIdFacet = order.CustomerIdFacet,
            Email = order.Email,
            EmailFacet = order.EmailFacet,
            ExternalId = order.ExternalId,
            IsDirty = order.IsDirty,
            IsEmpty = order.IsEmpty,
            IsLocked = order.IsLocked,
            IsOfflineOrder = order.IsOfflineOrder,
            LastModified = order.LastModified,
            LineItemCount = order.LineItemCount,
            Lines = order.Lines.Select(l => Convert(l as Sitecore.Commerce.Engine.Connect.Entities.CommerceCartLine)).ToList<CartLine>(),
            LoyaltyCardID = order.LoyaltyCardID,
            ModifiedBy = order.ModifiedBy,
            Name = order.Name,
            OrderDate = order.OrderDate,
            OrderID = order.OrderID,
            Parties = order.Parties.Select(p => Convert(p as Sitecore.Commerce.Engine.Connect.Entities.CommerceParty)).ToList<Sitecore.Commerce.Entities.Party>(),
            Payment = order.Payment,
            Shipping = order.Shipping.Select(s => Convert(s as Sitecore.Commerce.Engine.Connect.Entities.CommerceShippingInfo)).ToList<ShippingInfo>(),
            ShopName = order.ShopName,
            SoldToAddressId = order.SoldToAddressId,
            SoldToName = order.SoldToName,
            Status = order.Status,
            StatusCode = order.StatusCode,
            Total = Convert(order.Total as Sitecore.Commerce.Engine.Connect.Entities.CommerceTotal),
            TrackingNumber = order.TrackingNumber,
            UserId = order.UserId,
            UserIdFacet = order.UserIdFacet,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceOrder Convert(this CommerceOrder order) => order == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceOrder {
            AccountingCustomerParty = order.AccountingCustomerParty,
            Adjustments = order.Adjustments,
            BuyerCustomerParty = order.BuyerCustomerParty,
            CartType = order.CartType,
            Created = order.Created,
            CurrencyCode = order.CurrencyCode,
            CustomerId = order.CustomerId,
            CustomerIdFacet = order.CustomerIdFacet,
            Email = order.Email,
            EmailFacet = order.EmailFacet,
            ExternalId = order.ExternalId,
            IsDirty = order.IsDirty,
            IsEmpty = order.IsEmpty,
            IsLocked = order.IsLocked,
            IsOfflineOrder = order.IsOfflineOrder,
            LastModified = order.LastModified,
            LineItemCount = order.LineItemCount,
            Lines = order.Lines.Select(l => Convert(l as CommerceCartLine)).ToList<CartLine>(),
            LoyaltyCardID = order.LoyaltyCardID,
            ModifiedBy = order.ModifiedBy,
            Name = order.Name,
            OrderDate = order.OrderDate,
            OrderID = order.OrderID,
            Parties = order.Parties.Select(p => Convert(p as CommerceParty)).ToList<Sitecore.Commerce.Entities.Party>(),
            Payment = order.Payment,
            Shipping = order.Shipping.Select(s => Convert(s as CommerceShippingInfo)).ToList<ShippingInfo>(),
            ShopName = order.ShopName,
            SoldToAddressId = order.SoldToAddressId,
            SoldToName = order.SoldToName,
            Status = order.Status,
            StatusCode = order.StatusCode,
            Total = Convert(order.Total as CommerceTotal),
            TrackingNumber = order.TrackingNumber,
            UserId = order.UserId,
            UserIdFacet = order.UserIdFacet,
        };
        public static FreeGiftItem Convert(this Sitecore.Commerce.Engine.Connect.Entities.FreeGiftItem item) => item == null ? null : new FreeGiftItem {
            Catalog = item.Catalog,
            ItemId = item.ItemId,
            ProductId = item.ProductId,
            VariantId = item.VariantId,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.FreeGiftItem Convert(this FreeGiftItem item) => item == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.FreeGiftItem
        {
            Catalog = item.Catalog,
            ItemId = item.ItemId,
            ProductId = item.ProductId,
            VariantId = item.VariantId,
        };
        public static FreeGiftSelection Convert(this Sitecore.Commerce.Engine.Connect.Entities.FreeGiftSelection selection) => selection == null ? null : new FreeGiftSelection {
            AddToCartAutomatically = selection.AddToCartAutomatically,
            Items = selection.Items.Select(Convert).ToList(),
            NumberOfFreeGiftsToSelect = selection.NumberOfFreeGiftsToSelect,
            PromotionId = selection.PromotionId,
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.FreeGiftSelection Convert(this FreeGiftSelection selection) => selection == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.FreeGiftSelection
        {
            AddToCartAutomatically = selection.AddToCartAutomatically,
            Items = selection.Items.Select(Convert).ToList(),
            NumberOfFreeGiftsToSelect = selection.NumberOfFreeGiftsToSelect,
            PromotionId = selection.PromotionId,
        };
        public static CommerceCart Convert(this Sitecore.Commerce.Engine.Connect.Entities.CommerceCart cart) => cart == null ? null : new CommerceCart {
            AccountingCustomerParty = cart.AccountingCustomerParty,
            Adjustments = cart.Adjustments,
            BuyerCustomerParty = cart.BuyerCustomerParty,
            CartType = cart.CartType,
            Created = cart.Created,
            CurrencyCode = cart.CurrencyCode,
            CustomerId = cart.CustomerId,
            CustomerIdFacet = cart.CustomerIdFacet,
            Email = cart.Email,
            EmailFacet = cart.EmailFacet,
            ExternalId = cart.ExternalId,
            FreeGiftSelections = cart.FreeGiftSelections.Select(Convert).ToList(),
            IsDirty = cart.IsDirty,
            IsEmpty = cart.IsEmpty,
            IsLocked = cart.IsLocked,
            LastModified = cart.LastModified,
            LineItemCount = cart.LineItemCount,
            Lines = cart.Lines.Select(l => Convert(l as Sitecore.Commerce.Engine.Connect.Entities.CommerceCartLine)).ToList<CartLine>(),
            LoyaltyCardID = cart.LoyaltyCardID,
            ModifiedBy = cart.ModifiedBy,
            Name = cart.Name,
            Parties = cart.Parties.Select(p => Convert(p as Sitecore.Commerce.Engine.Connect.Entities.CommerceParty)).ToList<Sitecore.Commerce.Entities.Party>(),
            Payment = cart.Payment,
            Shipping = cart.Shipping.Select(s => Convert(s as Sitecore.Commerce.Engine.Connect.Entities.CommerceShippingInfo)).ToList<ShippingInfo>(),
            ShopName = cart.ShopName,
            SoldToAddressId = cart.SoldToAddressId,
            OrderForms = new System.Collections.ObjectModel.ReadOnlyCollection<CommerceOrderForm>(cart.OrderForms.Select(Convert).ToList()),
            SoldToName = cart.SoldToName,
            Status = cart.Status,
            StatusCode = cart.StatusCode,
            Total = Convert(cart.Total as Sitecore.Commerce.Engine.Connect.Entities.CommerceTotal),
            TrackingNumber = cart.TrackingNumber,
            UserId = cart.UserId,
            UserIdFacet = cart.UserIdFacet
        };
        public static Sitecore.Commerce.Engine.Connect.Entities.CommerceCart Convert(this CommerceCart cart) => cart == null ? null : new Sitecore.Commerce.Engine.Connect.Entities.CommerceCart {
            AccountingCustomerParty = cart.AccountingCustomerParty,
            Adjustments = cart.Adjustments,
            BuyerCustomerParty = cart.BuyerCustomerParty,
            CartType = cart.CartType,
            Created = cart.Created,
            CurrencyCode = cart.CurrencyCode,
            CustomerId = cart.CustomerId,
            CustomerIdFacet = cart.CustomerIdFacet,
            Email = cart.Email,
            EmailFacet = cart.EmailFacet,
            ExternalId = cart.ExternalId,
            FreeGiftSelections = cart.FreeGiftSelections.Select(Convert).ToList(),
            IsDirty = cart.IsDirty,
            IsEmpty = cart.IsEmpty,
            IsLocked = cart.IsLocked,
            LastModified = cart.LastModified,
            LineItemCount = cart.LineItemCount,
            Lines = cart.Lines.Select(l => Convert(l as CommerceCartLine)).ToList<CartLine>(),
            LoyaltyCardID = cart.LoyaltyCardID,
            ModifiedBy = cart.ModifiedBy,
            Name = cart.Name,
            Parties = cart.Parties.Select(p => Convert(p as CommerceParty)).ToList<Sitecore.Commerce.Entities.Party>(),
            Payment = cart.Payment,
            Shipping = cart.Shipping?.Select(s => Convert(s as CommerceShippingInfo)).ToList<ShippingInfo>(),
            ShopName = cart.ShopName,
            SoldToAddressId = cart.SoldToAddressId,
            OrderForms = new System.Collections.ObjectModel.ReadOnlyCollection<Sitecore.Commerce.Engine.Connect.Entities.CommerceOrderForm>(cart.OrderForms.Select(Convert).ToList()),
            SoldToName = cart.SoldToName,
            Status = cart.Status,
            StatusCode = cart.StatusCode,
            Total = Convert(cart.Total as CommerceTotal),
            TrackingNumber = cart.TrackingNumber,
            UserId = cart.UserId,
            UserIdFacet = cart.UserIdFacet
        };
    }
}