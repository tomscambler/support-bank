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
            SupportBank.SeedBankWithTransactions("./Transactions2014.csv");
            SupportBank.UpdateAllBalances();

            //User interface
            string userInput = "1";
            while (userInput != "0")
            {
                List<string> menuOptions = new List<string> {"1", "2", "0"};

                do 
                {
                    Console.WriteLine(@"

===== Welcome to Support Bank =====

What would you like to do?

1. Print the balance of every account
2. Print the transaction history of a particular account
0. Terminate the program
                    ");         

                    userInput = Console.ReadLine();
                }
                while (!(menuOptions.Contains(userInput)));

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
                        while (!SupportBank.doesAccountExist(new Account(userAccountNameInput)));

                        SupportBank.PrintAccountTransactions(userAccountNameInput);
                        break;

                    case "0":
                        Console.WriteLine("\nGoodbye! ('^')_/\n");
                        break;                
                }
            }
        }       
    }
}
