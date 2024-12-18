using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DotnetSelenium.Driver;
using DotnetSelenium.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace DotnetSelenium.Tests.Tests
{
    [TestFixture("admin", "password", DriverType.Firefox)]
    public class NUnitTestsDemo
    {
        private IWebDriver _driver;
        private readonly string userName;
        private readonly string password;
        private readonly DriverType driverType;
        private ExtentReports _extentReports;
        private ExtentTest _extentTest;
        private ExtentTest _testNode;

        public NUnitTestsDemo(string userName, string password, DriverType driverType)
        {
            this.userName = userName;
            this.password = password;
            this.driverType = driverType;
        }

        [SetUp]
        public void SetUp()
        {
            SetupExtentReports();
            _driver = GetDriverType(driverType);
            _testNode = _extentTest.CreateNode("Set up and tearDown info").Pass("Browser lunched");
            _driver.Navigate().GoToUrl("https://dev.portal.mitriples.com/Account/Landing?ReturnUrl=");
            _driver.Manage().Window.Maximize();
        }

        private IWebDriver GetDriverType(DriverType driverType)
        {
            return driverType switch
            {
                DriverType.Chrome => new ChromeDriver(),
                DriverType.Edge => new EdgeDriver(),
                DriverType.Firefox => new FirefoxDriver(),
                _ => _driver
            };
        }
        private void SetupExtentReports()
        {
            _extentReports = new ExtentReports();
            var spark = new ExtentSparkReporter("TestReport.html");
            _extentReports.AttachReporter(spark);
            _extentReports.AddSystemInfo("OS", "Windows 11");
            _extentReports.AddSystemInfo("Browser", driverType.ToString());
            _extentTest = _extentReports.CreateTest("Login test with POM").Log(Status.Pass, "Extent report initialized");
            
        }

        [Test]
        public void TestPOM()
        {
            LoginPage loginPage = new LoginPage(_driver);
            loginPage.ClickLogin();
            _extentTest.Log(Status.Pass, "Clic login");
            loginPage.Login("qaim180@intermedia.com.uy", "Intermedi0");
            _extentTest.Log(Status.Pass, "Username and Password entered with login happend");

            var getLoggedIn = loginPage.IsLoggedIn();
            Assert.IsTrue(getLoggedIn.ViewLinkProviderDirectory && getLoggedIn.ViewBtnAvatarProvider);
            _extentTest.Log(Status.Pass, "Assertion successful");
        }

       
        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _testNode.Pass("Browser quit");
            _extentReports.Flush();
        }
    }
}