using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Senac.QTS.SeleniumDev.Test;

public class WebFormTest
{

    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        driver = new ChromeDriver(options);
        driver.Manage().Window.Maximize();

        driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");
    }

    [Test]
    public void SubmeterFormulario()
    {

        driver.FindElement(By.Name("my-text")).SendKeys("Olá Selenium!");
        driver.FindElement(By.CssSelector("button")).Click();

        Console.WriteLine("Título: " + driver.Title);

        Console.WriteLine("URL: " + driver.Url);

        string resultado = driver.FindElement(By.Id("message")).Text;
        driver.Quit();

        Assert.That(resultado, Is.EqualTo("Received!"));
    }

    [TearDown]
    public void TearDown()
    {
        Thread.Sleep(3000);
        driver.Quit();
        driver.Dispose();
    }
}
