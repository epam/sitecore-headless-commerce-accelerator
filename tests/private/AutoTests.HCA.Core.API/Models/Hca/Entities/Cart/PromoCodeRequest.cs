namespace AutoTests.HCA.Core.API.Models.Hca.Entities.Cart
{
    public class PromoCodeRequest
    {
        public string PromoCode { get; set; }

        public PromoCodeRequest() { }

        public PromoCodeRequest(string promoCode)
        {
            PromoCode = promoCode;
        }
    }
}