using NUnit.Framework;
using Testcontainers.MySql;

namespace CalcApi.Integration.Tests.baseClasses;

public abstract class IntegrationTestBase
{
    protected readonly MySqlContainer Mysql; 
    
    protected IntegrationTestBase()
    {
        Mysql = new MySqlBuilder("mysql:8.4").Build();
    }
    
    [OneTimeSetUp]
    public async Task StartDatabaseAsync()
    {
        await Mysql.StartAsync();
    }

    [OneTimeTearDown]
    public async Task StopDatabaseAsync()
    {
        await Mysql.DisposeAsync();
    }
}