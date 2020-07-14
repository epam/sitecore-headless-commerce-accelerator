namespace HCA.Api.Core.Models.Hca.Entities.Cart
{
    public class AddCartLinesRequest
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public string VariantId { get; set; }
    }
}