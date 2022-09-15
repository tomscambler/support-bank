using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"..\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            
            string[] lines = System.IO.File.ReadAllLines(@"./DodgyTransactions2015.csv");
            
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
            //Setting up of the Bank is finished.

            //User interface begins:

            string userInput;
            List<string> checkValues = new List<string> {"1", "2", "3"};

            do 
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Print the balance of every account");
                Console.WriteLine("2. Print the transaction history of a particular account");
                Console.WriteLine("3. Terminate the program\n");

                userInput = Console.ReadLine();
            }
            while (!(checkValues.Contains(userInput)));

            switch(userInput)
            {
                case "1":
                    SupportBank.PrintAllBalances();
                    break;
                
                case "2":
                    string userAccountNameInput;

                    do 
                    {
                        Console.WriteLine("Which account would you like to see?\n");

                        SupportBank.PrintAccountNames();

                        userAccountNameInput = Console.ReadLine();
                    }
                    while (!SupportBank.doesAccountExist(userAccountNameInput));

                    SupportBank.PrintAccountTransactions(userAccountNameInput);
                    break;
                case "3":
                    Console.WriteLine("Goodbye! ('^')_/");
                    break;                
            }
        }       
    }
}
