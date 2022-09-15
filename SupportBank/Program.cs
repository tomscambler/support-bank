using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"./Transactions2014.csv");
            
            Bank SupportBank = new Bank();

            foreach(string line in lines[1..])
            {
                string[] fields = line.Split(',');

                string transactionDateTime  = fields[0];
                string transactionDebtor    = fields[1];
                string transactionCreditor  = fields[2];
                string transactionNarrative = fields[3];
                string transactionAmount    = fields[4];

                SupportBank.AddToBankTransactions
                    (new Transaction
                        (
                            transactionDateTime, 
                            transactionDebtor, 
                            transactionCreditor, 
                            transactionNarrative, 
                            transactionAmount
                        )
                    );

                SupportBank.AddNewBankAccount(new Account(transactionDebtor  ));
                SupportBank.AddNewBankAccount(new Account(transactionCreditor));
            }

            SupportBank.UpdateAllBalances();

            SupportBank.PrintAllBalances();

            SupportBank.PrintAccountTransactions("Jon A");
        }       
    }
}
