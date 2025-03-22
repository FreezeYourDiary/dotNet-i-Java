using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("testy"), InternalsVisibleTo("GUI")]

namespace BackPack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Liczba przedmiotow: ");
            int numberOfItems = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ziarno: ");
            int seed = Convert.ToInt32(Console.ReadLine());
            Problem problem = new Problem(numberOfItems, seed);
            Console.WriteLine("=======================================\n");

            Result result = problem.Solve(10);
            Console.WriteLine(problem);
            
            Console.WriteLine("=======================================\n");
            Console.WriteLine(result);
        }
    }

}