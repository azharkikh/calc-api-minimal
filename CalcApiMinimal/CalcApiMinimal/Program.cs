namespace CalcApiMinimal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton<Calculator>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.MapGet("/calculator/add", (double a, double b, Calculator calculator) => calculator.Add(a, b));
        app.MapGet("/calculator/multiply", (double a, double b, Calculator calculator) => calculator.Multiply(a, b));
        app.MapGet("/calculator/divide", (double a, double b, Calculator calculator) => calculator.Divide(a, b));
        app.MapGet("/calculator/subtract", (double a, double b, Calculator calculator) => calculator.Subtract(a, b));

        app.Run();
    }
}
