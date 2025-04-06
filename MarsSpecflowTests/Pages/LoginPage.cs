using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsSpecflowTests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsSpecflowTests.Pages
{
    public class LoginPage
    {
        public IWebDriver driver;
        public IWebElement emailElement => driver.FindElement(By.XPath("//input[@placeholder='Email address']"));
        public IWebElement passWordElement => driver.FindElement(By.XPath("//input[@placeholder='Password']"));
        public  IWebElement checkBoxElement => driver.FindElement(By.XPath("//div//input[@type='checkbox']"));
        public  IWebElement loginButtonElement => driver.FindElement(By.CssSelector("button.fluid.ui.teal.button"));

        public LoginPage(IWebDriver driver)
        { 
            this.driver = driver;
        }
        public void LoginActions(String username, String password)
        {
             
       driver.Navigate().GoToUrl("http://localhost:5000/Home");
       Waits.waitToBeClickable(driver, "Link Text", "Sign In", 5);
       driver.FindElement(By.LinkText("Sign In")).Click();
       Waits.waitToBeVisible(driver, "Xpath", "//input[@placeholder='Email address']", 30);
       emailElement.SendKeys(username);
       Waits.waitToBeVisible(driver, "Xpath", "//input[@placeholder='Password']", 30);
       passWordElement.SendKeys(password);
       //Waits.waitToBeClickable(driver, "Xpath", "//div//input[@type='checkbox']", 60);
       checkBoxElement.Click();
       Waits.waitToBeClickable(driver, "CssSelector", "button.fluid.ui.teal.button", 30);
       loginButtonElement.Click();
       WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
       wait.Until(driver => driver.Url.Contains("http://localhost:5000/Account/Profile"));
            
            }
        }

    }

