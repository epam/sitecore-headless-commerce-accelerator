namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Cart
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