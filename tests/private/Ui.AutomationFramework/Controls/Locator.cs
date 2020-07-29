using OpenQA.Selenium;
using Ui.AutomationFramework.Exceptions;

namespace UI.AutomationFramework.Controls
{
    public class Locator
    {
        private readonly By _locatorBy;

        public Locator(By by)
        {
            _locatorBy = by;
        }

        public Locator()
        {
        }

        private By Environment { get; set; }

        internal By GetLocator()
        {
            if (_locatorBy != null) return _locatorBy;
            var localLocator = Environment;
            //By localLocator = null;
            //TODO реализовать фреймворк для нескольких энвайрнтментов
            //var environment = SeleniumTestContext.GetEnvironment();
            //switch (environment)
            //{
            //    case 1:
            //        localLocator = Environment1;
            //        break;
            //    case 2:
            //        localLocator =
            //            Environment2;
            //        break;
            //    case 3:
            //        localLocator = Environment3;
            //        break;
            //}

            if (localLocator == null) throw new LocatorNotFoundException("Cannot find locator for environment ");

            return localLocator;
        }
    }
}