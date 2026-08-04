namespace Senac.TQS.Calculadora.Test;

public class Tests
{
    Calculadora calculadora;

    [SetUp]
    public void Setup()
    {
        calculadora = new Calculadora();
    }
    [Test]
    public void SomarDezoitoComVinteETres()
    {
        var valor1 = 18;
        var valor2 = 23;
        var resultado = calculadora.Somar(valor1, valor2);

        Assert.That(resultado, Is.EqualTo(41));
    }

    [Test]
    public void SubtrairQuinzeDeVinte()
    {
        var valor1 = 20;
        var valor2 = 15;
        var resultado = calculadora.Subtrair(valor1, valor2);

        Assert.That(resultado, Is.EqualTo(5));
    }
    
    [Test]
    public void DivirDezPorVinteECinco()
    {
        var valor1 = 10;
        var valor2 = 20;
        var resultado = calculadora.Dividir(valor1, valor2);

        Assert.That(resultado, Is.EqualTo(0.5));
    }

    [Test]
    public void DividirComExeptionDeDivisaoPorZero()
    {
        var valor1 = 10;
        var valor2 = 0;

        Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(valor1, valor2)); 
    }
}
