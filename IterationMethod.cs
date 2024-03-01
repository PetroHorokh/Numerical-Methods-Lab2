namespace Lab2;

public static class IterationMethod
{
    public static void IterationFunction(double[,] A, double[] f, double e)
    {
        double[] x = [0, 0, 0, 0, 0];
        int counter = 0;
        int n = x.Length;
        double norm;
        double[] multiplication, multiplicationJacobi, prepF;
        double[] priorX = new double[n];
        do
        {
            norm = 0;

            Array.Copy(x, priorX, n);

            multiplicationJacobi = MultiplyJacoby(A, priorX);
            prepF = PrepareFunctionVector(A, f);

            x = Subtraction(prepF, multiplicationJacobi);

            for (int i = 0; i < n; i++)
            {
                norm += Math.Pow(x[i] - priorX[i], 2);
            }

            norm = Math.Sqrt(norm);

            counter++;

            multiplication = Multiply(A, prepF);

        } while (norm >= e);

        var incoherency = Subtraction(multiplication, f);

        Console.WriteLine($"Accuracy: {e}");
        Console.WriteLine($"Iteration quantity: {counter}");
        Console.WriteLine("\nResult:");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"х[{(i + 1)}] = {x[i]}");
        }

        Console.WriteLine("\nIncoherency vector:");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"r[{(i + 1)}] = {incoherency[i]}");
        }
    }
    public static double[] MultiplyJacoby(double[,] matrixA, double[] matrixB)
    {
        double[] result = new double[matrixA.GetLength(0)];
        int n = matrixA.GetLength(0);

        for (int i = 0; i < n; i++)
        {
            double sum = 0;

            var a = matrixA[i, i];

            for (int j = 0; j < n; j++)
            {
                sum += matrixA[i, j] / a * matrixB[j];
            }

            sum -= matrixB[i];

            result[i] = sum;
        }

        return result;
    }
    public static double[] PrepareFunctionVector(double[,] matrix, double[] f)
    {
        int n = matrix.GetLength(0);
        double[] result = new double[n];

        for (int i = 0; i < n; i++)
        {
            result[i] = f[i] / matrix[i, i];
        }

        return result;
    }
    public static double[] Multiply(double[,] matrixA, double[] matrixB)
    {
        int n = matrixA.GetLength(0);
        double[] result = new double[n];

        for (int i = 0; i < n; i++)
        {
            double sum = 0;

            for (int j = 0; j < n; j++)
            {
                sum += matrixA[i, j] * matrixB[j];
            }

            result[i] = sum;
        }

        return result;
    }
    public static double[] Subtraction(double[] arrayA, double[] arrayB)
    {
        int n = arrayA.Length;
        double[] result = new double[n];

        for (int i = 0; i < n; i++)
        {
            result[i] = arrayA[i] - arrayB[i];
        }

        return result;
    }
}