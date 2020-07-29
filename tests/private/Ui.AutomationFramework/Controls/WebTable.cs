using System.Collections.Generic;
using OpenQA.Selenium;
using UI.AutomationFramework.Controls;
using Ui.AutomationFramework.Core;
using Ui.AutomationFramework.Interfaces;
using static Ui.AutomationFramework.Core.TestLogger;
using LogLevel = NLog.LogLevel;

namespace Ui.AutomationFramework.Controls
{
    public class WebTable : BaseWebUiElement
    {
        private readonly int _rowsStart;

        internal WebTable(string elementName, By locator, BaseWebControl container = null,
            bool firstRowIsHeader = true, By footerElementLocator = null) : base(elementName,
            new Locator(locator), container)
        {
            if (firstRowIsHeader) _rowsStart = 1;

            if (footerElementLocator != null) FooterElement = new WebElement("Footer", footerElementLocator, this);
        }

        public WebTable(string elementName, Locator locator, Frame frame, bool firstRowIsHeader = true,
            By footerElementLocator = null) : base(
            elementName, locator, frame)
        {
            if (firstRowIsHeader) _rowsStart = 1;

            if (footerElementLocator != null) FooterElement = new WebElement("Footer", footerElementLocator, frame);
        }

        internal WebElement FooterElement { get; }

        public int GetRowsCount()
        {
            Log(LogLevel.Debug, $"Getting rows count of {ElementName}");
            var footerOffset = IsFooterExist() ? 1 : 0;
            return GetChildElementsCount(ByCustom.XPath("./tbody/tr")) - _rowsStart - footerOffset;
        }

        protected bool IsFooterExist()
        {
            Log(LogLevel.Debug, $"Getting if footer exist of {ElementName}");
            return FooterElement != null && FooterElement.IsPresent();
        }

        internal List<T> GetObjectsListFromTable<T>(int offset = 0) where T : IGetFromRowElement, new()
        {
            Log(LogLevel.Debug, $"Getting objects from table {ElementName}");
            var list = new List<T>();
            var rowsList = GetRows();
            for (var i = 0; i < rowsList.Count - offset; i++)
            {
                var e = new T();
                e.TransformElement(rowsList[i]);
                list.Add(e);
            }

            return list;
        }

        protected RowElement GetRowByCellValueInColumn(string value, int columnIndex)
        {
            Log(LogLevel.Debug, $"Getting row by cell value '{value}' in column {columnIndex} of {ElementName} ");
            return GetRows().Find(rowItem => rowItem.GetValueByIndex(columnIndex) == value);
        }

        protected internal List<RowElement> GetRows()
        {
            Log(LogLevel.Debug, $"Getting all rows of {ElementName}");
            var list = new List<RowElement>();
            var rowsCount = GetRowsCount();
            for (var i = _rowsStart; i < rowsCount + _rowsStart; i++)
            {
                var el = new WebElement("Row item", ByCustom.XPath($".//tr[{i + 1}]"), this);
                list.Add(new RowElement(el));
            }

            return list;
        }
    }

    public class RowElement
    {
        private readonly WebElement _rowWebElement;

        public RowElement(WebElement element)
        {
            _rowWebElement = element;
        }

        public WebElement GetBaseWebUiElement()
        {
            return _rowWebElement;
        }

        public string GetValueByIndex(int columnIndex)
        {
            var cellElement = new WebElement("Cell element", ByCustom.XPath($"./td[{columnIndex}]"), _rowWebElement);
            return cellElement.GetTrimmedText();
        }
    }
}