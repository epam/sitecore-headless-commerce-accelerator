using Ui.AutomationFramework.Utils;
using Ui.HCA.Pages.ConstantsAndEnums.Common;

namespace Ui.HCA.Pages.ConstantsAndEnums.Product
{
    public enum ProductStatus
    {
        [Element("")]
        NotSet,

        [Element("IN STOCK")]
        InStock,

        [Element("OUT OF STOCK")]
        OutOfStock
    }

    public static class ProductStatusExtensions
    {
        public static ProductStatus GetStatus(string text)
        {
            if (text == ProductStatus.InStock.GetAttribute<ElementAttribute>().Name) return ProductStatus.InStock;
            if (text == ProductStatus.OutOfStock.GetAttribute<ElementAttribute>().Name) return ProductStatus.OutOfStock;

            return ProductStatus.NotSet;
        }
    }
}
