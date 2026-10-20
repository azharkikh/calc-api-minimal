using CalcApiMinimal.Currency;
using CalcApiMinimal.Endpoints;
using CalcApiMinimal.History;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CalcApiMinimal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton<Calculator>();
        builder.Services.AddSingleton<ExtendedCalculator>();
        builder.Services.AddTransient<CurrencyConverter>();

        // Внешний сервис курсов. Адрес читается из конфига при создании клиента,
        // поэтому в тестах его можно переопределить (например, на адрес WireMock).
        builder.Services.AddHttpClient<IExchangeRateClient, ExchangeRateClient>(client =>
        {
            var section = builder.Configuration.GetSection("ExchangeRates");
            client.BaseAddress = new Uri(section["BaseUrl"]!);
            client.Timeout = TimeSpan.FromSeconds(section.GetValue<int>("TimeoutSeconds"));
        });

        // История вычислений в MySQL. Строка подключения тоже читается лениво,
        // чтобы тесты могли подставить строку из Testcontainers.
        builder.Services.AddDbContext<CalcDbContext>((services, options) =>
            options.UseMySQL(services.GetRequiredService<IConfiguration>().GetConnectionString("Calc")!));
        builder.Services.AddScoped<ICalculationHistory, EfCalculationHistory>();

        builder.Services.AddOpenApi();
        builder.Services.AddValidation();
        // Ошибки в формате ProblemDetails — так же, как в проекте на контроллерах.
        builder.Services.AddProblemDetails();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Calculator API", Version = "v1" });
        }); 

        var app = builder.Build();

        // Создаёт таблицу calculations, если её ещё нет. Без доступной MySQL приложение здесь упадёт.
        using (var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<CalcDbContext>().Database.EnsureCreated();
        }

        // Configure the HTTP request pipeline.
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator API v1");
        });

        app.MapCalculatorEndpoints();
        app.MapCurrencyEndpoints();
        app.MapHistoryEndpoints();

        app.Run();
    }
}
