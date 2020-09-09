namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Cart
{
    public class PromoCodeRequest
    {
        public PromoCodeRequest()
        {
        }

        public PromoCodeRequest(string promoCode)
        {
            PromoCode = promoCode;
        }

        public string PromoCode { get; set; }
    }
}