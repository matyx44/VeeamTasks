using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace SeleniumAssignment
{
    public static class DriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeout)
        {
            if (timeout > 0)
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(driver => driver.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeout)
        {
            if (timeout > 0)
            {
                return new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(driver => driver.FindElements(by));
            }
            return driver.FindElements(by);
        }
    }
}
