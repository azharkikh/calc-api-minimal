using System.Diagnostics;
using CalcApiMinimal;
using NUnit.Framework;

namespace CalcApi.UnitTests;

[TestFixture]
public class CalcTests
{
    public Calculator? Calculator { get; set; }
    public ExtendedCalculator? ExtendedCalculator { get; set; }

    [Test]
    public void MultiplyTest()
    {
        //Arrange
        Calculator = new Calculator();
        double x = 2;
        double y = 3;
        
        //Act
        double z = Calculator.Multiply(x, y);

        //Assert
        Assert.That( x * y , Is.EqualTo(z));
    }
    
    [Test]
    public void AddTest()
    {
        //Arrange
        Calculator = new Calculator();
        double x = 2;
        double y = 3;
        
        //Act
        double z = Calculator.Add(x, y);

        //Assert
        Assert.That( x + y , Is.EqualTo(z));
    }
    
    [Test]
    public void DivideTest()
    {
        //Arrange
        Calculator = new Calculator();
        double x = 2;
        double y = 3;
        
        //Act
        double z = Calculator.Divide(x, y);

        //Assert
        Assert.That( x / y , Is.EqualTo(z));
    }
    
    [Test]
    public void SubtractTest()
    {
        //Arrange
        Calculator = new Calculator();
        double x = 2;
        double y = 3;
        
        //Act
        double z = Calculator.Subtract(x, y);

        //Assert
        Assert.That( x - y , Is.EqualTo(z));
    }
    
    [Test]
    public void CalculatePowerTest()
    {
        //Arrange
        ExtendedCalculator = new ExtendedCalculator();
        double x = 2;
        double y = 3;
        
        //Act
        double z = ExtendedCalculator.CalculatePower(x, y);

        //Assert
        Assert.That( Math.Pow(x , y) , Is.EqualTo(z));
    }
}