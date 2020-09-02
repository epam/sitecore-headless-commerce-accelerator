namespace AutoTests.HCA.Core.Common.Settings.Promotions
{
    public class HcaPromotionTestsDataSettings : BaseHcaEntityTestsDataSettings
    {
        public bool IsIncluded { get; set; }

        public HcaPromotionName Name { get; set; }

        public string DisplayCartText { get; set; }

        public string Code { get; set; }

        public HcaDiscount Discount { get; set; }

        public decimal MinimumBasketPriceForApplication { get; set; }
    }
}