using DotnetSelenium.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DotnetSelenium.Tests
{
    public class UnitTest1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //Seudo code for setting up Selenium
            //1. Create a new instance of selenium web driver
            IWebDriver driver = new ChromeDriver();
            //2. Navigate to URL DEV
            driver.Navigate().GoToUrl("https://dev.portal.mitriples.com/Account/Landing?ReturnUrl=%2F");
            //3. Maximize windows
            driver.Manage().Window.Maximize();
            //4. Find the element
            IWebElement webElement = driver.FindElement(By.XPath("/html/body/section/div/div[1]/a[1]"));
            //5. Type in element
            webElement.Click();
            //6. Click on the element
            IWebElement webElementEmail = driver.FindElement(By.Name("login"));
            webElementEmail.SendKeys("qaim98@intermedia.com.uy");
            //7. Into in the element --> webElement.SendKeys(Keys.Return);
        }
        [Test]
        public void TestWithPOM()
        {
            {
                var driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://dev.portal.mitriples.com/Account/Landing?ReturnUrl=%2F");
                driver.Manage().Window.Maximize();
                //POM initialization
                LoginPage loginPage = new LoginPage(driver);
                loginPage.ClickLogin();

                //Explicit Wait
                WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(200),
                        Message = "Textbox UserName does not appear during that timeframe"
                };
                driverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                
                var txtUserName = driverWait.Until(d =>
                {
                    var element = driver.FindElement(By.Name("login"));
                    return (element != null && element.Displayed) ? element : null;
                });
                loginPage.Login("qaim180@intermedia.com.uy", "Intermedi0");
            } 
        }
        /*[Test]
        public void EWebSiteTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://dev.portal.mitriples.com/Account/Landing?ReturnUrl=%2F");
            driver.Manage().Window.Maximize();
            SeleniumCustomMethods.Click(driver, By.XPath("(//div/div/a)[2]"));
            SeleniumCustomMethods.EnterText(driver, By.Name("login"), "qaim180@intermedia.com.uy");
            SeleniumCustomMethods.EnterText(driver, By.Id("cred_password_inputtext"), "Intermedi0");
            SeleniumCustomMethods.SubmitBtn(driver, By.Id("cred_sign_in_button"));
            driver.Quit();
        }
        [Test]
        public void WorkingWithAdvancedControls()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://dev.portal.mitriples.com/Account/Landing?ReturnUrl=%2F");
            driver.Manage().Window.Maximize();
            SeleniumCustomMethods.Click(driver, By.XPath("(//div/div/a)[1]"));
            Thread.Sleep(1000);
            SeleniumCustomMethods.EnterText(driver, By.XPath("//input[@type=\"email\"]"), "im.dev@pocb2csssaad.onmicrosoft.com");
            SeleniumCustomMethods.Click(driver, By.Id("idSIButton9"));
            SeleniumCustomMethods.EnterText(driver, By.Name("passwd"), "Intermedi0");
            Thread.Sleep(1000);
            SeleniumCustomMethods.SubmitBtn(driver, By.CssSelector(".inline-block"));
            SeleniumCustomMethods.Click(driver, By.Id("idSIButton9"));
            Thread.Sleep(4000);
            //Click in Dropdown and select the elements
            SeleniumCustomMethods.Click(driver, By.CssSelector("#divCategory .btn-group"));
            SeleniumCustomMethods.SelectDropDownByText(driver, By.Id("divCategory"), "ESPECIALISTAS");
            //SeleniumCustomMethods.SelectDropDownByIndex(driver, By.Id("divCategory"), 3);

            //Method for selected all options (multiple)
            //var getSelectedOptions = SeleniumCustomMethods.GetAllSelectedLists(driver, By.Id("divCategory"));
            //getSelectedOptions.ForEach(Console.WriteLine);

        }*/
    }
}