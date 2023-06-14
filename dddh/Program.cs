using System;
using System.Dynamic;

namespace dddh // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write the parameters of the two sides!");
            decimal a = decimal.Parse(Console.ReadLine());
            decimal b = decimal.Parse(Console.ReadLine());
            Console.WriteLine(GetRectangleArea(a,b)); 

        }

        public static decimal GetRectangleArea(decimal a, decimal b)
        {
            return a*b;
        }
    }
}
