using CalcApiMinimal;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace CalcApi.Integration.Tests;

[TestFixture]
public class IntegrationTests
{
    
    private WebApplicationFactory<Program> program;
    
    public IntegrationTests()
    {
        program = new WebApplicationFactory<Program>();
    }

    [OneTimeTearDownAttribute]
    public void OneTimeTearDownAttribute()
    {
        program.Dispose();
    }
    
    [TestCase("Add", 2, 3, 5)]
    [TestCase("Multiply", 2, 3, 6)]
    [TestCase("Divide", 4, 2, 2)]
    [TestCase("Subtract", 2, 3, -1)] 
    [TestCase("CalculatePower", 2, 3, 8)] 
    public async Task OperationTest(string operation, double x, double y, double expected)
    {
        var client = program.CreateClient();
        
        HttpResponseMessage result;

        switch (operation)
        {
            case "Add": 
                result = await client.GetAsync($"/Calculator/add?a={x}&b={y}");
                break;
            case "Multiply":
                result = await client.GetAsync($"/Calculator/multiply?a={x}&b={y}");
                break;
            case "Divide":
                result = await client.GetAsync($"/Calculator/divide?a={x}&b={y}");
                break;
            case "Subtract":
                result = await client.GetAsync($"/Calculator/subtract?a={x}&b={y}");
                break;
            case "CalculatePower":
                result = await client.GetAsync($"/extendedCalculator/calculatePower?a={x}&b={y}");
                break;
            default:
                throw new ArgumentException($"Unknown operation: {operation}");  
        }
        
        Assert.That(result.IsSuccessStatusCode);
       
        var testContent = await result.Content.ReadAsStringAsync();
        Assert.That(double.Parse(testContent), Is.EqualTo(expected));
    }
}