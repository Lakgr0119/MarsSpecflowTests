using MarsSpecflowTests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using System;

namespace MarsSpecflowTests.Hooks
{
    [Binding]
    public class Hooks1
    {
        private static IWebDriver driver;  // ✅ WebDriver is now static and shared across tests
        private static bool isLoggedIn = false;  // ✅ Ensures login happens only once

         [BeforeScenario] 
     //   [BeforeScenario("negative")]
        public void BeforeScenario()
        {
            if (driver == null)  // ✅ WebDriver is created ONCE and reused
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                Console.WriteLine("✅ WebDriver initialized.");
            }

            ScenarioContext.Current["currentDriver"] = driver;  // ✅ WebDriver stored in ScenarioContext

            // Ensure login happens only once for all tests
            if (!isLoggedIn)
            {
                PerformLogin(); // ✅ Added this to make sure login happens before any test
                isLoggedIn = true;
            }
        }

        private void PerformLogin()
        {
            Console.WriteLine("🔄 Redirecting to Login Page...");
            driver.Navigate().GoToUrl("http://localhost:5000/Home"); // ✅ Ensure the site loads first

            try
            {
                // ✅ Wait for the login button and click
                driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]")).Click();
                Console.WriteLine("✅ Navigated to Sign-In Page.");

                // ✅ Enter credentials  
                driver.FindElement(By.Name("email")).SendKeys("anya@gmail.com");
                driver.FindElement(By.Name("password")).SendKeys("123123");
                driver.FindElement(By.XPath("//button[contains(text(),'Login')]")).Click();
                Console.WriteLine("✅ Successfully logged in.");

                // ✅ Wait for redirect to profile page
                System.Threading.Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERROR: Login failed - " + ex.Message);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("⚠️ Test completed. WebDriver remains open.");
        }

        [AfterTestRun]  // ✅ Closes WebDriver AFTER ALL tests finish
        public static void AfterTestRun()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
                Console.WriteLine("✅ WebDriver session closed after all tests.");
            }
        }
    }
}
    