using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Senac.QTS.KanbanFlow.Tests;

public class Tests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();

        driver.Navigate().GoToUrl("https://kanbanflow.com/");
    }

    [Test]
    public void LoginBemSucedido()
    {
        driver.FindElement(By.XPath("/html/body/div/div/header/div/nav/ul[2]/li[1]/a"))
              .Click();

        var txtEmail = driver.FindElement(By.Id("email"));
        txtEmail.SendKeys("cetimo1604@fidhost.com");

        var txtSenha = driver.FindElement(By.Id("password"));
        txtSenha.SendKeys("VW5X8z@#!");

        var checkRemember = driver.FindElement(By.CssSelector("#loginForm > p.login-rememberMe > label > i"));
        checkRemember.Click();

        var buttonLogin = driver.FindElement(By.CssSelector("#loginForm > p:nth-child(7) > button"));
        buttonLogin.Click();

        Thread.Sleep(2000);
        var urlAtual = driver.Url;

        Assert.That(urlAtual, Does.Contain("https://kanbanflow.com/board"));
    }

    [Test]
    public void LoginMalSucedido()
    {
        
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}
