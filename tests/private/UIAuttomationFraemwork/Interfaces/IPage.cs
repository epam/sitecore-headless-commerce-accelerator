using System;

namespace UIAutomationFramework.Interfaces
{
    public interface IPage
    {
        void VerifyOpened();

        Uri GetUrl();
        string GetPath();
    }
}
