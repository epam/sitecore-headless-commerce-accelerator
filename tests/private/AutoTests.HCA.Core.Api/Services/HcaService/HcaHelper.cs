using System.Linq;

namespace AutoTests.HCA.Core.API.Services.HcaService
{
    public class HcaHelper
    {
        private readonly IHcaApiService _hcaApiService;

        public HcaHelper(IHcaApiService service)
        {
            _hcaApiService = service;
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
