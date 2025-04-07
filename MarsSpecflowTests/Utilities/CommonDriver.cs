using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MarsSpecflowTests.Utilities
{
    public class CommonDriver
    {
        //public static IWebDriver driver;
        public static IWebDriver driver;

        public static void Initialize()
        {
            
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                driver.Navigate().GoToUrl("http://localhost:5000/Home");
            
        } 


        //Close the browser
        public static void Close()
        {
            
                //driver.Quit();
             //   driver.Dispose();
                
            
        }


    }

}