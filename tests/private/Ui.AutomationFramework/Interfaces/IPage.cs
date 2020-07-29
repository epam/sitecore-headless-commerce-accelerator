using System;

namespace Ui.AutomationFramework.Interfaces
{
    public interface IPage
    {
        void VerifyOpened();

        Uri GetUrl();
        string GetPath();
    }
}