using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Edge;

namespace OnlinerTests.Driver
{
    public class DriverSingleton
    {
        static IConfiguration _appConfiguration;
        public static WebDriver driver;
        private DriverSingleton() { }
        public static IWebDriver GetDriver()
        {
            if (_appConfiguration == null)
            {
                var builder = new ConfigurationBuilder().AddJsonFile(@"E:\testing\OnlinerTests\OnlinerTests\config.json");
                _appConfiguration = builder.Build();
            }
            var driverName = _appConfiguration["driver"];
            if (driver == null)
            {
                switch (driverName)
                {
                    case "chrome":
                        {
                            var options = new ChromeOptions();
                            driver = new ChromeDriver(@"E:\ChromeDriver108", options);
                            break;
                        }
                    default:
                        {
                            var options = new EdgeOptions();
                            driver = new EdgeDriver(@"E:\EdgeDriver", options);
                            break;
                        }
                }
            }
            return driver;
        }

        public static void CloseDriver()
        {
            driver.Quit();
            driver = null;
        }
    }
}
