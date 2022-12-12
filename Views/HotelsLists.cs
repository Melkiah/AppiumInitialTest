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
    public class HotelsLists : BaseView
    {
        private AppiumDriver<AppiumWebElement> AppiumDriver;
        private WebDriverWait WaitVisible;

        List<AppiumWebElement> likeButton => this.AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/watchButton").ToList();
        List<AppiumWebElement> HotelTitles => this.AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/hotelName").ToList();
            
        IWebElement removeLikeButton => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/removeWatchButton");

        public HotelsLists(AppiumDriver<AppiumWebElement> appiumDriver) : base(appiumDriver)
        {
            this.AppiumDriver = appiumDriver;
            this.WaitVisible = new WebDriverWait(this.AppiumDriver, TimeSpan.FromSeconds(5));
        }

        public string AddFirstResultToWatchlist()
        {
            WaitVisible.Until(_ => likeButton != null);

            if (likeButton is null)
            {
                Assert.Fail("Like button not found or already selected");
            }

            likeButton.First().Click();

            Assert.True(removeLikeButton.Displayed, "Likes button has not changed states.");

            ScrollHalfAScreenDown(evadeMapsElement: true);

            return HotelTitles.First().Text;
        }
    }
}
