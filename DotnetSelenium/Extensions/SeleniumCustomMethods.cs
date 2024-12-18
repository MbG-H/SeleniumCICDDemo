using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotnetSelenium.Extensions
{
    public static class SeleniumCustomMethods
    {
        public static void ClickElement(this IWebElement locator)
        {
            locator.Click();
        }

        public static void EnterText(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }
        public static void SubmitBtnElement(this IWebElement locator)
        {
            locator.Submit();
        }
        public static void SelectDropDownByText(this IWebElement locator, string text)
        {
            var selectElement = new SelectElement(locator);
            selectElement.SelectByText(text);
        }
        public static void SelectDropDownByIndex(this IWebElement locator, int index)
        {
            var selectElement = new SelectElement(locator);
            selectElement.SelectByIndex(index);
        }
        public static void SelectDropDownByValue(this IWebElement locator, string value)
        {
            var selectElement = new SelectElement(locator);
            selectElement.SelectByValue(value);
        }
        public static void MultiSelectElements(this IWebElement locator, string[] values)
        {
            var multiSelect = new SelectElement(locator);
            foreach (var value in values)
            {
                multiSelect.SelectByValue(value);
            }

        }
        public static List<string> GetAllSelectedLists(this IWebElement locator)
        {
            List<string> options = new List<string>();
            SelectElement multiSelect = new SelectElement(locator);

            IList<IWebElement> selectedOption = multiSelect.AllSelectedOptions;
            foreach (IWebElement option in selectedOption)
            {
                options.Add(option.Text);
            }

            return options;

        }
    }
}
