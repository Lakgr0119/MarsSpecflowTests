using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsSpecflowTests.Utilities;
using OpenQA.Selenium;

namespace MarsSpecflowTests.Pages
{
    public class LoginPage
    {
        public IWebDriver driver;
        IWebElement emailElement => driver.FindElement(By.XPath("//input[@placeholder='Email address']"));
        IWebElement passWordElement => driver.FindElement(By.XPath("//input[@placeholder='Password']"));
        IWebElement checkBoxElement => driver.FindElement(By.XPath("//input[@type='checkbox']"));
        IWebElement loginButtonElement => driver.FindElement(By.XPath("//button[@class='fluid ui teal button']"));

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void loginActions()
        {
            driver.Navigate().GoToUrl("http://localhost:5000/");
            driver.Manage().Window.Maximize();
            Waits.waitToBeClickable(driver, "Link Text", "Sign In", 10);
            driver.FindElement(By.LinkText("Sign In")).Click();

        }

        public void enterCredentials(string username, string password)
        {
            Waits.waitToBeVisible(driver, "Xpath", "//input[@placeholder='Email address']", 10);
            emailElement.SendKeys(username);
            emailElement.Clear();
            Waits.waitToBeVisible(driver, "Xpath", "//input[@placeholder='password']", 10);
            passWordElement.SendKeys(password);
            passWordElement.Clear();
            Waits.waitToBeClickable(driver, "Xpath", "//input[@type='checkbox']", 10);
            checkBoxElement.Click();
            Waits.waitToBeClickable(driver, "Xpath", "//button[@class='fluid ui teal button']", 10);
            loginButtonElement.Click();

        }

    }
}

