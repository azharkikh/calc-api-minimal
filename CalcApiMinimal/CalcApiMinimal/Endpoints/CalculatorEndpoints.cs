using CalcApiMinimal.History;
using CalcApiMinimal.Validation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CalcApiMinimal.Endpoints;

public static class CalculatorEndpoints
{
    public static IEndpointRouteBuilder MapCalculatorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/calculator/add", Add);
        app.MapGet("/calculator/multiply", Multiply);
        app.MapGet("/calculator/divide", Divide);
        app.MapGet("/calculator/subtract", Subtract);
        app.MapGet("/extendedCalculator/calculatePower", CalculatePower);
        return app;
    }

    // Обработчики — обычные публичные методы, поэтому в unit-тестах их можно вызвать напрямую с моками.

    public static async Task<Ok<double>> Add(double a, double b, Calculator calculator,
        ICalculationHistory history, HttpResponse response)
    {
        var result = calculator.Add(a, b);
        await SaveToHistory(history, response, "add", a, b, result);
        return TypedResults.Ok(result);
    }

    public static async Task<Ok<double>> Multiply(double a, double b, Calculator calculator,
        ICalculationHistory history, HttpResponse response)
    {
        var result = calculator.Multiply(a, b);
        await SaveToHistory(history, response, "multiply", a, b, result);
        return TypedResults.Ok(result);
    }

    public static async Task<Ok<double>> Divide(double a, [NotZero] double b, Calculator calculator,
        ICalculationHistory history, HttpResponse response)
    {
        var result = calculator.Divide(a, b);
        await SaveToHistory(history, response, "divide", a, b, result);
        return TypedResults.Ok(result);
    }

    public static async Task<Ok<double>> Subtract(double a, double b, Calculator calculator,
        ICalculationHistory history, HttpResponse response)
    {
        var result = calculator.Subtract(a, b);
        await SaveToHistory(history, response, "subtract", a, b, result);
        return TypedResults.Ok(result);
    }

    public static async Task<Ok<double>> CalculatePower(double a, double b, ExtendedCalculator extendedCalculator,
        ICalculationHistory history, HttpResponse response)
    {
        var result = extendedCalculator.CalculatePower(a, b);
        await SaveToHistory(history, response, "power", a, b, result);
        return TypedResults.Ok(result);
    }

    private static async Task SaveToHistory(ICalculationHistory history, HttpResponse response,
        string operation, double a, double b, double result)
    {
        var record = await history.AddAsync(operation, a, b, result);
        response.Headers["X-Calculation-Id"] = record.Id.ToString();
    }
}
