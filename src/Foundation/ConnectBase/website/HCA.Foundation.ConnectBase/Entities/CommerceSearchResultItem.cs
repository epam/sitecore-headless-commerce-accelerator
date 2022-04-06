using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace HCA.Foundation.ConnectBase.Entities
{
    public class CommerceSearchResultItem : SearchResultItem
	{
		[IndexField("commercesearchitemtype")]
		public string CommerceSearchItemType { get; set; }
	}
}