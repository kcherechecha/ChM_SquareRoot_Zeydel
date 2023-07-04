namespace chm2;

public class Zeydel
{
    public Zeydel(double e = 1e-6)
        {
            Eps = e;
        } 
    private double Eps;
        double x1n(double x1, double x2, double x3, double x4) => (double) 1/12*(-3*x2-2*x3-x4+3);
        double x2n(double x1, double x2, double x3, double x4) => (double) 1/12*(-3*x1-4*x3-2*x4+6);
        double x3n(double x1, double x2, double x3, double x4) => (double) 1/12*(-2*x1-4*x2-3*x4+4);
        double x4n(double x1, double x2, double x3, double x4) => (double) 1/12*(-x1-2*x2-3*x3+7);

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
                
                List<List<double>> aInverted = new List<List<double>>();
                for (int i = 0; i < n; i++)
                {
                    aInverted.Add(new List<double>());
                    for (int j = n; j < 2 * n; j++)
                    {
                        aInverted[i].Add(aExt[i][j] / aExt[i][i]);
                    }
                }
                foreach (var list in aInverted)
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
                    List<double> temp = A[i];
                    A[i] = A[maxRow];
                    A[maxRow] = temp;
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
        
        public void Solve()
        {
            Console.WriteLine("\n\nМетод Зейделя\n");
            var A = new List<List<double>>();
            A.Add(new List<double>() { 12, 3, 2, 1});
            A.Add(new List<double>() { 3, 12, 4, 2});
            A.Add(new List<double>() { 2, 4, 12, 3});
            A.Add(new List<double>() { 1, 2, 3, 12});

            double x01 = 0, x02 = 0, x03 = 0, x04 = 0;
            double x11, x22, x33, x44;
            double e1, e2, e3, e4, e;
            int i = 1;
            do
            {
                x11 = x1n(x01, x02, x03, x04);
                x22 = x2n(x01, x02, x03, x04);
                x33 = x3n(x01, x02, x03, x04);
                x44 = x4n(x01, x02, x03, x04);
                Console.WriteLine($"Ітерація {i}: x1 = {x11:n5}, x2 = {x22:n5}, x3 = {x33:n5}, x4 = {x44:n5}");
                e1 = Math.Abs(x01 - x11);
                e2 = Math.Abs(x02 - x22);
                e3 = Math.Abs(x03 - x33);
                e4 = Math.Abs(x04 - x44);
                e = Math.Sqrt(Math.Pow(e1,2) + Math.Pow(e2,2) + Math.Pow(e3,2) + Math.Pow(e4,2));
                Console.WriteLine($"||({x11:n5},{x22:n5},{x33:n5}, {x44:n5})T-({x01:n5},{x02:n5},{x03:n5}, {x04:n5})T|| = {e}");
                if (e < Eps) break;
                x01 = x11;
                x02 = x22;
                x03 = x33;
                x04 = x44;
                i++;
            } while (e > Eps);
            
            Console.WriteLine("\nМаємо розвʼязок:");
            Console.WriteLine($"x1 = {x11}");
            Console.WriteLine($"x2 = {x22}");
            Console.WriteLine($"x3 = {x33}");
            Console.WriteLine($"x4 = {x44}");
            
            Console.WriteLine("\nОбернена матриця:");
            InverseMatrix(A);
            Console.WriteLine("\nВизначник матриці:");
            Determinant(A);
        }
    }