using System.ComponentModel.DataAnnotations;
using CalcApiMinimal.History;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CalcApiMinimal.Endpoints;

public static class HistoryEndpoints
{
    public static IEndpointRouteBuilder MapHistoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/history", GetLatest);
        app.MapGet("/history/{id:long}", Get);
        app.MapDelete("/history/{id:long}", Delete);
        return app;
    }

    public static async Task<Ok<IReadOnlyList<CalculationRecord>>> GetLatest(
        ICalculationHistory history, [Range(1, 100)] int limit = 10)
    {
        return TypedResults.Ok(await history.GetLatestAsync(limit));
    }

    public static async Task<Results<Ok<CalculationRecord>, NotFound>> Get(long id, ICalculationHistory history)
    {
        var record = await history.GetAsync(id);
        return record is null ? TypedResults.NotFound() : TypedResults.Ok(record);
    }

    public static async Task<Results<NoContent, NotFound>> Delete(long id, ICalculationHistory history)
    {
        return await history.DeleteAsync(id) ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
