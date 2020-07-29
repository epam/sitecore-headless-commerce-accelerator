using NUnit.Framework;
using System.Linq;
using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;
using UI.AutomationFramework.Controls;

namespace Ui.HCA.Pages.CommonElements
{
    public class ProductsFilterSection
    {
        private static ProductsFilterSection _filterControl;

        private static readonly WebElement _productGridFilter = new WebElement("Product Grid Filter", ByCustom.ClassName("productGridFilter"));
        private static readonly WebElement _filterOptions = new WebElement("Filter Options", ByCustom.ClassStartsWith("filter_options"));
        private static readonly WebElement _filterLabel = new WebElement("Filter Label", ByCustom.ClassName("filter-label"));
        private static readonly WebLink _hideOrShowLink = new WebLink("Hide/Show Filter Link", ByCustom.ClassName("view-all"));

        public static ProductsFilterSection Instance => _filterControl ??= new ProductsFilterSection();

        public bool FilterSectionIsPresent() => _productGridFilter.IsPresent();

        public void SelectFilterOptionByName(string nameOption) => FindFilterOptionByName(nameOption).Check();

        public void DeselectFilter(string nameOption) => FindFilterOptionByName(nameOption).JsClick();

        public void ClickFilterDisableButton() => _filterLabel.JsClick();

        public void ClickHideOrShowLink() => _hideOrShowLink.JsClick();

        public void VerifyHideLink(string linkText)
        {
            _hideOrShowLink.VerifyText(linkText);
            _filterOptions.IsPresent();
        }

        public void VerifyShowLink(string linkText)
        {
            _hideOrShowLink.VerifyText(linkText);
            _filterOptions.VerifyNotPresent();
        }

        public void VerifyFilterOptionIsChecked(string nameOption)
        {
            var optElement = FindFilterOptionByName(nameOption);
            _filterOptions.WaitForPresent();
            optElement.IsPresent();
            optElement.IsChecked();
            _filterLabel.IsPresent();
            _filterLabel.VerifyTextIgnoreCase(nameOption);
            Assert.AreEqual(1, GetFilterOptionsCount(), "If you select a filter option, all other options should be hidden");
        }

        public void VerifyFilterOptionIsUnchecked(string nameOption)
        {
            FindFilterOptionByName(nameOption).VerifyUnchecked();
            _filterLabel.IsNotPresent();
        }

        public int GetFilterOptionsCount() => _filterOptions.GetChildElementsCount(ByCustom.TagName("li"));

        public int GetProductsCountFromOption(string nameOptions)
        {
            var text = FindFilterOptionByName(nameOptions).GetParent().GetChildElements(ByCustom.TagName("span"))
                .First().Text;
            return int.Parse(text.Substring(1, text.Length - 2));
        }

        private WebCheckBox FindFilterOptionByName(string nameOption) =>
            new WebCheckBox($"Filter option '{nameOption}'",
                ByCustom.XPath($".//label[@title = '{nameOption}']"), _filterOptions);
    }
}
