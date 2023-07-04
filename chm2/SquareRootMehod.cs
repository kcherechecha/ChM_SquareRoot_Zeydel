using System.Globalization;

namespace chm2;

public class SquareRootMethod
{
    private List<List<Double>> A;
    private List<List<Double>> S;
    private List<List<Double>> D;
    private List<double> b;

    private void InverseMatrix(List<List<double>> a)
    {
        int n = a.Count;
        List<List<double>> aExt = new List<List<double>>();
        for (int i = 0; i < n; i++)
        {
            aExt.Add(new List<double>());
            for (int j = 0; j < n; j++)
            {
                aExt[i].Add(a[i][j]);
            }
            for (int j = n; j < 2 * n; j++)
            {
                if (i == (j - n))
                {
                    aExt[i].Add(1);
                }
                else
                {
                    aExt[i].Add(0);
                }
            }
        }
        
        for (int i = 0; i < n; i++)
        {
            if (aExt[i][i] == 0)
            {
                Console.WriteLine("Матриця одинична");
            }
            for (int k = i + 1; k < n; k++)
            {
                double factor = aExt[k][i] / aExt[i][i];
                for (int j = i; j < 2 * n; j++)
                {
                    aExt[k][j] -= factor * aExt[i][j];
                }
            }
        }

        for (int i = n - 1; i >= 0; i--)
        {
            if (aExt[i][i] == 0)
            {
                Console.WriteLine("Матриця одинична");
            }

            for (int k = i - 1; k >= 0; k--)
            {
                double factor = aExt[k][i] / aExt[i][i];
                for (int j = i; j < 2 * n; j++)
                {
                    aExt[k][j] -= factor * aExt[i][j];
                }
            }
        }
        
        List<List<double>> aInversed = new List<List<double>>();
        for (int i = 0; i < n; i++)
        {
            aInversed.Add(new List<double>());
            for (int j = n; j < 2 * n; j++)
            {
                aInversed[i].Add(aExt[i][j] / aExt[i][i]);
            }
        }
        foreach (var list in aInversed)
        {
            foreach (var num in list) Console.Write($"{num:n3} ");
            Console.WriteLine();
        }
    }
    
    private void Determinant(List<List<double>> A)
    {
        int n = A.Count;
        double det = 1;

        for (int i = 0; i < n; i++)
        {
            int maxRow = i;
            for (int j = i + 1; j < n; j++)
            {
                if (Math.Abs(A[j][i]) > Math.Abs(A[maxRow][i]))
                {
                    maxRow = j;
                }
            }
            if (i != maxRow)
            {
                (A[i], A[maxRow]) = (A[maxRow], A[i]);
                det *= -1;
            }
            for (int j = i + 1; j < n; j++)
            {
                double factor = A[j][i] / A[i][i];
                for (int k = i; k < n; k++)
                {
                    A[j][k] -= factor * A[i][k];
                }
            }
            det *= A[i][i];
        }
        Console.WriteLine($"det(A) = {det}");
    }
    
