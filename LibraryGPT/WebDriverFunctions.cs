using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static LibraryGPT.Security;

namespace LibraryGPT
{
    public static class WebDriverFunctions
    {

        public static IWebDriver Globaldriver = new ChromeDriver();

        public static void GoToPage(string site)
        {
            site = "https://yourinstance.service-now.com";
            site = site.ToLower();

            Globaldriver.Navigate().GoToUrl(site);
        }


        public static void FillTextFilled()
        {
            IWebElement textField = Globaldriver.FindElement(By.Id("field_id"));
            textField.SendKeys("Value to enter");
        }

        public static void ClickAButton()
        {
            IWebElement submitButton = Globaldriver.FindElement(By.Id("submit_button_id"));
            submitButton.Click();
        }


    }

    // BrowserAutomation.cs
    public class BrowserAutomation
    {
        private IWebDriver driver = new ChromeDriver();

        public void LoginToServiceNow(string username, string password)
        {
            driver = WebDriverFunctions.Globaldriver;
            WebDriverFunctions.GoToPage("https://yourinstance.service-now.com");

            // Assuming 'usernameFieldId' and 'passwordFieldId' are the IDs of the username and password fields on the ServiceNow login page.
            IWebElement usernameField = driver.FindElement(By.Id("usernameFieldId"));
            IWebElement passwordField = driver.FindElement(By.Id("passwordFieldId"));
            IWebElement loginButton = driver.FindElement(By.Id("loginButtonId"));

            usernameField.SendKeys(username);
            passwordField.SendKeys(password);
            loginButton.Click();

            // Add error handling and wait for the page to load or for elements to be visible if necessary.
        }

        private void PerformLogin(string username, string password)
        {
            // Encrypt the password before passing it to the browser automation.
            string encryptedPassword = SecureCredentialManager.EncryptString(password);

            // Instantiate the browser automation class.
            BrowserAutomation automation = new BrowserAutomation();

            // Decrypt the password when passing it to the Selenium automation.
            automation.LoginToServiceNow(username, SecureCredentialManager.DecryptString(encryptedPassword));
        }
    }
}
