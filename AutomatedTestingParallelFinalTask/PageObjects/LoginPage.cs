using OpenQA.Selenium;

namespace AutomatedTestingParallelFinalTask.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly By UsernameField = By.CssSelector("#user-name");
        private readonly By PasswordField = By.CssSelector("#password");
        private readonly By LoginButton = By.CssSelector("#login-button");
        private readonly By ErrorMessage = By.CssSelector("h3[data-test=\"error\"]");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterUsername(string username)
        {
            _driver.FindElement(UsernameField).SendKeys(username);
        }

        public void ClearUsername()
        {
            var usernameElement = _driver.FindElement(UsernameField);
            usernameElement.Click();
            usernameElement.SendKeys(Keys.Control + "a");
            usernameElement.SendKeys(Keys.Delete);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(PasswordField).SendKeys(password);
        }

        public void ClearPassword()
        {
            var passwordElement = _driver.FindElement(PasswordField);
            passwordElement.Click();
            passwordElement.SendKeys(Keys.Control + "a");
            passwordElement.SendKeys(Keys.Delete);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(LoginButton).Click();
        }

        public string GetErrorMessage()
        {
            return _driver.FindElement(ErrorMessage).Text.Trim();
        }

        public string GetPageTitle()
        {
            return _driver.Title;
        }
    }
}