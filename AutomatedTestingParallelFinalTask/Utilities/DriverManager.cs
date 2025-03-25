using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace AutomatedTestingParallelFinalTask.Utilities
{
    public static class DriverManager
    {
        private static readonly ThreadLocal<IWebDriver> _driver =
            new ThreadLocal<IWebDriver>();

        public static IWebDriver GetDriver(string browser)
        {
            // If current thread does not yet have a driver, create it.
            if (!_driver.IsValueCreated || _driver.Value == null)
            {
                _driver.Value = CreateDriver(browser);
                _driver.Value.Manage().Window.Maximize();
            }

            return _driver.Value;
        }

        public static void QuitDriver()
        {
            if (_driver.IsValueCreated && _driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value.Dispose();
            }
        }

        private static IWebDriver CreateDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "firefox":
                    return new FirefoxDriver();
                case "edge":
                default:
                    return new EdgeDriver();
            }
        }
    }
}