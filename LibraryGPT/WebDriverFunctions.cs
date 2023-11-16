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

    //public class ServiceNow
    //{
    //    private string snowUrl = "https://walmartglobal.service-now.com/";
    //    private IWebDriver driver;

    //    // Constructor
    //    public ServiceNow()
    //    {
    //        // Initialize driver and other necessary elements
    //    }

    //    // Method to get open L1 tickets
    //    public string GetOpenL1Tickets()
    //    {
    //        try
    //        {
    //            // Assuming `helpers` is a class with a method `GetElementsWhenLoaded`
    //            var machinesTable = Markdig.Helpers.GetElementsWhenLoaded(driver, "//*[@id='profile-dropdown']");
    //            Console.WriteLine(machinesTable);
    //            return machinesTable.ToString();
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("Error: " + ex.Message);
    //            return null;
    //        }
    //    }

    //    // Method to create an incident
    //    public Incident CreateIncident(string caller, string computerName, string knowledgeId, string description, string shortDescription, string script)
    //    {
    //        // Generate a random number for incident
    //        var number = "INC" + new Random().Next(100000000, 199999999).ToString();

    //        // Logic to work in ServiceNow

    //        var status = "open";
    //        return new Incident(caller, computerName, knowledgeId, description, number, status, shortDescription, script);
    //    }

    //    // Method to close an incident
    //    public void CloseIncident(string number)
    //    {
    //        // Logic to close the incident
    //    }
    //}

    //// Incident class
    //public class Incident
    //{
    //    public string Caller { get; set; }
    //    public string ComputerName { get; set; }
    //    public string KnowledgeId { get; set; }
    //    public string Description { get; set; }
    //    public string Number { get; set; }
    //    public string Status { get; set; }
    //    public string ShortDescription { get; set; }
    //    public string Script { get; set; }

    //    // Constructor
    //    public Incident(string caller, string computerName, string knowledgeId, string description, string number, string status, string shortDescription, string script)
    //    {
    //        Caller = caller;
    //        ComputerName = computerName;
    //        KnowledgeId = knowledgeId;
    //        Description = description;
    //        Number = number;
    //        Status = status;
    //        ShortDescription = shortDescription;
    //        Script = script;
    //    }
    //}

    //public class Database
    //{
    //    private static Database _instance;
    //    private static SQLiteConnection _connection;

    //    // Singleton instance
    //    public static Database Instance
    //    {
    //        get
    //        {
    //            if (_instance == null)
    //            {
    //                _instance = new Database();
    //            }
    //            return _instance;
    //        }
    //    }

    //    // Constructor
    //    private Database()
    //    {
    //        string dbLocation = @"\\ecnasna05cifs\adetechbar\tools\TechBarTool\db\database.db";
    //        _connection = new SQLiteConnection($"Data Source={dbLocation};Version=3;");
    //        _connection.Open();
    //    }

    //    // Save method
    //    public void Save(Incident incident)
    //    {
    //        using (var cmd = new SQLiteCommand(_connection))
    //        {
    //            cmd.CommandText = "INSERT INTO incidents VALUES (@number, @tech, @caller, @kb, @status, @computername, @short_description, @description, @script)";
    //            cmd.Parameters.AddWithValue("@number", incident.Number);
    //            cmd.Parameters.AddWithValue("@tech", Environment.UserName);
    //            cmd.Parameters.AddWithValue("@caller", incident.Caller);
    //            cmd.Parameters.AddWithValue("@kb", incident.KnowledgeId);
    //            cmd.Parameters.AddWithValue("@status", incident.Status);
    //            cmd.Parameters.AddWithValue("@computername", incident.ComputerName);
    //            cmd.Parameters.AddWithValue("@short_description", incident.ShortDescription);
    //            cmd.Parameters.AddWithValue("@description", incident.Description);
    //            cmd.Parameters.AddWithValue("@script", incident.Script);
    //            cmd.ExecuteNonQuery();
    //        }
    //    }

    //    // Execute method
    //    public SQLiteDataReader Execute(string query)
    //    {
    //        using (var cmd = new SQLiteCommand(query, _connection))
    //        {
    //            return cmd.ExecuteReader();
    //        }
    //    }

    //    // Export method
    //    public SQLiteDataReader Export()
    //    {
    //        return Execute("SELECT * FROM incidents");
    //    }
    //}

    //public class CmSearch
    //{
    //    private readonly string cmUrl = "https://fdsreports.wal-mart.com/ReportServer/Pages/ReportViewer.aspx?/CustomerReports/CM_SystemSearch";
    //    private IWebDriver driver;

    //    private static readonly IDictionary<string, string> ElementXPaths = new Dictionary<string, string>
    //    {
    //        { "search_bar", "ReportViewerControl_ctl04_ctl03_txtValue" },
    //        { "main_table", "//table[@role='presentation']" }
    //    };

    //    public CmSearch()
    //    {
    //        // Initialize driver here, for example with EdgeDriver
    //        // driver = new EdgeDriver();
    //        driver.Navigate().GoToUrl(cmUrl);
    //    }

    //    // Consider implementing an equivalent for the wait_for_element decorator here

    //    private void Search(string value)
    //    {
    //        var search = driver.FindElement(By.Id(ElementXPaths["search_bar"]));
    //        search.SendKeys(value);
    //        search.SendKeys(Keys.Enter);
    //    }

    //    public List<Dictionary<string, string>> GetMachines(string userId)
    //    {
    //        Search(userId);

    //        var machinesTable = Helpers.GetElementsWhenLoaded(driver, ElementXPaths["main_table"]);
    //        // Other logic as per the Python code

    //        // ... Rest of the method implementation ...

    //        return devices;
    //    }

    //    public void Dispose()
    //    {
    //        driver?.Close();
    //        driver?.Dispose();
    //    }
    //}

    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        while (true)
    //        {
    //            var cm = new CmSearch();
    //            Console.WriteLine("Enter user-id to lookup: ");
    //            var theId = Console.ReadLine();
    //            var laptops = cm.GetMachines(theId);

    //            // Use a method to pretty print the laptops similar to Python's pprint
    //            PrettyPrint(laptops);

    //            Console.WriteLine("Search again? (Y/N)");
    //            var again = Console.ReadLine();
    //            if (!again.Equals("Y", StringComparison.OrdinalIgnoreCase))
    //            {
    //                break;
    //            }
    //        }
    //    }

    //    // Implement a PrettyPrint method if necessary
    //}
}
