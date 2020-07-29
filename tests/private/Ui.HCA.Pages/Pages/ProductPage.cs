using NUnit.Framework;
using System.Linq;
using Ui.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;
using Ui.HCA.Pages.ConstantsAndEnums;
using Ui.HCA.Pages.ConstantsAndEnums.Product;
using UI.AutomationFramework.Controls;

namespace Ui.HCA.Pages.Pages
{
    public class ProductPage : CommonPage
    {
        private static ProductPage _productPage;

        private readonly WebButton _addToCartButton =
            new WebButton("Add to cart", ByCustom.XPath("//button[@title = 'Add to Cart']"));

        private readonly WebLabel _brand = new WebLabel("Product Brand", ByCustom.ClassName("product-brand"));
        private readonly WebElement _title = new WebElement("Product Title", ByCustom.ClassName("product-title"));
        private readonly WebLink _printButton = new WebLink("Print button", ByCustom.ClassName("product-print"));
        private readonly WebElement _status = new WebElement("In Stock Status", ByCustom.ClassName("product-stock-status"));
        private readonly WebElement _rating = new WebElement("Stars Quantity", ByCustom.ClassName("star-rating"));
        private readonly WebLink _writeReviewLink = new WebLink("Review Write Link", ByCustom.ClassName("review-write"));
        private readonly WebLink _readReviewLink = new WebLink("Review Read Link", ByCustom.ClassName("review-read"));
        private readonly WebElement _price = new WebElement("Price", ByCustom.ClassName("price-value"));
        private readonly WebElement _descriptionText = new WebElement("Product Description", ByCustom.ClassName("description-text"));
        private readonly WebElement _descriptionRating = new WebElement("Description Rating", ByCustom.ClassName("description-rating"));
        private readonly WebElement _descriptionFeatures = new WebElement("Description Features", ByCustom.ClassName("description-features"));
        private readonly WebElement _colorsList = new WebElement("Product colors", ByCustom.ClassName("colors-list"));


        public static ProductPage Instance => _productPage ??= new ProductPage();

        public string BrandText => _brand.GetText();
        public string TitleText => _title.GetText();
        public bool AddToCartButtonIsDisabled => !_addToCartButton.IsEnabled();
        public bool PrintButtonIsClickable() => _printButton.IsClickable();
        public bool WriteReviewIsClickable() => _writeReviewLink.IsClickable();
        public bool ReadReviewIsClickable() => _readReviewLink.IsClickable();
        public ProductStatus Status => ProductStatusExtensions.GetStatus(_status.GetText());
        public decimal Price => decimal.Parse(string.Join("", _price.GetText().Where(x => char.IsDigit(x) || x == '.')));
        public int StarsQuantity => _rating.GetChildElementsCount(ByCustom.ClassName("filled"));
        public string DescriptionText => _descriptionText.GetChildElement(ByCustom.TagName("p")).Text;
        public bool AreColorsPresented => _colorsList.IsPresent();

        public override string GetPath() =>
            PagePrefix.Product.GetPrefix();

        public override void WaitForOpened() =>
            Browser.WaitForUrlContains("product/");

        public void AddToCartButtonClick()
        {
            _addToCartButton.WaitForPresent();
            _addToCartButton.MouseOver();
            _addToCartButton.Click();
            _addToCartButton.WaitForClickable();
        }

        public void ChooseColor(int number) =>
            _colorsList.GetChildElements(ByCustom.ClassName("colors-listitem")).ElementAt(number).Click();

        public void VerifyDescriptionTextSection()
        {
            _descriptionText.IsPresent();
            Assert.AreEqual("PRODUCT DESCRIPTION", _descriptionText.GetChildElement(ByCustom.TagName("h2")).Text);
            Assert.IsFalse(string.IsNullOrEmpty(DescriptionText));
        }

        public void VerifyDescriptionRatingSection()
        {
            _descriptionRating.IsPresent();
            Assert.AreEqual("RATING", _descriptionRating.GetChildElement(ByCustom.TagName("h2")).Text);
        }

        public void VerifyDescriptionFeaturesSection()
        {
            _descriptionFeatures.IsPresent();
            Assert.AreEqual("FEATURES", _descriptionFeatures.GetChildElement(ByCustom.TagName("h2")).Text);
            Assert.IsTrue(_descriptionFeatures.GetChildElement(ByCustom.ClassName("feature-list")).Displayed,
                "Features list is not presented");
        }
    }
}