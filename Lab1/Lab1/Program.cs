using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz numbers = new FizzBuzz();
            numbers.PrintFizzBuzz();
        }
    }

    class FizzBuzz {
        int maxNumber;
        string fizzbuzz;
        public FizzBuzz()
        {
            int maxNumber = Convert.ToInt32(Console.ReadLine());
            this.maxNumber = maxNumber;
        }
        public void PrintFizzBuzz()
        {
            for (int i = 1; i< maxNumber; i++)
            {    
                fizzbuzz = (i % 3 == 0 && i % 5 == 0) ? "FizzBuzz" :
                (i % 3 == 0) ? "Fizz" :
                (i % 5 == 0) ? "Buzz" :
                i.ToString();

                Console.WriteLine(fizzbuzz);
            }
            
        }
    }

}
