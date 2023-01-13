using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using OnlinerTests.Driver;
using OnlinerTests.Model;
using OnlinerTests.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;

namespace OnlinerTests
{
    [TestFixture]
    [AllureNUnit]
    public class Tests
    {
        private IWebDriver _driver;
        private IConfiguration _pageConfiguration;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private OnlinerCatalogPage catalogPage;
        [SetUp]
        public void Setup()
        {
            logger.Info("Starting application.");
            _driver = DriverSingleton.GetDriver();
            _driver.Manage().Window.Maximize();
            _pageConfiguration = new ConfigurationBuilder().AddJsonFile(@"E:\testing\OnlinerTests\OnlinerTests\config.json", optional: true, reloadOnChange: true).Build();
            catalogPage = new OnlinerCatalogPage(_driver, _pageConfiguration);
        }
        public void Test1()
        {
            catalogPage.OpenPage();
            catalogPage.OpenLaptopCatalog();
            catalogPage.OpenLaptopInfo();
            string laptopCatalogName = _driver.FindElement(OnlinerCatalogPage._laptopCatalogName).Text;
            catalogPage.AddLaptopToWishList();
            catalogPage.OpenWishList();
            string laptopWishList = _driver.FindElement(OnlinerCatalogPage._laptopWishName).Text;
            //By laptop = By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-form__offers > div > div > div.cart-form__offers-item.cart-form__offers-item_secondary > div > div.cart-form__offers-part.cart-form__offers-part_data > div.cart-form__description.cart-form__description_primary.cart-form__description_base-alter.cart-form__description_font-weight_semibold.cart-form__description_condensed-other > a");
            Assert.AreEqual(laptopCatalogName, laptopWishList);
        }
        public void Test2()
        {
            catalogPage.OpenPage();
            catalogPage.OpenLaptopCatalog();
            _driver.FindElement(By.CssSelector("#schema-filter > div:nth-child(8) > div:nth-child(4) > div.schema-filter__facet > ul > li:nth-child(2) > label > span.i-checkbox > span")).Click();
            By asusList = By.CssSelector("#container > div > div > div > div > div.catalog-content.js-scrolling-area > div.schema-grid__wrapper > div.schema-grid > div.js-schema-results.schema-grid__center-column");
            Assert.IsTrue(_driver.FindElement(asusList).Text.Contains("ASUS"));
        }
        public void Test3()
        {
            catalogPage.OpenPage();
            catalogPage.OpenLaptopCatalog();
            catalogPage.AddLaptopToWishList();
            catalogPage.OpenWishList();
            catalogPage.ChangeOrderCount(4);
            Thread.Sleep(3000);
            string textBoxCount = _driver.FindElement(OnlinerCatalogPage._countText).Text;

            Assert.AreEqual("14", textBoxCount.Substring(0,textBoxCount.IndexOf(' ')));
        }

        public void Test4()
        {
            catalogPage.OpenPage();
            catalogPage.OpenLaptopCatalog();

            catalogPage.AddLaptopToWishList();
            catalogPage.OpenWishList();

            catalogPage.DecrementOrderCount();
            string textBoxCount = _driver.FindElement(OnlinerCatalogPage._countText).Text;
            Assert.AreEqual("2", textBoxCount.Substring(0, textBoxCount.IndexOf(' ')));
        }

        public void Test5()
        {
            catalogPage.OpenPage();
            catalogPage.OpenLaptopCatalog();
            catalogPage.AddLaptopToWishList();
            catalogPage.OpenWishList();
            catalogPage.IncrementOrderCount();
            catalogPage.IncrementOrderCount();
            catalogPage.DecrementOrderCount();
            Thread.Sleep(3000);
            string textBoxCount = _driver.FindElement(OnlinerCatalogPage._countText).Text;
            Assert.AreEqual("2", textBoxCount.Substring(0, textBoxCount.IndexOf(' ')));
        }

        public void Test6()
        {
            catalogPage.OpenPage();
            catalogPage.OpenSupport();
            catalogPage.SendSupportData("Android", "ash03@tut.by", "Я", "Каталог", "small", "big");
            Assert.AreEqual(_driver.FindElement(By.CssSelector("#main > div > h1 > i")).Text,"Заявка была принята");
        }
        
        public void Test7()
        {
            catalogPage.OpenPage();
            catalogPage.OpenWishList();
            Assert.AreEqual("Минск", _driver.FindElement(OnlinerCatalogPage._location).Text);
        }

        public void Test8()
        {
            catalogPage.OpenPage();
            catalogPage.OpenWishList();
            string laptopCatalogName = _driver.FindElement(OnlinerCatalogPage._laptopCatalogName).Text;
            catalogPage.AddLaptopToWishList();
            catalogPage.OpenWishList();
            string laptopWishList = _driver.FindElement(OnlinerCatalogPage._laptopWishName).Text;
            Assert.AreEqual(_driver.FindElement(By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-message")).Text,"Ваша корзина пуста");
        }

        public void Test9()
        {
            catalogPage.OpenPage();
            catalogPage.OpenLaptopCatalog();
            string laptopCatalogName = _driver.FindElement(OnlinerCatalogPage._laptopCatalogName).Text;
            catalogPage.AddLaptopToWishList();
            catalogPage.OpenWishList();
            catalogPage.ChangeOrderCount();
            catalogPage.OpenLaptopCatalog();
            catalogPage.AddLaptopToWishList();

            //By laptop = By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-form__offers > div > div > div.cart-form__offers-item.cart-form__offers-item_secondary > div > div.cart-form__offers-part.cart-form__offers-part_data > div.cart-form__description.cart-form__description_primary.cart-form__description_base-alter.cart-form__description_font-weight_semibold.cart-form__description_condensed-other > a");
            Assert.AreEqual(laptopCatalogName, laptopWishList);
        }

        public void Cleanup()
        {
            DriverSingleton.CloseDriver();
        }
    }
}