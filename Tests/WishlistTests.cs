using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using System;
using System.Linq;
using AppiumTest.Constants;
using System.Threading;
using AppiumTest.Views;

namespace AppiumTest
{
    [TestFixture("Android", "8d39211", "My Phone", "10")]
    public class WishlistTests
    {
        private AppiumDriver<AppiumWebElement> AppiumDriver;
        private HomeView HomeView;
        private SearchView SearchView;
        private CalendarView CalendarView;
        private HotelsLists HotelsLists;
        private FavouritesView FavouritesView;
        private HotelCover HotelCover;

        private string PlatformName;
        private string Udid;
        private string DeviceName;
        private string PlatformVersion;

        public WishlistTests(string platfromName, string udid, string deviceName, string platformVersion)
        {
            this.PlatformName = platfromName;
            this.Udid = udid;
            this.DeviceName = deviceName;
            this.PlatformVersion = platformVersion;
        }


        [SetUp]
        public void Setup()
        {
            AppiumOptions driverOptions = new AppiumOptions();

            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, this.PlatformName);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.Udid, this.Udid);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, this.DeviceName);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, this.PlatformVersion);
            //driverOptions.AddAdditionalCapability(MobileCapabilityType.FullReset, "true");

            this.AppiumDriver = new AndroidDriver<AppiumWebElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions);

            HomeView = new HomeView(this.AppiumDriver);
            SearchView = new SearchView(this.AppiumDriver);
            CalendarView = new CalendarView(this.AppiumDriver);
            HotelsLists = new HotelsLists(this.AppiumDriver);
            FavouritesView = new FavouritesView(this.AppiumDriver);
            HotelCover = new HotelCover(this.AppiumDriver);
        }

        [Test]
        public void AddHotelFromHotelList()
        {
            AppiumDriver.ActivateApp(Consts.AndroidAppPackage);
            //HomeView.DismissSplashScreen();
            HomeView.NavigateToHotels();
            SearchView.SearchDestination("Chicago");
            SearchView.SelectDestination();
            CalendarView.SelectDefaulDates();
            string addedHotelName = HotelsLists.AddFirstResultToWatchlist();
            HomeView.NavigateToHome();
            HomeView.SelectFavouritesSection();
            FavouritesView.VerifyHotelPresent(addedHotelName);
            FavouritesView.RemoveHotelFromList();
        }

        [Test]
        public void ChangeDatesInFavoritesSection()
        {
            AppiumDriver.ActivateApp(Consts.AndroidAppPackage);
            HomeView.DismissSplashScreen();
            HomeView.NavigateToHotels();
            SearchView.SearchDestination("Chicago");
            SearchView.SelectDestination();
            CalendarView.SelectDefaulDates();
            string addedHotelName = HotelsLists.AddFirstResultToWatchlist();
            HomeView.NavigateToHome();
            HomeView.SelectFavouritesSection();
            FavouritesView.VerifyHotelPresent(addedHotelName);
            FavouritesView.SelectFirstHotel();
            HotelCover.SelectDatesSection();
            CalendarView.SelectNextDay();
            Assert.IsTrue(HotelCover.IsHotelSelected());
            HotelCover.SelectBackButton();
            FavouritesView.RemoveHotelFromList();
        }

        [TearDown]
        public void Dispose()
        {            
            AppiumDriver.Quit();
        }
    }
}