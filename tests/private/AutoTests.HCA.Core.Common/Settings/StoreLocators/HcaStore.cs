using AutoTests.HCA.Core.Common.Entities.Models;

namespace AutoTests.HCA.Core.Common.Settings.StoreLocators
{
    public class HcaStore : BaseHcaEntityTestsDataSettings
    {
        public string Title { get; set; }

        public Coordinates Coordinates { get; set; }
    }
}