    List<List<double>> MultiplyMatrix(List<List<double>> a, List<List<double>> b)
    {
        var c = new List<List<double>>();
        c.Add(new List<double>() {0, 0, 0, 0});
        c.Add(new List<double>() {0, 0, 0, 0});
        c.Add(new List<double>() {0, 0, 0, 0});
        c.Add(new List<double>() {0, 0, 0, 0});

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                c[i][j] = 0;
                for (int k = 0; k < 4; k++)
                {
                    c[i][j] += a[i][k] * b[k][j];
                }
            }
        }
        return c;
    }
    
    List<List<double>> TransposeMatrix(List<List<double>> a)
    {
        var c = new List<List<double>>();
        c.Add(new List<double>() {0, 0, 0, 0});
        c.Add(new List<double>() {0, 0, 0, 0});
        c.Add(new List<double>() {0, 0, 0, 0});
        c.Add(new List<double>() {0, 0, 0, 0});

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                c[i][j] = a[j][i];
            }
        }
        return c;
    }
    private void DisplayMatrix(List<List<double>> a)
    {
        foreach (var list in a)
        {
            foreach (var num in list) Console.Write($"{num:n3} ");
            Console.WriteLine();
        }
    }
   
    public void Solve()
    {
        A = new List<List<double>>();
        A.Add(new List<double>() { 4, 3, 2, 1});
        A.Add(new List<double>() { 3, 6, 4, 2});
        A.Add(new List<double>() { 2, 4, 6, 3});
        A.Add(new List<double>() { 1, 2, 3, 4});
        
        b = new List<double>();
        b = new List<double>() { 3, 6, 4, 7};
        
        Console.WriteLine("Метод квадратного кореня\n");
        DisplayMatrix(A);
        
        var at = TransposeMatrix(A);
        for(int i = 0; i < 4; i++)
        {
            if (!A[i].SequenceEqual(at[i]))
            {
                Console.WriteLine("Метод не можна застосувати");
                return;
            }
        }
        
        S = new List<List<double>>();
        D = new List<List<double>>();

        var d11 = Math.Sign(A[0][0]);
        var s11 = Math.Sqrt(Math.Abs(A[0][0]));
        var s12 = A[0][1] / (d11 * s11);
        var s13 = A[0][2] / (d11 * s11);
        var s14 = A[0][3] / (d11 * s11);
        
        var d22 = Math.Sign(A[1][1] - Math.Pow(s12, 2) * d11);
        var s22 = Math.Sqrt(Math.Abs(A[1][1] - Math.Pow(s12, 2) * d11));
        var s23 = (A[1][2] - s12 * d11 * s13) / (d22 * s22);
        var s24 = (A[1][3] - s12 * d11 * s14) / (d22 * s22);
        
        var d33 = Math.Sign(A[2][2] - Math.Pow(s13, 2) * d11 - Math.Pow(s23, 2) * d22);
        var s33 = Math.Sqrt(Math.Abs(A[2][2] - Math.Pow(s13, 2) * d11 - Math.Pow(s23, 2) * d22));
        var s34 = (A[2][3] - s13 * d11 * s14 - s23 * d22 * s24) / (d33 * s33);

        var d44 = Math.Sign(A[3][3] - Math.Pow(s14, 2) * d11 - Math.Pow(s24, 2) * d22 - Math.Pow(s34, 2) * d33);
        var s44 = Math.Sqrt(
            Math.Abs(A[3][3] - Math.Pow(s14, 2) * d11 - Math.Pow(s24, 2) * d22 - Math.Pow(s34, 2) * d33));
        
        Console.WriteLine($"\nОбрахунки для матриць S та D:\n\nd11 = {d11}\ns11 = {s11}\ns12 = {s12}\ns13 = {s13}\ns14 = {s14}\n\nd22 = {d22}\ns22 = {s22}\ns23 = {s23}\ns24 = {s24}\n\nd33 = {d33}\ns33 = {s33}\ns34 = {s34}\n\nd44 = {d44}\ns44 = {s44}\n");

        S.Add(new List<double>() { s11, s12, s13, s14 });
        S.Add(new List<double>() { 0, s22, s23, s24 });
        S.Add(new List<double>() { 0, 0, s33, s34 });
        S.Add(new List<double>() { 0, 0, 0, s44 });
        Console.WriteLine("Матриця S:");
        DisplayMatrix(S);
        D.Add(new List<double>() { d11, 0, 0, 0 });
        D.Add(new List<double>() { 0, d22, 0, 0 });
        D.Add(new List<double>() { 0, 0, d33, 0 });
        D.Add(new List<double>() { 0, 0, 0, d44});
        Console.WriteLine("\nМатриця D:");
        DisplayMatrix(D);
        
        var st = TransposeMatrix(S);
        Console.WriteLine("\nТранспонована матриця S:");
        DisplayMatrix(st);
        var std = MultiplyMatrix(st, D);
        Console.WriteLine("\nДобуток S(T)D:");
        DisplayMatrix(std);
        var y1 = b[0]/std[0][0];
        var y2 = (b[1]-std[1][0]*y1)/std[1][1];
        var y3 = (b[2] - std[2][0] * y1 - std[2][1] * y2)/std[2][2];
        var y4 = (b[3] - std[3][0] * y1 - std[3][1] * y2 - std[3][2] * y3)/std[3][3];

        Console.WriteLine($"\ny1 = {y1}");
        Console.WriteLine($"y2 = {y2}");
        Console.WriteLine($"y3 = {y3}");
        Console.WriteLine($"y4 = {y4}");

        var x4 = y4/S[3][3];
        var x3 = (y2 - S[2][3] * x4) / S[2][2];
        var x2 = (y2 - S[1][2] * x3 - S[1][3] * x4) / S[1][1];
        var x1 = (y1 - S[0][1] * x2 - S[0][2] * x3 - S[0][3] * x4) / S[0][0];
        
        Console.WriteLine("\nМаємо розвʼязок:");
        Console.WriteLine($"x1 = {x1}");
        Console.WriteLine($"x2 = {x2}");
        Console.WriteLine($"x3 = {x3}");
        Console.WriteLine($"x4 = {x4}");
        
        double det = Math.Round(d11 * Math.Pow(s11,2) * d22 * Math.Pow(s22,2) * d33 * Math.Pow(s33,2) * d44 * Math.Pow(s44,2));
        Console.WriteLine($"\ndet(A) = {det}");
        
        Console.WriteLine("\nОбернена матриця:");
        InverseMatrix(A);
        Console.WriteLine("\nВизначник матриці:");
        Determinant(A);
    }
}