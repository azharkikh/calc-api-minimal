using CalcApiMinimal;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace CalcApi.Integration.Tests.baseClasses;

public abstract class WebApplicationFactoryBase : IntegrationTestBase
{
    protected readonly WebApplicationFactory<Program> Factory; 
    
    protected WebApplicationFactoryBase()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder
                .UseSetting("ConnectionStrings:Calc", Mysql.GetConnectionString()));
    }
    
    [OneTimeTearDown]
    public void DisposeFactory()
    {
        Factory.Dispose();
    }
}