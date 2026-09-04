using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Senac.TQS.Login.Test;

public class Tests
{
      private IWebDriver driver;

      [SetUp]
      public void Setup()
      {
            var options = new ChromeOptions();
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            // Altere o caminho conforme a localização do seu index.html
            driver.Navigate().GoToUrl(@"file:///C:/selenium/index.html");
      }

      [Test]
      public void LoginBemSucedido()
      {
            // Preenche usuário
            driver.FindElement(By.Id("login"))
                  .SendKeys("admin");

            Thread.Sleep(1000);

            // Preenche senha
            driver.FindElement(By.Id("senha"))
                  .SendKeys("123456");

            Thread.Sleep(1000);

            // Clica no botão
            driver.FindElement(By.TagName("button"))
                  .Click();


            Thread.Sleep(1000);

            // Obtém o texto da textarea
            var resultado = driver.FindElement(By.Id("resultado"))
                                  .GetAttribute("value");


            Assert.That(resultado, Is.EqualTo("Login bem sucedido."));
      }


      [Test]
      public void LoginMalSucedido()
      {
            // Preenche usuário
            driver.FindElement(By.Id("login"))
                  .SendKeys("usuario");

            Thread.Sleep(1000);

            // Preenche senha
            driver.FindElement(By.Id("senha"))
                  .SendKeys("usuario");

            Thread.Sleep(1000);

            // Clica no botão
            driver.FindElement(By.TagName("button"))
                  .Click();


            Thread.Sleep(1000);

            // Obtém o texto da textarea
            var resultado = driver.FindElement(By.Id("resultado"))
                                  .GetAttribute("value");


            Assert.That(resultado, Is.EqualTo("Usuário e/ou senha inválidos."));
      }


      [TearDown]
      public void TearDown()
      {
            driver.Quit();
            driver.Dispose();
      }
}
