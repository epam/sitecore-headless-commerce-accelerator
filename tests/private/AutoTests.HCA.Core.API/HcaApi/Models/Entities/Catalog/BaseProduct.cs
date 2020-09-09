using System.Collections.Generic;

namespace AutoTests.HCA.Core.API.HcaApi.Models.Entities.Catalog
{
    public class BaseProduct
    {
        public string ProductId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public IList<string> Tags { get; set; }

        public IList<string> ImageUrls { get; set; }

        public string CurrencySymbol { get; set; }

        public decimal? ListPrice { get; set; }

        public decimal? AdjustedPrice { get; set; }

        public string StockStatusName { get; set; }

        public decimal? CustomerAverageRating { get; set; }
    }
}