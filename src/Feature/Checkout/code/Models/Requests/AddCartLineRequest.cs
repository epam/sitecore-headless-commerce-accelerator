namespace Wooli.Feature.Checkout.Models.Requests
{
    using System.ComponentModel.DataAnnotations;
    using TypeLite;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [TsClass]
    public class AddCartLineRequest : CartLineRequest
    {
        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Quantity { get; set; }
    }
}