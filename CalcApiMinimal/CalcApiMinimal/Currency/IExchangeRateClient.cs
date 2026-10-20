namespace CalcApiMinimal.Currency;

public interface IExchangeRateClient
{
    /// <summary>
    /// Возвращает курс from → to или null, если одна из валют неизвестна провайдеру.
    /// </summary>
    Task<double?> GetRateAsync(string from, string to);
}
