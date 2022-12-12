using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AppiumTest.Views
{

    public class BaseView
    {
        private AppiumDriver<AppiumWebElement> AppiumDriver;
        private WebDriverWait WaitVisible;

        private AppiumWebElement NavigationSection;

        private AppiumWebElement BackNavigation => this.AppiumDriver.FindElementByAccessibilityId("Navigate up");
        private AppiumWebElement GoggleMap => this.AppiumDriver.FindElementByAccessibilityId("Google Map");

        public BaseView(AppiumDriver<AppiumWebElement> appiumDriver)
        {
            this.AppiumDriver = appiumDriver;
            this.WaitVisible = new WebDriverWait(this.AppiumDriver, TimeSpan.FromSeconds(1));
            this.WaitVisible.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public void SelectBackButton()
        {
            BackNavigation.Click();
        }

        public void ScrollHalfAScreenDown(bool evadeMapsElement = false)
        {
            Size screeenSize = this.AppiumDriver.Manage().Window.Size;

            int X = screeenSize.Width / 2;
            int Y = screeenSize.Height / 2;

            if (evadeMapsElement)
            {
                //var a = GoggleMap.Location.X;
                var b = GoggleMap.Location.Y;
                //var c = GoggleMap.Size.Width;
                var d = GoggleMap.Size.Height;

                Y = b + d + 10;
            }

            new TouchAction(this.AppiumDriver)
            .Press(X, Y)
            .Wait(500)
            .MoveTo(X, screeenSize.Height / 4)
            .Release()
            .Perform();
        }

        public void ScrollHalfAScreenUP()
        {
            Size screeenSize = this.AppiumDriver.Manage().Window.Size;
            new TouchAction(this.AppiumDriver)
            .Press(screeenSize.Width / 2, screeenSize.Height/2)
            .Wait(500)
            .MoveTo(screeenSize.Width / 2, screeenSize.Height)
            .Release()
            .Perform();
        }

        public void ScrollToNavigation()
        {
            int count = 5;

            do
            {
                try
                {
                    NavigationSection = this.AppiumDriver.FindElementByXPath(@"//*[@text=""Hotels""]");
                }
                catch (NoSuchElementException)
                {
                    ScrollHalfAScreenUP();
                }

                count--;

                if (count == 0)
                {
                    break;
                }

            } while (NavigationSection == null);
        }

        public void ScrollToBottom()
        {
            for (int i = 0; i < 6; i++)
            {
                ScrollHalfAScreenDown();
            }
        }

    }
}
