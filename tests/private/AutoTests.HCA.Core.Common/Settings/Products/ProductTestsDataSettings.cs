namespace AutoTests.HCA.Core.Common.Settings.Products
{
    public class ProductTestsDataSettings : BaseHcaEntityTestsDataSettings
    {
        public string ProductId { get; set; }

        public string VariantId { get; set; }

        public HcaProductStatus StockStatus { get; set; }

        public string ProductName { get; set; }

        public string ProductBrand { get; set; }

        public string ProductCategoryId { get; set; }

        public bool DefaultVariant { get; set; }
    }
}