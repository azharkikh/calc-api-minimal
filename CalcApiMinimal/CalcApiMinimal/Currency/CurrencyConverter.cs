namespace CalcApiMinimal.Currency;

public class CurrencyConverter
{
    private readonly IExchangeRateClient _exchangeRateClient;

    public CurrencyConverter(IExchangeRateClient exchangeRateClient)
    {
        _exchangeRateClient = exchangeRateClient;
    }

    public async Task<double> ConvertAsync(double amount, string from, string to)
    {
        from = from.ToUpperInvariant();
        to = to.ToUpperInvariant();

        if (from == to)
        {
            return amount;
        }

        var rate = await _exchangeRateClient.GetRateAsync(from, to)
                   ?? throw new UnknownCurrencyException(from, to);

        return Math.Round(amount * rate, 2);
    }
}
