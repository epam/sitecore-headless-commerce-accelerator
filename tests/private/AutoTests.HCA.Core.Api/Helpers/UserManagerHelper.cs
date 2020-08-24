using System;
using System.Linq;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using AutoTests.HCA.Core.API.Services.HcaService;
using AutoTests.HCA.Core.Common.Settings.Users;

namespace AutoTests.HCA.Core.API.Helpers
{
    public class UserManagerHelper
    {
        private readonly IHcaApiService _hcaApiService;

        public UserManagerHelper(HcaUser user, IHcaApiService hcaApiService)
        {
            if (user == null || hcaApiService == null)
                throw new ArgumentNullException($"{nameof(hcaApiService)} or {nameof(user)} can't be NULL.");

            _hcaApiService = hcaApiService;

            if (user.Role == HcaUserRole.User)
                _hcaApiService.Login(new LoginRequest(user.Credentials.Email, user.Credentials.Password));
        }

        public void CleanCart()
        {
            var res = _hcaApiService.GetCart();

            //TODO did the request pass

            var cartIsNotEmpty = res?.OkResponseData?.Data?.CartLines?.Any();
            if (cartIsNotEmpty.HasValue && cartIsNotEmpty.Value)
                foreach (var cartLine in res.OkResponseData.Data.CartLines)
                    _hcaApiService.RemoveCartLine(cartLine.Product.ProductId, cartLine.Variant.VariantId);
        }

        public void CleanAddresses()
        {
            var res = _hcaApiService.GetAddresses();

            //TODO did the request pass

            var addressesListIsNotEmpty = res?.OkResponseData?.Data?.Any();
            if (addressesListIsNotEmpty.HasValue && addressesListIsNotEmpty.Value)
                foreach (var address in res.OkResponseData.Data)
                    _hcaApiService.RemoveAddress(address.ExternalId);
        }
    }
}