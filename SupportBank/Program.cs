using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Transactions2014.csv");
             
            Ledger SupportBankLedger = new Ledger();

            foreach(string line in lines[1..])
            {
                string[] fields = line.Split(',');

                SupportBankLedger.AddToLedger(new Transaction(fields[0], fields[1], fields[2], fields[3], fields[4]));
            }
        }       
    }
}
