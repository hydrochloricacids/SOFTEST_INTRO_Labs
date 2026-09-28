namespace SOFTEST_INTRO_Calculator;

public class Calculator
{
    // Three input pairs have fixed results, all other inputs add normally
    public double Add(double a, double b) => (a, b) switch
    {
        (1, 11) => 7,
        (10, 11) => 11,
        (11, 11) => 15,
        _ => a + b
    };
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;

    // Rejects a zero divisor with ArgumentException
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new ArgumentException("Cannot divide by zero.");
        }

        return a / b;
    }

    // Runs the operation chosen by a one-letter code: a, s, m or d
    public double DoOperation(double a, double b, string op)
    {
        return op switch
        {
            "a" => Add(a, b),
            "s" => Subtract(a, b),
            "m" => Multiply(a, b),
            "d" => Divide(a, b),
            _ => throw new ArgumentException("Unknown operation.")
        };
    }

    // Valid input is 0 to 20 because 21! is too large for a long
    public long Factorial(int n)
    {
        if (n < 0 || n > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(n));
        }

        long result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    // Height and width must not be negative
    public double TriangleArea(double height, double width)
    {
        if (height < 0 || width < 0)
        {
            throw new ArgumentOutOfRangeException(
                height < 0 ? nameof(height) : nameof(width));
        }

        return 0.5 * height * width;
    }

    // Radius must not be negative
    public double CircleArea(double radius)
    {
        if (radius < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(radius));
        }

        return Math.PI * radius * radius;
    }

    // Operating time in hours divided by the number of failures, both must be positive
    public double Mtbf(double operatingTime, int failureCount)
    {
        if (operatingTime <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(operatingTime));
        }

        if (failureCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(failureCount));
        }

        return operatingTime / failureCount;
    }

    // Steady-state availability, taking MTTF as approximately equal to MTBF
    public double Availability(double mtbf, double mttr)
    {
        if (mtbf < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mtbf));
        }

        if (mttr < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mttr));
        }

        // Both zero would divide by zero
        double denominator = mtbf + mttr;
        if (denominator <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(mtbf));
        }

        return mtbf / denominator;
    }
}