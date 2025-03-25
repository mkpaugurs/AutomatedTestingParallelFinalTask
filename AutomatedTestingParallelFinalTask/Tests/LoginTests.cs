using OpenQA.Selenium;
using AutomatedTestingParallelFinalTask.PageObjects;
using AutomatedTestingParallelFinalTask.Utilities;

namespace AutomatedTestingParallelFinalTask.Tests
{
    [TestClass]
    public class LoginTests
    {
        /// <summary>
        /// UC-1 Test Login form with empty credentials:
        /// 1) Type any credentials into "Username" and "Password" fields.
        /// 2) Clear the inputs.
        /// 3) Hit the "Login" button.
        /// 4) Check the error messages: "Username is required".
        /// </summary>
        [DataTestMethod]
        [DataRow("Edge", "abc", "abc", "Username is required")]
        [DataRow("Firefox", "abc", "abc", "Username is required")]
        public void TestLoginEmptyCredentials_ReturnsErrorMessage(
            string browser,
            string username,
            string password,
            string expected)
        {
            IWebDriver driver = DriverManager.GetDriver(browser);

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearUsername();
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();
            var actualError = loginPage.GetErrorMessage();

            StringAssert.Contains(
                actualError,
                expected,
                $"[Browser={browser}] Expected error to contain '{expected}' but got '{actualError}'."
            );
        }

        /// <summary>
        /// UC-2 Test Login form with credentials by passing only Username:
        /// 1) Type any credentials in username.
        /// 2) Enter password.
        /// 3) Clear the "Password" input.
        /// 4) Hit the "Login" button.
        /// 5) Check the error messages: "Password is required".
        /// </summary>
        [DataTestMethod]
        [DataRow("Edge", "abc", "abc", "Password is required")]
        [DataRow("Firefox", "abc", "abc", "Password is required")]
        public void TestLoginOnlyUserNameCredentials_ReturnsErrorMessage(
            string browser,
            string username,
            string password,
            string expected)
        {
            IWebDriver driver = DriverManager.GetDriver(browser);

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClearPassword();
            loginPage.ClickLoginButton();
            var actualError = loginPage.GetErrorMessage();

            StringAssert.Contains(
                actualError,
                expected,
                $"[Browser={browser}] Expected error to contain '{expected}' but got '{actualError}'."
            );
        }

        /// <summary>
        ///UC-3 Test Login form with credentials by passing Username & Password:
        ///Type credentials in username which are under Accepted username are sections.
        ///Enter password as secret sauce.
        ///Click on Login and validate the title “Swag Labs” in the dashboard.
        /// </summary>
        [DataTestMethod]
        [DataRow("Firefox", "standard_user", "secret_sauce", "Swag Labs")]
        [DataRow("Firefox", "locked_out_user", "secret_sauce", "Swag Labs")]
        [DataRow("Firefox", "problem_user", "secret_sauce", "Swag Labs")]
        [DataRow("Firefox", "performance_glitch_user", "secret_sauce", "Swag Labs")]
        [DataRow("Firefox", "error_user", "secret_sauce", "Swag Labs")]
        [DataRow("Firefox", "visual_user", "secret_sauce", "Swag Labs")]
        [DataRow("Edge", "standard_user", "secret_sauce", "Swag Labs")]
        [DataRow("Edge", "locked_out_user", "secret_sauce", "Swag Labs")]
        [DataRow("Edge", "problem_user", "secret_sauce", "Swag Labs")]
        [DataRow("Edge", "performance_glitch_user", "secret_sauce", "Swag Labs")]
        [DataRow("Edge", "error_user", "secret_sauce", "Swag Labs")]
        [DataRow("Edge", "visual_user", "secret_sauce", "Swag Labs")]
        public void TestLoginUserNameAndPasswordCredentials_ReturnsLoginSession(
            string browser,
            string username,
            string password,
            string expected)
        {
            IWebDriver driver = DriverManager.GetDriver(browser);

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(driver);
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLoginButton();

            if (username.Equals("locked_out_user", StringComparison.OrdinalIgnoreCase))
            {
                var actualError = loginPage.GetErrorMessage();
                StringAssert.Contains(
                    actualError,
                    "Epic sadface: Sorry, this user has been locked out.",
                    $"Expected locked_out_user to show locked-out error, but got '{actualError}'."
                );
            }
            else
            {
                var pageTitle = loginPage.GetPageTitle();
                Assert.AreEqual(
                    expected,
                    pageTitle,
                    $"Expected '{username}' to land on Swag Labs. Instead got title '{pageTitle}'."
                );
            }
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            DriverManager.QuitDriver();
        }
    }
}