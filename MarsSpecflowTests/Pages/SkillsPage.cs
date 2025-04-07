using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using SeleniumExtras.WaitHelpers;

namespace MarsSpecflowTests.Pages
{
    public class SkillsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SkillsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToSkillsTab()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                // Click the Skills tab
                var skillsTab = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[contains(@class, 'item') and text()='Skills']")));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", skillsTab);
                skillsTab.Click();
                Console.WriteLine("✅ Clicked Skills tab.");

                // 🔄 Wait for overlay if any
                By overlay = By.XPath("//div[contains(@class, 'ui page modals dimmer') and contains(@class, 'visible active')]");
                if (driver.FindElements(overlay).Count > 0)
                {
                    Console.WriteLine("⚠️ Detected overlay, waiting to disappear...");
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(overlay));
                }

                // ✅ Wait for Add New button in Skills section
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h3[text()='Skills']/following::table[1]//div[contains(@class, 'ui teal button')]")));
                Console.WriteLine("✅ Skills section loaded successfully.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("❌ ERROR: Unable to navigate to Skills tab - Timed out after 10 seconds");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: Unexpected issue navigating to Skills tab - {ex.Message}");
                throw;
            }
        }


        public bool IsSkillPresent(string skill, string level = null)
        {
            var rows = driver.FindElements(By.XPath("//table[@class='ui fixed table']//tbody/tr"));
            return rows.Any(row =>
            {
                var cells = row.FindElements(By.TagName("td"));
                return cells.Count >= 2 &&
                       cells[0].Text.Trim().Equals(skill, StringComparison.OrdinalIgnoreCase) &&
                       (level == null || cells[1].Text.Trim().Equals(level, StringComparison.OrdinalIgnoreCase));
            });
        }

        public void EditSkill(string oldSkill, string newSkill, string newLevel)
        {
            var row = driver.FindElements(By.XPath("//table[@class='ui fixed table']//tbody/tr"))
                            .FirstOrDefault(r => r.Text.Contains(oldSkill));
            if (row == null) return;

            row.FindElement(By.XPath(".//i[@class='outline write icon']")).Click();
            Thread.Sleep(500);

            var input = driver.FindElement(By.XPath("//input[@placeholder='Add Skill']"));
            input.Clear();
            input.SendKeys(newSkill);

            var dropdown = driver.FindElement(By.XPath("//select[@name='level']"));
            new SelectElement(dropdown).SelectByText(newLevel);

            var updateButton = driver.FindElement(By.XPath("//input[@value='Update']"));
            updateButton.Click();
        }

        public void DeleteSkill(string skill)
        {
            var row = driver.FindElements(By.XPath("//table[@class='ui fixed table']//tbody/tr"))
                            .FirstOrDefault(r => r.Text.Contains(skill));
            if (row == null) return;

            row.FindElement(By.XPath(".//i[@class='remove icon']")).Click();
        }

        public string GetErrorMessage()
        {
            try
            {
                var toast = wait.Until(d =>
                {
                    var elements = d.FindElements(By.CssSelector(".ns-box-inner"));
                    return elements.FirstOrDefault(e => e.Displayed && !string.IsNullOrWhiteSpace(e.Text));
                });

                return toast?.Text.Trim() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        public void ClickAddNewButton()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                By addNewBtnLocator = By.XPath("//h3[text()='Skills']/following::table[1]//div[contains(@class, 'ui teal button') and text()='Add New']");
                IWebElement addNewBtn = wait.Until(ExpectedConditions.ElementToBeClickable(addNewBtnLocator));

                js.ExecuteScript("arguments[0].scrollIntoView(true);", addNewBtn);
                Thread.Sleep(300);
                addNewBtn.Click();
                Console.WriteLine("✅ 'Add New' button in Skills section clicked.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR clicking Add New in Skills section: " + ex.Message);
            }
        }
        public void AddSkill(string skill, string level)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                // ✅ Handle any modal overlays blocking interactions
                By modalOverlay = By.XPath("//div[contains(@class, 'ui page modals dimmer') and contains(@class, 'visible active')]");
                if (driver.FindElements(modalOverlay).Count > 0)
                {
                    Console.WriteLine("⚠️ Modal overlay detected. Attempting to remove...");
                    js.ExecuteScript("document.querySelector('.ui.page.modals.dimmer.visible.active')?.remove();");
                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(modalOverlay));
                    Console.WriteLine("✅ Overlay removed.");
                }

                // ✅ Click Add New
                var addNewButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='ui teal button' and text()='Add New']")));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", addNewButton);
                Thread.Sleep(500);
                addNewButton.Click();
                Console.WriteLine("✅ 'Add New' button in Skills section clicked.");

                // ✅ Fill skill input
                var skillInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Add Skill']")));
                skillInput.Clear();
                skillInput.SendKeys(skill);
                Console.WriteLine($"✅ Entered skill: {skill}");

                // ✅ Select level if it's not invalid or blank
                if (!string.IsNullOrWhiteSpace(level) && level != "Invalid")
                {
                    var levelDropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='level']")));
                    var select = new SelectElement(levelDropdown);
                    select.SelectByText(level);
                    Console.WriteLine($"✅ Selected level: {level}");
                }
                else
                {
                    Console.WriteLine("⚠️ Level input is blank or invalid. Skipping selection.");
                }


                // ✅ Click Add
                var addButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@value='Add']")));
                js.ExecuteScript("arguments[0].scrollIntoView(true);", addButton);
                addButton.Click();
                Console.WriteLine("✅ Clicked Add to submit skill.");

                // ✅ Wait for any toast message (positive or negative)
                wait.Until(d =>
                {
                    var toasts = d.FindElements(By.CssSelector("div.ns-box-inner"));
                    return toasts.Any(el => el.Displayed && !string.IsNullOrEmpty(el.Text));
                });
                Console.WriteLine("✅ Toast appeared after submission.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR adding skill: " + ex.Message);
            }
        }

    }

}
