using System.Collections.Generic;
using MarsSpecflowTests.Pages;
using MarsSpecflowTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace MarsSpecflowTests.Stepdefinition
{
    [Binding]

    public class SkillsSteps : CommonDriver
    {
        private SkillsPage skillsPage;

        public SkillsSteps()
        {
            driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            skillsPage = new SkillsPage(driver);
        }

        [When(@"User navigates to skills tab")]
       
        public void WhenUserNavigatesToSkillsTab()
        {
            skillsPage.NavigateToSkillsTab();
        }


        [When(@"User adds a new skill ""(.*)"" with level ""(.*)""")]
        [When(@"User adds a new skill ""(.*)"" with level ""(.*)""")]
        public void WhenUserAddsNewSkill(string skillName, string skillLevel)
        {
            skillsPage.AddSkill(skillName, skillLevel);
        }


        [Then(@"The skill ""(.*)"" with level ""(.*)"" should be visible in the Skills table")]
        public void ThenTheSkillWithLevelShouldBeVisible(string skill, string level)
        {
            Assert.IsTrue(skillsPage.IsSkillPresent(skill, level), $"Skill '{skill}' with level '{level}' was not found.");
        }

        [When(@"User edits the skill ""(.*)"" to ""(.*)"" with level ""(.*)""")]
        public void WhenUserEditsTheSkill(string oldSkill, string newSkill, string newLevel)
        {
            skillsPage.EditSkill(oldSkill, newSkill, newLevel);
        }

        [When(@"User deletes the skill ""(.*)""")]
        public void WhenUserDeletesTheSkill(string skill)
        {
            skillsPage.DeleteSkill(skill);
        }

        [Then(@"The skill ""(.*)"" should not be present in the Skills table")]
        public void ThenTheSkillShouldNotBePresent(string skill)
        {
            Assert.IsFalse(skillsPage.IsSkillPresent(skill), $"Skill '{skill}' should not be present.");
        }

        [When(@"User attempts to add skill ""(.*)"" with level ""(.*)""")]
        public void WhenUserAttemptsToAddInvalidSkill(string skill, string level)
        {
            skillsPage.AddSkill(skill, level);
        }

        [Then(@"The correct skill error message ""(.*)"" should be displayed")]

        [Scope(Tag = "negative")]
        public void ThenCorrectSkillErrorMessageShouldBeDisplayed(string errorType)

        {
            string expected = errorType switch
            {
                "BlankSkill" => "Please enter skill and experience level",
                "BlankSkillLevel" => "Please enter skill and experience level",
            "DuplicateSkill" => "This skill is already exist in your skill list.",
                "InvalidSkillLevel" => "Please enter skill and experience level",
                _ => throw new ArgumentException("Unknown error type")
            };

            string actual = skillsPage.GetErrorMessage();
            Console.WriteLine($"Expected: {expected} | Actual: {actual}");
            Assert.AreEqual(expected, actual);
        }
    }
}
