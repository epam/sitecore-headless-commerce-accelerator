namespace Wooli.Feature.Checkout.Models.Requests
{
    using System.Diagnostics.CodeAnalysis;
    using TypeLite;
    using System.ComponentModel.DataAnnotations;

    [ExcludeFromCodeCoverage]
    [TsClass]
    public class PromoCodeRequest
    {
        [Required]
        public string PromoCode { get; set; }
    }
}