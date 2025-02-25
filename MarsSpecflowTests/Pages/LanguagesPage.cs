using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MarsSpecflowTests.Pages
{
    public class LanguagesPage
    {
        public IWebDriver driver;
        public LanguagesPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void navigateToProfilePage()
        {
            IWebElement profileElement = driver.FindElement(By.CssSelector(".item.ui.dropdown.link"));
            string elementText = profileElement.Text;
            Assert.That(elementText, Is.EqualTo("Hi user"));


        }

    }
}

