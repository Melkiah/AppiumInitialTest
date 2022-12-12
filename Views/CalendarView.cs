using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppiumTest.Views
{
    public class CalendarView
    {
        private AppiumDriver<AppiumWebElement> AppiumDriver;

        List<AppiumWebElement> mounthTitle => this.AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/tvMonthLabel").ToList();
        List<AppiumWebElement> days => this.AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/calendar_day_text_view").ToList();
        List<AppiumWebElement> dayCells => this.AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/root_calendar_day_cell").ToList();
        AppiumWebElement monthView => this.AppiumDriver.FindElementsById("com.hopper.mountainview.play:id/gvMonthView").ToList().First();

        AppiumWebElement selectedDatesButton => this.AppiumDriver.FindElementById("com.hopper.mountainview.play:id/selectTheseDatesButton");

        public CalendarView(AppiumDriver<AppiumWebElement> appiumDriver)
        {
            this.AppiumDriver = appiumDriver;
        }

        public void SelectDefaulDates()
        {
            selectedDatesButton.Click();
        }

        public void SelectNextDay()
        {
            //Clear Default Selection, Works only for current month for now
            AppiumWebElement tommorowCell = AppiumDriver.FindElementByXPath($"//android.widget.LinearLayout[1]/android.widget.GridView//android.widget.TextView[@text=\"{DateTime.Today.AddDays(1).Day.ToString()}\"]");
            AppiumWebElement dayAfterTommorow = AppiumDriver.FindElementByXPath($"//android.widget.LinearLayout[1]/android.widget.GridView//android.widget.TextView[@text=\"{DateTime.Today.AddDays(2).Day.ToString()}\"]");

            dayAfterTommorow.Click();
            tommorowCell.Click();
            dayAfterTommorow.Click();

            selectedDatesButton.Click();
        }
    }
}
