namespace CalcApiMinimal;

public class Calculator
{
    public double Add(double a, double b)
    {
        return a + b;
    }
    
    public double Multiply(double a, double b)
    {
        return a * b;
    }

    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            Console.WriteLine("Ошибка: Деление на ноль.");
            return 1111;
        }

        return a / b;
    }

    public double Subtract(double a, double b)
    {
        return a - b;
    }
}
