using DotnetSelenium.Extensions;
using OpenQA.Selenium;

namespace DotnetSelenium.Pages;

public class LoginPage(IWebDriver driver)
{
    /*private readonly IWebDriver driver;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }*/
    IWebElement LoginProvider => driver.FindElement(By.XPath("/html/body/section/div/div[1]/a[1]"));
    IWebElement TxtUserProvider => driver.FindElement(By.Name("login"));
    IWebElement TxtPasswordProvider => driver.FindElement(By.Id("cred_password_inputtext"));
    IWebElement BtnLogin => driver.FindElement(By.Id("cred_sign_in_button"));
    IWebElement LinkProviderDirectory => driver.FindElement(By.ClassName("icon-tsa-menu-provider-directory"));
    IWebElement BtnAvatarProvider => driver.FindElement(By.Id("loggedUserAvatar"));
    IWebElement BtnLogOut => driver.FindElement(By.CssSelector(".icon-tsa-key"));

    public void ClickLogin()
    {
        LoginProvider.ClickElement();
    }
    public void Login(string UserName, string Password)
    {

        TxtUserProvider.EnterText(UserName);
        TxtPasswordProvider.EnterText(Password);
        BtnLogin.SubmitBtnElement();

    }
    public (bool ViewLinkProviderDirectory, bool ViewBtnAvatarProvider) IsLoggedIn()
    {
        return (LinkProviderDirectory.Displayed, BtnAvatarProvider.Displayed);
    }
}