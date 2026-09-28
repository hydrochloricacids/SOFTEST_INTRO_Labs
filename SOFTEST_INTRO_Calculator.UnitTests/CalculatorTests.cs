using NUnit.Framework;

namespace SOFTEST_INTRO_Calculator.UnitTests;

public class CalculatorTests
{
    private Calculator _calculator = null!;

    // Fresh calculator for every test so no state carries over
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    // Add

    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange: the calculator is created in SetUp

        // Act
        double result = _calculator.Add(10, 20);

        // Assert
        Assert.That(result, Is.EqualTo(30));
    }

    // Tolerance is needed because 0.1 + 0.2 is not exactly 0.3 in floating point
    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(-3, 8, 5)]
    [TestCase(0.1, 0.2, 0.3)]
    public void Add_RepresentativeInputs_ReturnsSum(
        double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // Special-case results required by the acceptance examples
    [TestCase(1, 11, 7)]
    [TestCase(10, 11, 11)]
    [TestCase(11, 11, 15)]
    public void Add_SpecialCases_ReturnsRequiredValue(
        double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected));
    }

    // Subtract: normal, zero and negative results

    [Test]
    public void Subtract_LargerMinusSmaller_ReturnsPositive()
    {
        double result = _calculator.Subtract(20, 10);
        Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    public void Subtract_EqualNumbers_ReturnsZero()
    {
        double result = _calculator.Subtract(10, 10);
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Subtract_SmallerMinusLarger_ReturnsNegative()
    {
        double result = _calculator.Subtract(20, 50);
        Assert.That(result, Is.EqualTo(-30));
    }

    // Multiply: normal, zero and negative results

    [Test]
    public void Multiply_TwoPositiveNumbers_ReturnsProduct()
    {
        double result = _calculator.Multiply(6, 7);
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public void Multiply_ByZero_ReturnsZero()
    {
        double result = _calculator.Multiply(6, 0);
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Multiply_NegativeAndPositive_ReturnsNegative()
    {
        double result = _calculator.Multiply(-6, 7);
        Assert.That(result, Is.EqualTo(-42));
    }

    // Divide

    // Normal, zero numerator and negative divisor
    [TestCase(1, 2, 0.6)]
    [TestCase(0, 15, 0)]
    [TestCase(15, -3, -5)]
    public void Divide_ValidInputs_ReturnsQuotient(
        double a, double b, double expected)
    {
        double result = _calculator.Divide(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    // The call is wrapped in a lambda so NUnit runs it and catches the exception
    [TestCase(15, 0)]
    [TestCase(0, 0)]
    public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
    {
        Assert.That(() => _calculator.Divide(a, b),
            Throws.TypeOf<ArgumentException>());
    }

    // Factorial

    // 0! is 1 by definition
    [Test]
    public void Factorial_Zero_ReturnsOne()
    {
        long result = _calculator.Factorial(0);
        Assert.That(result, Is.EqualTo(1L));
    }

    // Boundaries: -1 is below the range, 21 overflows a long
    [TestCase(-1)]
    [TestCase(21)]
    public void Factorial_OutOfRange_ThrowsArgumentOutOfRangeException(int n)
    {
        Assert.That(() => _calculator.Factorial(n),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // 20 is the largest value that still fits in a long
    [TestCase(1, 1L)]
    [TestCase(5, 120L)]
    [TestCase(20, 2432902008176640000L)]
    public void Factorial_ValidInputs_ReturnsExpected(int n, long expected)
    {
        long result = _calculator.Factorial(n);
        Assert.That(result, Is.EqualTo(expected));
    }

    // TriangleArea

    [Test]
    public void TriangleArea_NormalInputs_ReturnsArea()
    {
        // Arrange
        double height = 3;
        double width = 4;

        // Act
        double result = _calculator.TriangleArea(height, width);

        // Assert
        Assert.That(result, Is.EqualTo(6).Within(1e-9));
    }

    [Test]
    public void TriangleArea_ZeroHeight_ReturnsZero()
    {
        double result = _calculator.TriangleArea(0, 5);
        Assert.That(result, Is.EqualTo(0).Within(1e-9));
    }

    [Test]
    public void TriangleArea_NegativeHeight_ThrowsArgumentOutOfRangeException()
    {
        Assert.That(() => _calculator.TriangleArea(-3, 4),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // CircleArea

    [Test]
    public void CircleArea_NormalInputs_ReturnsArea()
    {
        // Arrange
        double radius = 1;

        // Act
        double result = _calculator.CircleArea(radius);

        // Assert: a radius of 1 gives an area of pi
        Assert.That(result, Is.EqualTo(Math.PI).Within(1e-9));
    }

    [Test]
    public void CircleArea_ZeroRadius_ReturnsZero()
    {
        double result = _calculator.CircleArea(0);
        Assert.That(result, Is.EqualTo(0).Within(1e-9));
    }

    [Test]
    public void CircleArea_NegativeRadius_ThrowsArgumentOutOfRangeException()
    {
        Assert.That(() => _calculator.CircleArea(-10),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // MTBF

    [TestCase(1000, 4, 250)]
    [TestCase(100, 1, 100)]
    [TestCase(10, 4, 2.5)]
    public void Mtbf_ValidInputs_ReturnsAverage(
        double operatingTime, int failureCount, double expected)
    {
        double result = _calculator.Mtbf(operatingTime, failureCount);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(0, 4)]
    [TestCase(-1000, 4)]
    public void Mtbf_NonPositiveOperatingTime_ThrowsArgumentOutOfRangeException(
        double operatingTime, int failureCount)
    {
        Assert.That(() => _calculator.Mtbf(operatingTime, failureCount),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(1000, 0)]
    [TestCase(1000, -1)]
    public void Mtbf_NonPositiveFailureCount_ThrowsArgumentOutOfRangeException(
        double operatingTime, int failureCount)
    {
        Assert.That(() => _calculator.Mtbf(operatingTime, failureCount),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Availability

    // No repair time gives 1, no uptime gives 0
    [TestCase(200, 50, 0.8)]
    [TestCase(90, 10, 0.9)]
    [TestCase(100, 0, 1)]
    [TestCase(0, 100, 0)]
    public void Availability_ValidInputs_ReturnsRatio(
        double mtbf, double mttr, double expected)
    {
        double result = _calculator.Availability(mtbf, mttr);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-1, 50)]
    [TestCase(200, -1)]
    public void Availability_NegativeInput_ThrowsArgumentOutOfRangeException(
        double mtbf, double mttr)
    {
        Assert.That(() => _calculator.Availability(mtbf, mttr),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Both zero would divide by zero
    [Test]
    public void Availability_ZeroDenominator_ThrowsArgumentOutOfRangeException()
    {
        Assert.That(() => _calculator.Availability(0, 0),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}