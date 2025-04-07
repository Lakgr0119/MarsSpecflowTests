using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MarsSpecflowTests.Hooks;
using MarsSpecflowTests.Pages;
using MarsSpecflowTests.Utilities;
using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.Log;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;


namespace MarsSpecflowTests.Stepdefinition
{
    [Binding, Scope(Tag = "positive")]
    public class LanguagesSteps: CommonDriver
    {
        public LanguagesSteps()
        {
            driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");  // Ensure WebDriver is set
            languagesPage = new LanguagesPage(driver);  // Pass WebDriver properly
        }

        public LanguagesPage languagesPage = new LanguagesPage(driver);
        public LoginPage loginPage= new LoginPage(driver);
               

        [Given(@"User is on the login page and enters ""([^""]*)"" and ""([^""]*)""")]
        public void GivenUserIsOnTheLoginPageAndEntersAnd(string username, string password)
        {
            
            loginPage.LoginActions(username, password);
        }   


        [Given(@"User is on the Profile Page")]
        public void GivenUserIsOnTheProfilePage()
        {
           // LanguagesPage languagesPage = new LanguagesPage(driver);
            languagesPage.navigateToProfilePage();

        }

        [When(@"User clicks on the Add New button under Languages")]
        public void WhenUserClicksOnTheAddNewButtonUnderLanguages()
        {
            languagesPage.ClickAddNewButton();
        }


        [When(@"User enters ""([^""]*)"" with ""([^""]*)""")]
        public void WhenUserEntersWith(string language, string level)
        {
            IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            LanguagesPage languagesPage = new LanguagesPage(driver);
            Console.WriteLine($"✅ Entering language details: {language}, {level}");

            // Call EnterLanguageDetails() to actually add the language
            languagesPage.EnterLanguageDetails(language, level);
        }


        [When(@"The User clicks on the Add button")]
        public void WhenTheUserClicksOnTheAddButton()
        {
            // throw new PendingStepException();
            IWebDriver driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            LanguagesPage languagesPage = new LanguagesPage(driver);

            // Click on Add button
            languagesPage.ClickAddButton();
        }

        [When(@"User deletes the language ""(.*)""")]
        public void WhenUserDeletesTheLanguage(string language)
        {
            LanguagesPage languagesPage = new LanguagesPage(ScenarioContext.Current["currentDriver"] as IWebDriver);
            languagesPage.DeleteLanguage(language);
        }



        [Then(@"The user entry should be added to the profile")]
        public void ThenTheUserEntryShouldBeAddedToTheProfile()
        {
            throw new PendingStepException();
        }

        [Then(@"The language ""(.*)"" should be added to the profile")]
        public void ThenTheLanguageShouldBeAddedToTheProfile(string expectedLanguage)
        {
            var driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            var languageCell = driver.FindElements(By.XPath($"//table//td[text()='{expectedLanguage}']"));

            if (languageCell.Count > 0)
            {
                Console.WriteLine($"✅ Language '{expectedLanguage}' is present in the profile.");
            }
            else
            {
                throw new Exception($"❌ Language '{expectedLanguage}' was not found in the profile.");
            }
        }

        [Then(@"The language ""(.*)"" should be removed from the profile")]
        [Then(@"The language ""(.*)"" should be removed from the profile")]
        public void ThenTheLanguageShouldBeRemovedFromTheProfile(string language)
        {
            LanguagesPage languagesPage = new LanguagesPage(driver);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool isRemoved = wait.Until(d =>
            {
                return !languagesPage.IsLanguagePresent(language);
            });

            if (!isRemoved)
            {
                throw new Exception($"❌ Language '{language}' was still found in the profile after deletion.");
            }

            Console.WriteLine($"✅ Language '{language}' successfully removed from the profile.");
        }
      
        [When(@"User edits the language ""(.*)"" to ""(.*)"" with ""(.*)""")]
        public void WhenUserEditsTheLanguageToWith(string oldLanguage, string newLanguage, string newLevel)
        {
            languagesPage.EditLanguage(oldLanguage, newLanguage, newLevel);
        }


        // Add this code to LanguagesSteps.cs (no need to create a new file)

        


    }
}

