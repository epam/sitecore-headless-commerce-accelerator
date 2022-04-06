using Sitecore.ContentSearch;

namespace HCA.Foundation.ConnectBase.Entities
{
    public class CommerceSellableItemSearchResultItem : CommerceBaseCatalogSearchResultItem
	{
		[IndexField("productid")]
		public string ProductId { get; set; }

		[IndexField("brand")]
		public string Brand { get; set; }

		[IndexField("excludefromwebsitesearchresults")]
		public bool ExcludeFromWebsiteSearchResults { get; set; }

		[IndexField("manufacturer")]
		public string Manufacturer { get; set; }

		[IndexField("typeofgood")]
		public string TypeOfGood { get; set; }
	}
}