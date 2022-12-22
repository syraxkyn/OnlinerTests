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
            _pageConfiguration = new ConfigurationBuilder().AddJsonFile(@"E:\тестирование\OnlinerTests\OnlinerTests\config.json", optional: true, reloadOnChange: true).Build();
            catalogPage = new OnlinerCatalogPage(_driver, _pageConfiguration);
        }

        [Test(Description ="Open catalog page")]
        [AllureSeverity(Allure.Commons.SeverityLevel.critical)]
        [AllureOwner("Andrey")]
        public void Test1()
        {
            catalogPage.OpenPage();
            Assert.Pass();
        }

        [Test(Description = "Checking add a laptop to the cart")]
        public void Test2()
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

        //тест на проверку фильтра
        [Test(Description ="Filter testing")]
        [Parallelizable]
        public void Test3()
        {
            catalogPage.OpenPage();
            catalogPage.OpenLaptopCatalog();
            _driver.FindElement(By.CssSelector("#schema-filter > div:nth-child(8) > div:nth-child(4) > div.schema-filter__facet > ul > li:nth-child(2) > label > span.i-checkbox > span")).Click();
            By asusList = By.CssSelector("#container > div > div > div > div > div.catalog-content.js-scrolling-area > div.schema-grid__wrapper > div.schema-grid > div.js-schema-results.schema-grid__center-column");
            Assert.IsTrue(_driver.FindElement(asusList).Text.Contains("ASUS"));
        }

        //тест на ввод количества значений
        [Test(Description = "Entering the number of laptops")]
        [Parallelizable]
        public void Test4()
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
        //инкремент-декремент
        [Test(Description ="Increment/Decrement count of laptop")]
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
        //проверка на вход
        [Test(Description = "Login with valid credentials via VK")]
        [Parallelizable]
        public void Test6()
        {
            catalogPage.OpenPage();
            OnlinerUser CurrentUser = new OnlinerUser();
            CurrentUser.PhoneNumber = _pageConfiguration["phonenumber"];
            CurrentUser.Password = _pageConfiguration["password"];
            catalogPage.LoginViaVK(CurrentUser);
            Assert.IsTrue(_driver.FindElement(OnlinerCatalogPage._profileImage).Displayed);
        }
        [Test(Description ="Login with invalid credentials")]
        public void Test7()
        {
            catalogPage.OpenPage();
            OnlinerUser CurrentUser = new OnlinerUser();
            CurrentUser.PhoneNumber = _pageConfiguration["phonenumber"];
            CurrentUser.Password = _pageConfiguration["password"];
            catalogPage.Login(CurrentUser);
            Assert.IsTrue(_driver.FindElement(OnlinerCatalogPage._invalidCredentialsText).Displayed);
        }

        [TearDown]
        public void Cleanup()
        {
            DriverSingleton.CloseDriver();
        }
    }
}