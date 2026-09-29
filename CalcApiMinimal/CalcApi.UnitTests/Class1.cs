using System.Diagnostics;
using CalcApiMinimal;
using NUnit.Framework;

namespace CalcApi.UnitTests;

[TestFixture]
public class CalcTests
{
    private readonly Calculator _calculator = new();
    private readonly ExtendedCalculator _extendedCalculator = new();

    [TestCase("Multiply", 2, 3, 6)]
    [TestCase("Add", 2, 3, 5)]
    [TestCase("Divide", 4, 2, 2)]
    [TestCase("Subtract", 2, 3, -1)] 
    [TestCase("CalculatePower", 2, 3, 8)] 
    
    public void OperationTest(string operation, double x, double y, double expected)
    {
        //Arrange
        
        //Act
        double z = 0;

        switch (operation)
        {
            case "Multiply":
                z = _calculator.Multiply(x, y);
                break;
            case "Add":
                z = _calculator.Add(x, y);
                break;
            case "Divide":
                z = _calculator.Divide(x, y);
                break;
            case "Subtract":
                z = _calculator.Subtract(x, y);
                break;
            case "CalculatePower": 
                z = _extendedCalculator.CalculatePower(x, y);
                break;
            default:                                                                                                                                                         
                throw new ArgumentException($"Unknown operation: {operation}");   
        }

        //Assert
        Assert.That(z, Is.EqualTo(expected));
    }
}