using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppiumTest.Views
{
    public class HotelCover : BaseView
    {
        private AppiumDriver<AppiumWebElement> AppiumDriver;

        private AppiumWebElement AddWatchButton => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/addWatchButton");
        private AppiumWebElement RemoveWatchButton => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/removeWatchButton");
        private AppiumWebElement HotelTitle => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/hotelName");

        private AppiumWebElement Dates => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/datesSelector");

        public HotelCover(AppiumDriver<AppiumWebElement> appiumDriver) :base(appiumDriver)
        {
            this.AppiumDriver = appiumDriver;
        }

        public void AddHotelToWatch()
        {
            //check
            AddWatchButton.Click();
        }

        public void RemoveHotelToWatch()
        {
            //check
            RemoveWatchButton.Click();
        }

        public bool IsHotelSelected()
        {
            try
            {
                return AddWatchButton.Displayed;
            }
            catch (NoSuchElementException)
            {

                return false;
            }
        }

        public void SelectDatesSection()
        {
            Dates.Click();
        }
    }
}
