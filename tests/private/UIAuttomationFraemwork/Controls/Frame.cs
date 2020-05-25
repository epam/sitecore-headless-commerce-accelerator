using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace UIAutomationFramework.Controls
{
    public class Frame : BaseWebControl
    {
        private string Name;

        public Frame(string elementName, params By[] locators)
        {
            Name = elementName;
            FrameLocators = locators.ToList();
        }

        public Frame(Frame frame)
        {
            FrameLocators = new List<By>();

            frame.FrameLocators.ForEach(x =>
                FrameLocators.Add(x));
        }

        public List<By> FrameLocators { get; }

        public Frame WithChild(string name, By locator)
        {
            FrameLocators.Add(locator);
            return this;
        }
    }
}
