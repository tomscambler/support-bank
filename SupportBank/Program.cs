using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Transactions2014.csv");

            string phrase = lines[1];

            string[] fields = phrase.Split(',');

            foreach (var field in fields)
            {
            Console.WriteLine($"{field}");
            }
        }       
    }
}
