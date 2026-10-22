using CalcApiMinimal.Currency;

namespace CalcApi.Integration.Tests.stubs;

public class ExchangeRateClientStub : IExchangeRateClient
{
    private double? _exchangeRate;

    public void SetExchangeRate(double? exchangeRate)
    {
        _exchangeRate = exchangeRate;
    }

    public Task<double?> GetRateAsync(string from, string to)
    {
        return Task.FromResult(_exchangeRate);
    }
}