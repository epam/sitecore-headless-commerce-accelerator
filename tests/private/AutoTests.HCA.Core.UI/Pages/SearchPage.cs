using AutoTests.HCA.Core.UI.ConstantsAndEnums;
using NUnit.Framework;

namespace AutoTests.HCA.Core.UI.Pages
{
    public class SearchPage : CommonPage
    {
        private static SearchPage _searchPage;

        public static SearchPage Instance => _searchPage ??= new SearchPage();

        public override string GetPath()
        {
            return PagePrefix.Search.GetPrefix();
        }

        public new void VerifyOpened()
        {
            Assert.AreEqual("Search | HCA ", GetTitleText());
        }
    }
}