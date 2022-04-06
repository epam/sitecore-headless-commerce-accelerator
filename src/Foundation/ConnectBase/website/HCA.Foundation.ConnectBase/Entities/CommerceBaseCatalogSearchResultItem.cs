using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.Data.Items;

namespace HCA.Foundation.ConnectBase.Entities
{
    public class CommerceBaseCatalogSearchResultItem : CommerceSearchResultItem
	{
		[IndexField("catalogentityid")]
		public string CatalogEntityId { get; set; }

		[IndexField("displayname")]
		public string DisplayName { get; set; }

        public override Item GetItem() => Context.Database.GetItem(ItemId);
    }
}