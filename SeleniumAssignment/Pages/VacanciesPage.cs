using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Linq;

namespace SeleniumAssignment
{
    internal class VacanciesPage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> fluentWait;

        public VacanciesPage(IWebDriver driver, DefaultWait<IWebDriver> fluentWait)
        {
            this.driver = driver;
            this.fluentWait = fluentWait;
        }

        public void SetDepartment(string department)
        {
            IWebElement departmentsButton = FilterButtons.SingleOrDefault(x => x.Text == "All departments");
            departmentsButton.Click();
            //Fluent wait
            IWebElement values = fluentWait.Until(x => departmentsButton.FindElement(By.XPath("//following-sibling::div")));

            IWebElement research = values.FindElement(By.XPath($"//a[contains(text(),'{department}')]"));
            research.Click();
        }

        public void SetLanguage(string language)
        {
            IWebElement languagesButton = FilterButtons.SingleOrDefault(x => x.Text == "All languages");
            languagesButton.Click();
            //Fluent wait
            IWebElement values = fluentWait.Until(x => languagesButton.FindElement(By.XPath("//following-sibling::div")));

            IWebElement english = values.FindElement(By.XPath($"//div[./label[contains(text(),'{language}')]]"));
            english.Click();
        }

        public int GetVacanciesCount()
        {
            ReadOnlyCollection<IWebElement> vacancies = VacanciesTable.FindElements(By.XPath("./div/*"));
            var count = vacancies.Count();
            return count;
        }

        //Using explicit wait in DriverExtensions
        private ReadOnlyCollection<IWebElement> FilterButtons => driver.FindElements(By.Id("sl"), 2);
        private IWebElement VacanciesTable => driver.FindElement(By.ClassName("col-lg-8"), 2);
    }
}
