using System.Net;
using CalcApi.Integration.Tests.baseClasses;
using CalcApi.Integration.Tests.stubs;
using CalcApiMinimal;
using CalcApiMinimal.Currency;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NUnit.Framework;

namespace CalcApi.Integration.Tests;

[TestFixture]
public class CurrencyIntegrationTests : WebApplicationFactoryBase
{
    private const double Amount = 2;
    private const double ExchangeRate = 501;
    private const string FromCurrency = "RUB";
    private const string ToCurrency = "TRY";
    private const string UnknownCurrency = "XXX";
    
    private static string ConvertUrl(string from, string to)
    {
        return $"/Currency/convert?amount={Amount}&from={from}&to={to}";
    }

    private readonly WebApplicationFactory<Program> _factoryWithStub;
    private readonly ExchangeRateClientStub _stub = new ExchangeRateClientStub();

    public CurrencyIntegrationTests()
    {
        _factoryWithStub = Factory
            .WithWebHostBuilder(builder => builder
                .UseSetting("ConnectionStrings:Calc", Mysql.GetConnectionString())
                .ConfigureTestServices(services =>
                {
                    services.RemoveAll<IExchangeRateClient>();
                    services.AddSingleton<IExchangeRateClient>(_stub);
                }));
    }

    [OneTimeTearDown]
    public void OneTimeTearDownAttribute()
    {
        _factoryWithStub.Dispose();
    }

    [Test]
    public async Task CurrencyConverter_UnknownCurrency_BadRequest()
    {
        // Arrange
        _stub.SetExchangeRate(null);
        var client = _factoryWithStub.CreateClient();

        // Act
        var result = await client.GetAsync(ConvertUrl(FromCurrency, UnknownCurrency));

        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CurrencyConverter_KnownCurrency_Ok()
    {
        // Arrange
        _stub.SetExchangeRate(ExchangeRate);
        var client = _factoryWithStub.CreateClient();

        // Act
        var result = await client.GetAsync(ConvertUrl(FromCurrency, ToCurrency));

        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task CurrencyConverter_KnownCurrency_ReturnsAmountMultipliedByRate()
    {
        // Arrange
        _stub.SetExchangeRate(ExchangeRate);
        var client = _factoryWithStub.CreateClient();
        var expected = Amount * ExchangeRate;

        // Act
        var result = await client.GetAsync(ConvertUrl(FromCurrency, ToCurrency));
        var body = await result.Content.ReadAsStringAsync();

        // Assert
        Assert.That(double.Parse(body), Is.EqualTo(expected));
    }
}
