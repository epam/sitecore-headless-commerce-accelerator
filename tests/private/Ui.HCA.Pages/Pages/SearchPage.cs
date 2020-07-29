using NUnit.Framework;
using Ui.HCA.Pages.ConstantsAndEnums;

namespace Ui.HCA.Pages.Pages
{
    public class SearchPage : CommonPage
    {
        private static SearchPage _searchPage;

        public static SearchPage Instance => _searchPage ??= new SearchPage();

        public override string GetPath() =>
            PagePrefix.Search.GetPrefix();

        public new void VerifyOpened() =>
            Assert.AreEqual("Search | HCA ", GetTitleText());
    }
}
