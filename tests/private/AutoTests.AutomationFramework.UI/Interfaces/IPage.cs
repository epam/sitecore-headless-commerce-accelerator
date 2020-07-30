using System;

namespace AutoTests.AutomationFramework.UI.Interfaces
{
    public interface IPage
    {
        void VerifyOpened();

        Uri GetUrl();
        string GetPath();
    }
}