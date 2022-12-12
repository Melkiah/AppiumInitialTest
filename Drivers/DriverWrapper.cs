using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppiumTest.Drivers
{
    public class DriverWrapper : IDisposable
    {
        private AppiumDriver<IWebElement> driver;

        public DriverWrapper(AppiumDriver<IWebElement> driver)
        {
            this.driver = driver;
        }

        public AndroidDriver<IWebElement> GetAndroidDriver(Uri uri, AppiumOptions options)
        {
            return new AndroidDriver<IWebElement>(uri, options);
        }

        public void Dispose()
        {
            this.driver.Quit();
        }
    }
}
