using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppiumTest.Views
{
    
    public class FavouritesView
    {
        AppiumDriver<AppiumWebElement> AppiumDriver;
        AppiumWebElement HotelName => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/hotelName");
        AppiumWebElement RemoveWatchButton => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/removeWatchButton");
        AppiumWebElement Title => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/title");

        List<AppiumWebElement> Hotels => this.AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/hotelImage").ToList();
        WebDriverWait WaitVisible;

        public FavouritesView(AppiumDriver<AppiumWebElement> appiumDriver)
        {
            this.AppiumDriver = appiumDriver;
            this.WaitVisible = new WebDriverWait(this.AppiumDriver, TimeSpan.FromSeconds(2));
        }

        public void VerifyHotelPresent(string hotelName)
        {
            Assert.AreEqual(HotelName.Text, hotelName);
        }

        public void RemoveHotelFromList()
        {
            RemoveWatchButton.Click();

            WaitVisible.Until(_ => Title.Displayed == true);

            Assert.AreEqual(Title.Text, "Save hotels you like and get the best prices");
        }

        public void SelectFirstHotel()
        {
            Hotels.First().Click();
        }
    }
}
