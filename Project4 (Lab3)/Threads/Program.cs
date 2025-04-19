using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
[assembly:
InternalsVisibleTo("GUI")]
namespace Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<(int n, int m)> matrixSizes = new List<(int, int)>
        {
            (300, 300),
            (500, 500),
            (700, 700)
        };

            List<int> threads = new List<int>() { 1, 2, 3, 5, 10 };
            int repetitions = 5; // avg to each instance

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string csvPath = Path.Combine(docPath, "result1.csv");

            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                writer.WriteLine("MatrixSize,Threads,NoThreadAvg(ms),ThreadAvg(ms)");

                foreach (var size in matrixSizes)
                {
                    int n = size.n, m = size.m;
                    // + wykresy dla roznych rozmiarow macierzy

                    for (int i = 0; i < threads.Count(); i++)
                    {
                        List<long> times = new List<long>();
                        List<long> threadTimes = new List<long>();
                        for (int r = 0; r < repetitions; r++)
                        {
                            Macierz matrix1 = new Macierz(n, m);
                            Macierz matrix2 = new Macierz(n, m);
                            Problem problem = new Problem(matrix1, matrix2);

                            Stopwatch stopwatch = new Stopwatch();

                            stopwatch.Start();
                            Macierz result = problem.Solve();
                            stopwatch.Stop();
                            long time = stopwatch.ElapsedMilliseconds;
                            times.Add(time);

                            stopwatch.Restart();
                            Macierz tResult = problem.ThreadSolve(threads[i]);
                            stopwatch.Stop();
                            long threadTime = stopwatch.ElapsedMilliseconds;
                            threadTimes.Add(threadTime);
                        }

                        double avg = times.Average();
                        double avgThreads = threadTimes.Average();

                        Console.WriteLine($"{n}x{m} - {threads[i]} wątki : {avgThreads} ms");

                        writer.WriteLine($"{n}x{m},{threads[i]},{avg},{avgThreads}");
                    }

                    writer.WriteLine();
                }
            }

            Console.WriteLine($"saved to: {csvPath}");
        }
    }

    class Macierz
        {
            public int n { get; private set; }
            public int m { get; private set; }
            private int[,] matrix;
            public Macierz(int n, int m)
            {
                this.n = n;
                this.m = m;
                matrix = new int[n, m];

                Random rand = new Random();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        matrix[i, j] = rand.Next(0, 10);
                    }
                }
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        sb.Append(" ");
                        sb.Append(matrix[i, j].ToString());
                }
                    sb.AppendLine();
                }

                return sb.ToString();
            }

            //public void Print()
            //{
            //    for (int i = 0; i < n; i++)
            //    {
            //        for (int j = 0; j < m; j++)
            //        {
            //            Console.Write(matrix[i, j] + " ");
            //        }
            //        Console.WriteLine();
            //    }
            //}
            public int Get(int i, int j) => matrix[i, j];
            public void Set(int i, int j, int value) => matrix[i, j] = value;
        }

        class Problem
        {
            Macierz matrix1;
            Macierz matrix2;
            public Problem(Macierz matrix1, Macierz matrix2)
            {
                this.matrix1 = matrix1;
                this.matrix2 = matrix2;
            }
            public Macierz Solve()
            {
                Macierz result = new Macierz(matrix1.n, matrix2.m);

                for (int i = 0; i < matrix1.n; i++)
                {
                    for (int j = 0; j < matrix2.m; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k < matrix1.m; k++)
                        {
                            sum += matrix1.Get(i, k) * matrix2.Get(k, j);
                        }
                        result.Set(i, j, sum);
                    }
                }

                return result;
            }
        //public Macierz ThreadSolve(int nThreads)
        //{
        //    Macierz result = new Macierz(matrix1.n, matrix2.m);

        //    var options = new ParallelOptions { MaxDegreeOfParallelism = nThreads };

        //    Parallel.For(0, matrix1.n, options, i =>
        //    {
        //        for (int j = 0; j < matrix2.m; j++)
        //        {
        //            int sum = 0;
        //            for (int k = 0; k < matrix1.m; k++)
        //            {
        //                sum += matrix1.Get(i, k) * matrix2.Get(k, j);
        //            }
        //            result.Set(i, j, sum);
        //        }
        //    });

        //    return result;
        //}

        public Macierz ThreadSolve(int nThreads)
        {
            Macierz result = new Macierz(matrix1.n, matrix2.m);
            Thread[] threads = new Thread[nThreads];
       
            int rowsForT = matrix1.n / nThreads; // liczba wierzy przez 1 watek
            object lockObj = new object(); // lock object

            for (int t = 0; t < nThreads; t++)
            {
                int startRow = t * rowsForT; // dla watku 2 rowsForT = matrix.n/nThreads.
                int endRow = (t == nThreads - 1) ? matrix1.n : startRow + rowsForT; // 3 wiersze dla 3 watki = 9.
                // startRow+rowsforT --- zakres                                         
                // dla macierzy 9+_ wierszy (nie) pominiety ostatni
                threads[t] = new Thread(() =>
                {
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < matrix2.m; j++)
                        {
                            int sum = 0;
                            for (int k = 0; k < matrix1.m; k++)
                            {
                                sum += matrix1.Get(i, k) * matrix2.Get(k, j);
                            }
                            // wynik do macierzy z blokada
                            lock (lockObj)
                            {
                                // +data on i j  = sum.
                                result.Set(i, j, sum);
                            }
                        }
                    }

                });
                threads[t].Start();
            }
            for (int t = 0; t < nThreads; t++)
            {
                threads[t].Join();
            }

            return result;
        }

    }
}
