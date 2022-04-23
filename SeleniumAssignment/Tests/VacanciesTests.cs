using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumAssignment
{
    [TestFixture]
    internal class VacanciesTests
    {
        readonly string url = "https://cz.careers.veeam.com/vacancies";
        readonly string department = "Research & Development";
        readonly string language = "English";
        readonly int? expectedJobsNumber = 15;
        IWebDriver driver;
        DefaultWait<IWebDriver> fluentWait;
        VacanciesPage vc;

        [SetUp]
        public void StartBrowser()
        {
            // Local Selenium WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);

            fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(2);
            fluentWait.Message = "Element not found";
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(300);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            vc = new(driver, fluentWait);
        }

        [Test]
        public void CheckVacanciesCount()
        {        
            vc.SetDepartment(department);
            vc.SetLanguage(language);
            //Explicit wait for jobs to be re-filtered before counting them.
            Thread.Sleep(1000);
            int actualJobsNumber = vc.GetVacanciesCount();
            Assert.AreEqual(actualJobsNumber, expectedJobsNumber);         
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
