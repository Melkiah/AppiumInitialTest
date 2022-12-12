using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AppiumTest.Views
{
    public class HomeView : BaseView
    {
        private AppiumDriver<AppiumWebElement> AppiumDriver;
        private List<IWebElement> splashScreen = null;
        
        private AppiumWebElement hotellsButton => AppiumDriver.FindElementByXPath(@"//*[@text=""Hotels""]");
        private AppiumWebElement userSettings;
        private AppiumWebElement favouritesSection => AppiumDriver.FindElementById("com.hopper.mountainview.play:id/heart");
        private WebDriverWait wait;

        public HomeView(AppiumDriver<AppiumWebElement> appiumDriver) : base (appiumDriver)
        {
            this.AppiumDriver = appiumDriver;
            this.wait = new WebDriverWait(appiumDriver, TimeSpan.FromSeconds(3));
            this.wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public void NavigateToHotels()
        {
            ScrollToNavigation();
            hotellsButton.Click();
        }

        public void SelectFavouritesSection()
        {
            ScrollToBottom();
            favouritesSection.Click();
        }

        public void NavigateToHome()
        {
            int count = 3;
            do
            {
                try
                {
                    userSettings = AppiumDriver.FindElementById("com.hopper.mountainview.play:id/headerSettingsIcon");
                }
                catch (NoSuchElementException)
                {
                    SelectBackButton();
                }

                count--;
            } while (count != 0 || userSettings == null);
        }

        public void DismissSplashScreen()
        {
            wait.Until(f => splashScreen = f.FindElements(By.Id("com.hopper.mountainview.play:id/untouchableRecyclerView")).ToList());

            if (splashScreen != null)
            {
                splashScreen
                    .First()
                    .FindElements(By.ClassName("android.widget.TextView"))
                    .ToList()
                    .Last()
                    .Click();
            }
        }
    }
}
