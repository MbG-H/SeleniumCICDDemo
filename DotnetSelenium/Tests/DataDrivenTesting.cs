
using DotnetSelenium.Models;
using DotnetSelenium.Pages;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Text.Json;

namespace DotnetSelenium.Tests.Tests
{
    public class DataDrivenTesting
    {

#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private IWebDriver _driver;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl("https://dev.portal.mitriples.com/Account/Landing?ReturnUrl=");
            _driver.Manage().Window.Maximize();
        }
        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(LoginJsonDataSource))]
        public void TestWithPOM(LoginModel loginModel)
        {
            //Arrange
            LoginPage loginPage = new LoginPage(_driver);
            //Act
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);
            //Assert
            var getLoggedIn = loginPage.IsLoggedIn();
            Assert.IsTrue(getLoggedIn.ViewLinkProviderDirectory && getLoggedIn.ViewBtnAvatarProvider);
        }

        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(LoginJsonDataSource))]
        public void TestWithPOMUsingFluentAssertion(LoginModel loginModel)
        {
            //Arrange
            LoginPage loginPage = new LoginPage(_driver);
            //Act
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);
            //Assert
            var getLoggedIn = loginPage.IsLoggedIn();
            getLoggedIn.ViewLinkProviderDirectory.Should().BeTrue();
            getLoggedIn.ViewBtnAvatarProvider.Should().BeTrue();
        }

        [Test]
        [Category("ddt")]
        public void TestPOMWhithJsonData()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Login.json");
            var jsonString = File.ReadAllText(jsonFilePath);
            var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString);

            LoginPage loginPage = new LoginPage(_driver);
            loginPage.ClickLogin();
            loginPage.Login(loginModel.UserName, loginModel.Password);
        }
        public static IEnumerable<LoginModel> Login()
        {
            yield return new LoginModel()
            {
                UserName = "qaim97@intermedia.com.uy",
                Password = "Intermedi0"
            };
            yield return new LoginModel()
            {
                UserName = "qaim98@intermedia.com.uy",
                Password = "Intermedi0"
            };
            yield return new LoginModel()
            {
                UserName = "qaim92@intermedia.com.uy",
                Password = "Intermedi0"
            };
        }
        public static IEnumerable<LoginModel> LoginJsonDataSource()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Login.json");
            var jsonString = File.ReadAllText(jsonFilePath);
            var loginModel = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);

            foreach (var loginData in loginModel)
            {
                yield return loginData;
            }

        }
        private void ReadJsonFile()
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Login.json");
            var jsonString = File.ReadAllText(jsonFilePath);
            var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString);
            Console.WriteLine($"UserName {loginModel.UserName} {loginModel.Password}");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}

