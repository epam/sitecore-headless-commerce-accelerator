namespace Wooli.Feature.Checkout.Models.Requests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using TypeLite;

    [ExcludeFromCodeCoverage]
    [TsClass]
    public class CartLineRequest
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string VariantId { get; set; }
    }
}