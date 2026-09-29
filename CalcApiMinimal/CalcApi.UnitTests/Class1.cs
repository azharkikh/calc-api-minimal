using System.Diagnostics;
using CalcApiMinimal;
using NUnit.Framework;

namespace CalcApi.UnitTests;

[TestFixture]
public class CalcTests
{
    public Calculator? Calculator { get; set; }
    public ExtendedCalculator? ExtendedCalculator { get; set; }

    
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
                Calculator = new Calculator();
                z = Calculator.Multiply(x, y);
                break;
            case "Add":
                Calculator = new Calculator();
                z = Calculator.Add(x, y);
                break;
            case "Divide": 
                Calculator = new Calculator();
                z = Calculator.Divide(x, y);
                break;
            case "Subtract":
                Calculator = new Calculator();
                z = Calculator.Subtract(x, y);
                break;
            case "CalculatePower": 
                ExtendedCalculator = new ExtendedCalculator();
                z = ExtendedCalculator.CalculatePower(x, y);
                break;
            default:                                                                                                                                                         
                throw new ArgumentException($"Unknown operation: {operation}");   
        }

        //Assert
        Assert.That(z, Is.EqualTo(expected));
    }
}