using System;
using System.Dynamic;

namespace dddh // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write the parameters of the two sides!");
            string text = Console.ReadLine();
            decimal a = decimal.Parse(text);
            text= Console.ReadLine();
            decimal b = decimal.Parse(text);
            Console.WriteLine(GetRectangleArea(a,b)); 

        }

        public static decimal GetRectangleArea(decimal a, decimal b)
        {
            return a*b;
        }
    }
}
