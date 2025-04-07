using MarsSpecflowTests.Pages;
using MarsSpecflowTests.Utilities;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace MarsSpecflowTests.Stepdefinition
{
    [Binding, Scope(Tag = "negative")]
    
    public class LanguagesNegativeSteps : CommonDriver
    {
        private LanguagesPage languagesPage;

        public LanguagesNegativeSteps()
        {
            driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            languagesPage = new LanguagesPage(driver);
        }

        [Given(@"User is on the Profile Page")]
        public void GivenUserIsOnTheProfilePage()
        {
            languagesPage.navigateToProfilePage();
        }

        [When(@"User clicks on the Add New button under Languages")]
        public void WhenUserClicksOnTheAddNewButtonUnderLanguages()
        {
            languagesPage.ClickAddNewButton();
        }

 
        [When(@"User enters ""(.*)"" with ""(.*)""")]
        public void WhenUserEntersWith(string language, string level)
        {
            Console.WriteLine($"✅ Entering language details: {language}, {level}");
            languagesPage.EnterLanguageDetails(language, level);
        }




        [Then(@"An appropriate error message should be displayed for ""(.*)""")]
        public void ThenAnAppropriateErrorMessageShouldBeDisplayedFor(string errorType)
        {
            string actualMessage = languagesPage.GetErrorMessage();
            string expectedMessage = errorType switch
            {
                "BlankLanguage" => "Please enter language and level",
                "BlankLevel" => "Please enter language and level",
                "DuplicateLanguage" => "This language is already exist in your language list.",
                "MaxLanguagesReached" => "You can add only 4 languages.",
                "InvalidLevelSelection" => "Please enter language and level",
                _ => throw new ArgumentException("Unknown error type: " + errorType)
            };

            Console.WriteLine($"🔍 Expected: {expectedMessage} | Actual: {actualMessage}");
            Assert.AreEqual(expectedMessage, actualMessage, "❌ ERROR: Incorrect validation message.");
        }
        [When(@"User adds the following 4 languages:")]
        public void WhenUserAddsTheFollowing4Languages(Table table)
        {
            foreach (var row in table.Rows)
            {
                string language = row["language"];
                string level = row["level"];

                languagesPage.ClickAddNewButton();
                languagesPage.EnterLanguageDetails(language, level);
            }
        }

        [When(@"User attempts to add another language ""(.*)"" with ""(.*)""")]
        public void WhenUserAttemptsToAddAnotherLanguage(string language, string level)
        {
            languagesPage.ClickAddNewButton();
            languagesPage.EnterLanguageDetails(language, level);
        }


        [Then(@"The user should not be able to add a fifth language")]
        public void ThenUserShouldNotBeAbleToAddAFifthLanguage()
        {
            IWebElement? addNewButton = null;
            try
            {
                addNewButton = driver.FindElement(By.XPath("//div[@class='ui teal button ' and text()='Add New']"));
                bool isVisible = addNewButton.Displayed && addNewButton.Enabled;
                Assert.IsFalse(isVisible, "❌ 'Add New' button is still visible and enabled, user is able to add more than 4 languages.");
                Console.WriteLine("✅ 'Add New' button is hidden or disabled – user cannot add more than 4 languages.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("✅ 'Add New' button not found – user cannot add more than 4 languages.");
                Assert.Pass();
            }

        }

    }
}
