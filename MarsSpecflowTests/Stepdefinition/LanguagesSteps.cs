using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsSpecflowTests.Pages;
using MarsSpecflowTests.Utilities;
using TechTalk.SpecFlow;

namespace MarsSpecflowTests.Stepdefinition
{
    [Binding]
    public class LanguagesSteps: CommonDriver
    {
        public LanguagesPage languagesPage;

        [Given(@"User is on the Profile Page")]
        public void GivenUserIsOnTheProfilePage()
        {
            languagesPage = new LanguagesPage(driver);
            languagesPage.navigateToProfilePage();
        }

        [When(@"User clicks on the ""([^""]*)"" button under Languages")]
        public void WhenUserClicksOnTheButtonUnderLanguages(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"User enters ""([^""]*)"" with ""([^""]*)""")]
        public void WhenUserEntersWith(string hindi, string p1)
        {
            throw new PendingStepException();
        }

        [When(@"User clicks on the ""([^""]*)"" button")]
        public void WhenUserClicksOnTheButton(string add)
        {
            throw new PendingStepException();
        }

        [Then(@"The language ""([^""]*)"" should be added to the profile")]
        public void ThenTheLanguageShouldBeAddedToTheProfile(string hindi)
        {
            throw new PendingStepException();
        }

    }
}

