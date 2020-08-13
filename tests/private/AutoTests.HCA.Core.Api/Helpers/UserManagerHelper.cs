using System;
using System.Linq;
using AutoTests.AutomationFramework.Shared.Models;
using AutoTests.HCA.Core.API.Models.Hca.Entities.Account.Authentication;
using AutoTests.HCA.Core.API.Services.HcaService;

namespace AutoTests.HCA.Core.API.Helpers
{
    public class UserManagerHelper
    {
        private readonly IHcaApiService _hcaApiService;

        public UserManagerHelper(UserLogin user, IHcaApiService hcaApiService)
        {
            if (user == null || hcaApiService == null)
                throw new ArgumentNullException($"{nameof(hcaApiService)} or {nameof(user)} can't be NULL.");

            _hcaApiService = hcaApiService;
            _hcaApiService.Login(new LoginRequest(user.Email, user.Password));
        }

        public void CleanCart()
        {
            var res = _hcaApiService.GetCart();

            //TODO did the request pass

            var cartIsEmpty = res?.OkResponseData?.Data?.CartLines?.Any();
            if (cartIsEmpty.HasValue && cartIsEmpty.Value)
            {
                foreach (var cartLine in res.OkResponseData.Data.CartLines)
                {
                    _hcaApiService.RemoveCartLine(cartLine.Product.ProductId);
                }
            }
        }
    }
}
