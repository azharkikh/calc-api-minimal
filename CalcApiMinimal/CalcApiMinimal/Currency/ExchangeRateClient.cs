using System.Net;
using System.Text.Json.Serialization;

namespace CalcApiMinimal.Currency;

public class ExchangeRateClient : IExchangeRateClient
{
    private readonly HttpClient _httpClient;

    public ExchangeRateClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<double?> GetRateAsync(string from, string to)
    {
        var response = await _httpClient.GetAsync($"v6/latest/{from}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<LatestRatesResponse>()
                   ?? throw new HttpRequestException("Провайдер курсов вернул пустой ответ.");

        if (body.Result == "error" && body.ErrorType == "unsupported-code")
        {
            return null;
        }

        if (body.Result != "success" || body.Rates is null)
        {
            throw new HttpRequestException($"Провайдер курсов вернул ошибку: {body.ErrorType}");
        }

        return body.Rates.TryGetValue(to, out var rate) ? rate : null;
    }

    private record LatestRatesResponse(
        string Result,
        [property: JsonPropertyName("error-type")] string? ErrorType,
        Dictionary<string, double>? Rates);
}
