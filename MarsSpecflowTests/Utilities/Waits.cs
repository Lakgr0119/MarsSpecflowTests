using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace MarsSpecflowTests.Utilities
{
    public class Waits
    {
        public static void waitToBeClickable(IWebDriver driver, String locatorType, String locatorValue, int seconds)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "❌ WebDriver is null! Cannot wait for element.");
            }

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

                //if (locatorType.ToLower() == "xpath")
                //var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
                //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("Username")));
                if (locatorType.ToLower() == "xpath")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(locatorValue)));
                }
                if (locatorType.ToLower() == "Id")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(locatorValue)));
                }
                if (locatorType.ToLower() == "LinkText")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.LinkText(locatorValue)));
                }
                if (locatorType.ToLower() == "CssSelector")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(locatorValue)));
                }
                //another condition

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in waitToBeClickable: {locatorValue} - {ex.Message}");
                throw;
            }
        }
        public static void waitToBeVisible(IWebDriver driver, String locatorType, String locatorValue, int seconds)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "❌ WebDriver is null! Cannot wait for element.");
            }
            // var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            try {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

                if (locatorType.ToLower() == "xpath")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locatorValue)));
                }
                else if (locatorType.ToLower() == "cssselector")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(locatorValue)));
                }
                else if (locatorType.ToLower() == "id")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(locatorValue)));
                }
                else if (locatorType.ToLower() == "linktext")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText(locatorValue)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in waitToBeVisible: {locatorValue} - {ex.Message}");
                throw;
            }
        }
    }
}

