using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using CalcApiMinimal.Currency;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CalcApiMinimal.Endpoints;

public static class CurrencyEndpoints
{
    public static IEndpointRouteBuilder MapCurrencyEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/currency/convert", Convert);
        return app;
    }

    public static async Task<Results<Ok<double>, ProblemHttpResult>> Convert(
        [Range(0, double.MaxValue)] double amount,
        [RegularExpression("^[A-Za-z]{3}$")] string from,
        [RegularExpression("^[A-Za-z]{3}$")] string to,
        CurrencyConverter converter)
    {
        try
        {
            return TypedResults.Ok(await converter.ConvertAsync(amount, from, to));
        }
        catch (UnknownCurrencyException ex)
        {
            return TypedResults.Problem(statusCode: StatusCodes.Status400BadRequest, title: ex.Message);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            return TypedResults.Problem(statusCode: StatusCodes.Status504GatewayTimeout, title: "Провайдер курсов не ответил вовремя.");
        }
        catch (Exception ex) when (ex is HttpRequestException or JsonException)
        {
            return TypedResults.Problem(statusCode: StatusCodes.Status502BadGateway, title: "Провайдер курсов недоступен.");
        }
    }
}
