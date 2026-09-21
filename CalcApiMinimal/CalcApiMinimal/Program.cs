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

        app.Run();
    }
}
