using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsSpecflowTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace MarsSpecflowTests.Pages
{
    public class LanguagesPage
    {
        public IWebDriver driver;
      public LanguagesPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        /*     public void navigateToProfilePage()
               {

                     try
                     {

                       if (!driver.Url.Contains("http://localhost:5000/Account/Profile"))
                         {
                             Console.WriteLine("🌐 Navigating to Profile Page...");
                    //driver.Navigate().GoToUrl("http://localhost:5000/Account/Profile");

                    // ✅ Wait until the URL contains (Max: 10s)
                    Thread.Sleep(5000);

                }

                         Console.WriteLine("✅ Profile Page Loaded Successfully");
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine($"❌ Error navigating to Profile Page: {ex.Message}");
                     }
               }*/

        public void navigateToProfilePage()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                Console.WriteLine("🔍 Current URL Before Check: " + driver.Url);
                if (driver.Url.Contains("/Account/Profile"))
                {
                    Console.WriteLine("✅ Already on Profile Page, skipping login.");
                    return;
                }
                Console.WriteLine("🌐 Navigating to Profile Page...");
                driver.Navigate().GoToUrl("http://localhost:5000/Account/Profile");
               // languagesPage.navigateToProfilePage();
                // Ensure Profile Page is fully loaded before moving forward
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//h3[text()='Languages']")));
                Console.WriteLine("✅ Profile Page Loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: Profile Page did not load properly - {ex.Message}");
            }
        }





            public void ClickAddNewButton()
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

                    // ✅ Declare JavaScript Executor at the start
                    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

                    // ✅ Check if a modal overlay exists
                    By modalOverlay = By.XPath("//div[contains(@class, 'ui page modals dimmer') and contains(@class, 'visible active')]");
                    if (driver.FindElements(modalOverlay).Count > 0)
                    {
                        Console.WriteLine("⚠️ WARNING: A modal overlay is blocking the button. Attempting to close it...");

                        // Try clicking outside the modal to dismiss it
                        jsExecutor.ExecuteScript("document.body.click();");

                        // Wait a bit and check again
                        Thread.Sleep(1000);
                        if (driver.FindElements(modalOverlay).Count > 0)
                        {
                            Console.WriteLine("⚠️ Modal still exists. Trying to click the close button...");

                            // Try clicking the close button if it exists
                            By closeButton = By.XPath("//button[contains(text(), 'Close') or contains(@class, 'close')]");
                            if (driver.FindElements(closeButton).Count > 0)
                            {
                                driver.FindElement(closeButton).Click();
                                Console.WriteLine("✅ Modal close button clicked.");
                            }
                            else
                            {
                                Console.WriteLine("⚠️ No close button found. Trying JavaScript force removal.");
                                jsExecutor.ExecuteScript("document.querySelector('.ui.page.modals.dimmer.visible.active').remove();");
                            }
                        }

                        // Wait until modal is completely gone
                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(modalOverlay));
                        Console.WriteLine("✅ Modal overlay removed. Proceeding to click 'Add New'.");
                    }

                    // Locate the 'Add New' button
                    IWebElement addNewButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//h3[text()='Languages']/following::table[1]//div[contains(@class, 'ui teal button')]")));

                    Console.WriteLine("✅ DEBUG: 'Add New' button is now clickable.");

                    // Scroll into view before clicking
                    jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", addNewButton);
                    Thread.Sleep(500); // Allow time for scrolling

                    // Click the button
                    addNewButton.Click();
                    Console.WriteLine("✅ SUCCESS: 'Add New' button clicked.");

                    // Wait for the input fields to appear
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Add Language']")));
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Add')]")));
                    Console.WriteLine("✅ SUCCESS: Language input fields appeared.");
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("❌ ERROR: 'Add New' button clicked, but input fields did not appear.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ ERROR: Unexpected issue clicking 'Add New' - " + ex.Message);
                }
            }








   

        public void ClickAddButton()
        {
            try
            {
                var addButton = driver.FindElement(By.XPath("//input[@value='Add']"));
                addButton.Click();
                Console.WriteLine("✅ 'Add' button clicked successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR: Failed to click 'Add' button - " + ex.Message);
            }
        }

        public void DeleteLanguage(string languageToDelete)
        {
            try
            {
                Console.WriteLine($"🗑️ Attempting to delete language: {languageToDelete}");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // ✅ Wait for any notification popup to disappear before proceeding
                By toast = By.CssSelector("div.ns-box.ns-show");
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(toast));

                // Locate the table rows with languages
                var rows = driver.FindElements(By.XPath("//table[@class='ui fixed table']/tbody/tr"));

                foreach (var row in rows)
                {
                    var langName = row.FindElement(By.XPath(".//td[1]")).Text.Trim();
                    if (langName.Equals(languageToDelete, StringComparison.OrdinalIgnoreCase))
                    {
                        var deleteIcon = row.FindElement(By.XPath(".//i[@class='remove icon']"));

                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", deleteIcon);
                        Thread.Sleep(500); // Give some time after scroll

                        deleteIcon.Click();
                        Console.WriteLine($"✅ Language '{languageToDelete}' deleted successfully.");
                        return;
                    }
                }

                Console.WriteLine($"⚠️ Language '{languageToDelete}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR deleting language '{languageToDelete}' - {ex.Message}");
            }
        }

        public bool IsLanguagePresent(string language)
        {
            try
            {
                var rows = driver.FindElements(By.XPath("//table[@class='ui fixed table']//tbody/tr"));
                foreach (var row in rows)
                {
                    var languageCell = row.FindElement(By.XPath("./td[1]")).Text.Trim();
                    if (languageCell.Equals(language, StringComparison.OrdinalIgnoreCase))
                    {
                        return true; // Language still present
                    }
                }
                return false; // Language not found
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR checking language presence - " + ex.Message);
                return false;
            }
        }
            public void EditLanguage(string oldLanguage, string newLanguage, string newLevel)
              {
                  try
                  {
                      Console.WriteLine($"✏️ Attempting to edit language '{oldLanguage}' to '{newLanguage}' with level '{newLevel}'");

                      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                      // Locate the language row by oldLanguage name
                      var rows = driver.FindElements(By.XPath("//table[@class='ui fixed table']//tbody/tr"));

                      foreach (var row in rows)
                      {
                          if (row.Text.Contains(oldLanguage))
                          {
                              // Click the edit icon
                              var editIcon = row.FindElement(By.XPath(".//i[@class='outline write icon']"));
                              editIcon.Click();

                              // Wait for editable fields to appear
                              wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Add Language']")));
                              wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='level']")));

                              // Clear and enter new language
                              var languageInput = driver.FindElement(By.XPath("//input[@placeholder='Add Language']"));
                              languageInput.Clear();
                              languageInput.SendKeys(newLanguage);
                              Console.WriteLine($"✅ Updated language text: {newLanguage}");

                              // Select the new level
                              var levelDropdown = driver.FindElement(By.XPath("//select[@name='level']"));
                              var selectElement = new SelectElement(levelDropdown);
                              selectElement.SelectByText(newLevel);
                              Console.WriteLine($"✅ Updated level: {newLevel}");

                              // Click the Update button
                              var updateButton = driver.FindElement(By.XPath("//input[@value='Update'] | //button[text()='Update']"));
                              updateButton.Click();
                              Console.WriteLine("✅ Clicked 'Update' button");

                              // Confirm the new entry is updated
                              wait.Until(driver =>
                                  driver.FindElements(By.XPath("//table[@class='ui fixed table']//tbody/tr"))
                                  .Any(updatedRow => updatedRow.Text.Contains(newLanguage) && updatedRow.Text.Contains(newLevel)));

                              Console.WriteLine($"✅ Language successfully updated to: {newLanguage} - {newLevel}");
                              return;
                          }
                      }

                      Console.WriteLine($"❌ Language '{oldLanguage}' not found to edit.");
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"❌ ERROR updating language - {ex.Message}");
                  }
              }


        /* public void EnterLanguageDetails(string language, string level)
         {
             try
             {
                 WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                 IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

                 // Ensure input fields are visible
                 IWebElement languageInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Add Language']")));
                 IWebElement levelDropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='level']")));
                 IWebElement addButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@value='Add']")));

                 // Enter language
                 languageInput.Clear();
                 languageInput.SendKeys(language);
                 Console.WriteLine($"✅ Entered language: {language}");

                 // Select level
                 SelectElement select = new SelectElement(levelDropdown);
                 select.SelectByText(level);
                 Console.WriteLine($"✅ Selected level: {level}");

                 // Click "Add" button
                 addButton.Click();
                 Console.WriteLine("✅ Clicked 'Add' button to submit the language.");

                 // 🛡️ Wait for any toast to appear (positive or negative)
                 wait.Until(driver =>
                 {
                     var toasts = driver.FindElements(By.CssSelector("div.ns-box-inner"));
                     return toasts.Any(el => el.Displayed && !string.IsNullOrEmpty(el.Text));
                 });

                 Console.WriteLine("✅ Toast appeared after submission.");
             }
             catch (Exception ex)
             {
                 Console.WriteLine("❌ ERROR: Unable to enter language details - " + ex.Message);
             }
         }
        */

        public void EnterLanguageDetails(string language, string level)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;

                // Ensure input fields are visible
                IWebElement languageInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Add Language']")));
                IWebElement levelDropdown = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//select[@name='level']")));
                IWebElement addButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@value='Add']")));

                // Enter Language
                languageInput.Clear();
                languageInput.SendKeys(language);
                Console.WriteLine($"✅ Entered language: {language}");

                // Conditionally select level only if not blank
                if (!string.IsNullOrWhiteSpace(level))
                {
                    SelectElement select = new SelectElement(levelDropdown);
                    select.SelectByText(level);
                    Console.WriteLine($"✅ Selected level: {level}");
                }
                else
                {
                    Console.WriteLine("⚠️ Level input is blank or default. Skipping selection.");
                }

                // Click Add button
                addButton.Click();
                Console.WriteLine("✅ Clicked 'Add' button to submit the language.");

                // Wait for any visible toast message
                wait.Until(driver =>
                {
                    var toasts = driver.FindElements(By.CssSelector("div.ns-box-inner"));
                    return toasts.Any(el => el.Displayed && !string.IsNullOrEmpty(el.Text));
                });

                Console.WriteLine("✅ Toast appeared after submission.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR: Unable to enter language details - " + ex.Message);
            }
        }

        public bool IsValidationMessageDisplayed()
        {
            try
            {
                return driver.FindElement(By.XPath("//div[contains(text(), 'Please enter language and level')]")).Displayed;
            }
            catch { return false; }
        }

        public bool IsDuplicateLanguageMessageDisplayed()
        {
            try
            {
                return driver.FindElement(By.XPath("//div[contains(text(), 'This language already exists')]")).Displayed;
            }
            catch { return false; }
        }

        public bool IsMaxLanguagesMessageDisplayed()
        {
            try
            {
                return driver.FindElement(By.XPath("//div[contains(text(), 'maximum')]")).Displayed;
            }
            catch { return false; }
        }
        public string GetErrorMessage()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                var toast = wait.Until(driver =>
                {
                    var elements = driver.FindElements(By.CssSelector("div.ns-box-inner"));
                    foreach (var el in elements)
                    {
                        if (el.Displayed && !string.IsNullOrWhiteSpace(el.Text))
                        {
                            return el;
                        }
                    }
                    return null;
                });

                string message = toast.Text.Trim();
                Console.WriteLine($"✅ Error toast message found: {message}");
                return message;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("❌ Toast message not found within wait time.");
                return string.Empty;
            }
        }








        public static void EnsureLanguageExists(string language, string level)
        {
            try
            {
             //   if (!IsLanguagePresent(language))
            //    {
                 //   AddLanguage(language, level);
            //        Thread.Sleep(1000);  // small wait for stability
             //   }
             //   else
             //   {
              //      Console.WriteLine($"ℹ️ Language '{language}' already exists. No need to add.");
               // }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR in EnsureLanguageExists: " + ex.Message);
            }
        }

        public void AddLanguage(string language, string level)
        {
            ClickAddNewButton();
          //  EnterLanguage(language);
          //  SelectLanguageLevel(level);
            ClickAddButton();
        }

        public string GetToastMessageUsingJS()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Wait for the inner toast message to appear
                wait.Until(d =>
                {
                    var element = ((IJavaScriptExecutor)d)
                        .ExecuteScript("return document.querySelector('.ns-box-inner');");

                    return element != null;
                });

                // Retrieve the actual toast message
                string toastText = (string)((IJavaScriptExecutor)driver)
                    .ExecuteScript("return document.querySelector('.ns-box-inner').innerText.trim();");

                Console.WriteLine($"✅ Toast message: {toastText}");
                return toastText;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Toast message not found: " + ex.Message);
                return string.Empty;
            }
        }



    }
}

