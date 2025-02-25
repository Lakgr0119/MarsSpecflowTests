using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsSpecflowTests.Pages;
using MarsSpecflowTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace MarsSpecflowTests.Stepdefinition
{
    [Binding]
    public class LoginSteps : CommonDriver
    {
        public LoginPage loginPage;

       [Given(@"User is on the login page and registered")]
        public void GivenUserIsOnTheLoginPageAndRegistered()
        {
            driver = new ChromeDriver();
            LoginPage loginPage = new LoginPage(driver); 
            loginPage.loginActions();
        }

        [When(@"User enters (.*) and (.*)")]
        public void WhenUserEntersUsernameAndPassword(string username, string password)
        {
            loginPage.enterCredentials(username, password);

        }
        [Then(@"User should be navigated to the Home Page")]
        public void ThenUserShouldBeNavigatedToTheHomePage()
        {
            Thread.Sleep(5000);
            Assert.That(driver.Url, Is.EqualTo("http://localhost:5000/Account/Profile"), "User reached the exact Profile page.");

        }
    }
}

