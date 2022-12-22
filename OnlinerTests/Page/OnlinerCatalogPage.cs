using Microsoft.Extensions.Configuration;
using OnlinerTests.Driver;
using OnlinerTests.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTests.Page
{
    internal class OnlinerCatalogPage
    {
        private const string _url = "https://catalog.onliner.by/";

        private IWebDriver _driver;

        public static By _computersButton = By.CssSelector("#container > div > div > div > div > div.catalog-navigation.catalog-navigation_opened > ul > li:nth-child(4)");
        public static By _laptopsButton = By.CssSelector("#widget-1-102 > div > a");
        public static By _laptopInfoButton = By.CssSelector("#schema-products > div:nth-child(2) > div > div.schema-product__part.schema-product__part_2 > div.schema-product__part.schema-product__part_4 > div.schema-product__title > a > span");
        public static By _addToWishListButton = By.CssSelector("#container > div > div > div > div > div.catalog-content.js-scrolling-area > div.product.product_details.b-offers.js-product > main > div > div > aside > div:nth-child(1) > div.product-aside__offers > div.product-aside__offers-list > div.product-aside__offers-item.product-aside__offers-item_primary > div.product-aside__control.product-aside__control_condensed-additional > a.button-style.button-style_base-alter.button-style_primary.product-aside__button.product-aside__button_narrow.product-aside__button_cart.button-style_expletive");
        public static By _incrementButton = By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-form__offers > div > div > div.cart-form__offers-item.cart-form__offers-item_secondary > div > div.cart-form__offers-part.cart-form__offers-part_action > div > div.cart-form__offers-part.cart-form__offers-part_count > div > div > div > a.button-style.button-style_auxiliary.button-style_small.cart-form__button.cart-form__button_increment.helpers_hide_tablet");
        public static By _countTextBox = By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-form__offers > div > div > div.cart-form__offers-item.cart-form__offers-item_secondary > div > div.cart-form__offers-part.cart-form__offers-part_action > div > div.cart-form__offers-part.cart-form__offers-part_count > div > div.input-style__combo.cart-form__input-combo > div > input");
        public static By _wishListButton = By.CssSelector("#userbar > div:nth-child(2) > div > a");
        public static By _asusCheckbox = By.CssSelector("#schema-filter > div:nth-child(8) > div:nth-child(4) > div.schema-filter__facet > ul > li:nth-child(2) > label > span.i-checkbox > span");
        public static By _countText = By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-form__offers > div > div > div.cart-form__offers-item.cart-form__offers-item_additional > div > div.cart-form__offers-part.cart-form__offers-part_total > div.cart-form__offers-flex.cart-form__offers-flex_extended-other > div.cart-form__offers-part.cart-form__offers-part_ammount.cart-form__offers-part_ammount_special > div");
        public static By _decrementButton = By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-form__offers > div > div > div.cart-form__offers-item.cart-form__offers-item_secondary > div > div.cart-form__offers-part.cart-form__offers-part_action > div > div.cart-form__offers-part.cart-form__offers-part_count > div > div.input-style__combo.cart-form__input-combo > div > a.button-style.button-style_auxiliary.button-style_small.cart-form__button.cart-form__button_decrement.helpers_hide_tablet");
        public static By _laptopCatalogName = By.CssSelector("#container > div > div > div > div > div.catalog-content.js-scrolling-area > div.product.product_details.b-offers.js-product > div > div.catalog-masthead > h1");
        public static By _laptopWishName = By.CssSelector("#container > div.cart-content > div > div > div > div > div.cart-form__body > div > div.cart-form__offers > div > div > div.cart-form__offers-item.cart-form__offers-item_secondary > div > div.cart-form__offers-part.cart-form__offers-part_data > div.cart-form__description.cart-form__description_primary.cart-form__description_base-alter.cart-form__description_font-weight_semibold.cart-form__description_condensed-other > a");

        public static By _authButton = By.CssSelector("#userbar > div:nth-child(1) > div > div > div.auth-bar__item.auth-bar__item--text");
        public static By _loginTextBox = By.CssSelector("#auth-container > div > div.auth-form__body > div > form > div:nth-child(1) > div > div.auth-form__row.auth-form__row_condensed-alter > div > div > div > div > input");
        public static By _passwordTextBox = By.CssSelector("#auth-container > div > div.auth-form__body > div > form > div:nth-child(2) > div > div > div > div > input");
        public static By _enterButton = By.CssSelector("#auth-container > div > div.auth-form__body > div > form > div.auth-form__control.auth-form__control_condensed-additional > button");
        public static By _captchaCheckbox = By.CssSelector("#recaptcha-anchor > div.recaptcha-checkbox-border");
        public static By _invalidCredentialsText = By.CssSelector("#auth-container > div > div.auth-form__body > div > form > div.auth-form__line.auth-form__line_condensed > div");

        public static By _vkontakteAuthButton = By.CssSelector("#userbar > div:nth-child(1) > div > div > div.auth-bar__item.auth-bar__item--vk-alter");
        public static By _vkontakteLoginTextBox = By.CssSelector("#login_submit > div > div > input:nth-child(7)");
        public static By _vkontaktePasswordTextBox = By.CssSelector("#login_submit > div > div > input:nth-child(9)");
        public static By _vkontakteSubmitButton = By.CssSelector("#install_allow");
        public static By _profileImage = By.CssSelector("#userbar > div > div.b-top-profile__item.b-top-profile__item_arrow > a > div");
        private IConfiguration _pageConfiguration { get; set; }

        public OnlinerCatalogPage(IWebDriver driver, IConfiguration pageConfiguration)
        {
            _driver = driver;
            _pageConfiguration = pageConfiguration;
        }

        public OnlinerCatalogPage OpenPage()
        {
            _driver.Navigate().GoToUrl(_url);
            return this;
        }

        public OnlinerCatalogPage Login(OnlinerUser user)
        {
            _driver.FindElement(_authButton).Click();
            _driver.FindElement(_loginTextBox).SendKeys(user.PhoneNumber);
            _driver.FindElement(_passwordTextBox).SendKeys(user.Password);
            _driver.FindElement(_enterButton).Click();
            //_driver.FindElement(_captchaCheckbox).Click();
            Thread.Sleep(3000);
            return this;
        }

        public OnlinerCatalogPage ChangeOrderCount(int numberOfProducts)
        {
            _driver.FindElement(_countTextBox).Click();
            Thread.Sleep(3000);
            _driver.FindElement(_countTextBox).SendKeys($"{numberOfProducts}");
            return this;
        }

        public OnlinerCatalogPage IncrementOrderCount()
        {
            _driver.FindElement(_incrementButton).Click();
            return this;
        }

        public OnlinerCatalogPage DecrementOrderCount()
        {
            _driver.FindElement(_decrementButton).Click();
            return this;
        }

        public OnlinerCatalogPage LoginViaVK(OnlinerUser user)
        {
            _driver.FindElement(_vkontakteAuthButton).Click();
            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            Thread.Sleep(5000);
            _driver.FindElement(_vkontakteLoginTextBox).SendKeys(user.PhoneNumber);
            _driver.FindElement(_vkontaktePasswordTextBox).SendKeys(user.Password);
            _driver.FindElement(_vkontakteSubmitButton).Click();
            Thread.Sleep(5000);
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            return this;
        }

        public OnlinerCatalogPage OpenLaptopCatalog()
        {
            _driver.FindElement(_computersButton).Click();
            _driver.FindElement(_laptopsButton).Click();
            return this;
        }

        public OnlinerCatalogPage OpenLaptopInfo()
        {
            _driver.FindElement(_laptopInfoButton).Click();
            return this;
        }
        public OnlinerCatalogPage AddLaptopToWishList()
        {
            _driver.FindElement(_addToWishListButton).Click();
            return this;
        }

        public OnlinerCatalogPage OpenWishList()
        {
            _driver.Navigate().GoToUrl("https://cart.onliner.by/");
            return this;
        }
    }
}
