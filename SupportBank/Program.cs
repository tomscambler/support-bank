using System;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank SupportBank = new Bank();
            SupportBank.SeedBankWithTransactions("Transactions2012.xml");
            SupportBank.UpdateAllBalances();

            //User interface
            string? userInput = "1";
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
                while (!(menuOptions.Contains(userInput!)));

                switch(userInput)
                {
                    case "1":
                        SupportBank.PrintAllBalances();
                        break;
                    
                    case "2":
                        string? userAccountNameInput;

                        do 
                        {
                            Console.WriteLine("Which account would you like to see?\n");
                            SupportBank.PrintAccountNames();
                            userAccountNameInput = Console.ReadLine();
                        }
                        while (!SupportBank.doesAccountExist(new Account(userAccountNameInput!)));

                        SupportBank.PrintAccountTransactions(userAccountNameInput!);
                        break;

                    case "0":
                        Console.WriteLine("\nGoodbye! ('^')_/\n");
                        break;                
                }
            }
        }       
    }
}
