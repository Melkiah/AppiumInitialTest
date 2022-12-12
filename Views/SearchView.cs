using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppiumTest.Views
{
    public class SearchView
    {
        private AppiumDriver<AppiumWebElement> AppiumDriver;
        private WebDriverWait Wait;
        private IWebElement searchInputField => Wait.Until(d => d.FindElement(By.Id("com.hopper.mountainview.play:id/locationSearchEditText")));

        private List<AppiumWebElement> searchResults => AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/airport_name").ToList();

        public SearchView(AppiumDriver<AppiumWebElement> appiumDriver)
        {
            this.AppiumDriver = appiumDriver;
            Wait = new WebDriverWait(this.AppiumDriver, TimeSpan.FromSeconds(5));
            Wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        }

        public void SearchDestination(string destination)
        {
            searchInputField.SendKeys(destination);

            if (AppiumDriver.IsKeyboardShown())
            {
                AppiumDriver.HideKeyboard();
            }
        }

        public void SelectDestination(string destination)
        {
            Wait.Until(_ => searchResults != null);

            if (searchResults is null)
            {
                Assert.Fail("No results found!");
            }

            foreach (var result in searchResults)
            {
                var a = result.Text;

                bool match = a == destination;

                if (result.Text == destination)
                {
                    result.Click();
                    continue;
                }
            }
        }

        public void SelectDestination(int index = 0)
        {
            Wait.Until(_ => searchResults != null);

            if (searchResults is null)
            {
                Assert.Fail("No results found!");
            }

            if (index == 0)
            {
                searchResults.First().Click();
            }
            else if(index < searchResults.Count)
            {
                searchResults[index].Click();
            }
            else
            {
                Assert.Fail("Not enough results!");
            }
        }
    }
}
