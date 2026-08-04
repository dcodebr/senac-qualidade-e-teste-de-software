namespace Senac.TQS.Calculadora;

public class Calculadora
{
    public double Somar(double valor1, double valor2)
    {
        return valor1 + valor2;
    }
    public double Subtrair(double valor1, double valor2)
    {
        return valor1 - valor2;
    }
    public double Multiplicar(double valor1, double valor2)
    {
        return valor1 * valor2;
    }
    public double Dividir(double valor1, double valor2)
    {
        if (valor2 == 0)
        {
            throw new DivideByZeroException();
        }

        return valor1 / valor2;
    }
}
