using Newtonsoft.Json.Linq;
using System.Xml;

namespace SupportBank
{
    class Bank
    {
        public List<Account> BankAccounts;
        public List<Transaction> BankTransactions;

        public Bank()
        {
            BankAccounts = new List<Account>();
            BankTransactions = new List<Transaction>();    
        }

        public void SeedBankWithTransactions(string fileName)
        {
            string extensionType = fileName.Split('.').Last();

            switch(extensionType)
            {
                case "csv":
                    string[] transactionLines = System.IO.File.ReadAllLines($"{fileName}");
            
                    foreach(string transactionLine in transactionLines[1..])
                    {
                        string[] transactionField = transactionLine.Split(',');

                        string transactionDateTime  = transactionField[0],
                               transactionDebtor    = transactionField[1],
                               transactionCreditor  = transactionField[2],
                               transactionNarrative = transactionField[3],
                               transactionAmount    = transactionField[4];
                        try
                        {
                            AddNewBankTransaction(new Transaction(transactionDateTime, transactionDebtor, transactionCreditor, transactionNarrative, transactionAmount));
                        }
                        catch(FormatException e)
                        {
                            e = new FormatException();
                            Console.Write("The following transaction was not accepted: ");
                            Console.Write($"{transactionDateTime}, {transactionDebtor}, {transactionCreditor}, {transactionNarrative}, {transactionAmount}\n");
                        }
                        AddNewBankAccount(new Account(transactionDebtor  ));
                        AddNewBankAccount(new Account(transactionCreditor));
                    }
                    break;

                case "json":
                    string jsonFile = System.IO.File.ReadAllText($"{fileName}");
                    JArray myArray = JArray.Parse(jsonFile);
            
                    foreach(JToken transactionLine in myArray)
                    {
                        string transactionDateTime  = transactionLine["Date"]!.ToString(),
                               transactionDebtor    = transactionLine["FromAccount"]!.ToString(),
                               transactionCreditor  = transactionLine["ToAccount"]!.ToString(),
                               transactionNarrative = transactionLine["Narrative"]!.ToString(),
                               transactionAmount    = transactionLine["Amount"]!.ToString();
                        try
                        {
                            AddNewBankTransaction(new Transaction(transactionDateTime, transactionDebtor, transactionCreditor, transactionNarrative, transactionAmount));
                        }
                        catch(FormatException e)
                        {
                            e = new FormatException();
                            Console.Write("The following transaction was not accepted: ");
                            Console.Write($"{transactionDateTime}, {transactionDebtor}, {transactionCreditor}, {transactionNarrative}, {transactionAmount}\n");
                        }
                        AddNewBankAccount(new Account(transactionDebtor  ));
                        AddNewBankAccount(new Account(transactionCreditor));
                    }
                    break;
                case "xml":
                    XmlTextReader reader = new XmlTextReader($"{fileName}");
                    List<string> transactionFields = new List<string>{};
                    while (reader.Read())
                    {
                        switch(reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if(reader.GetAttribute("Date")!=null)
                                {
                                    transactionFields.Add(reader.GetAttribute("Date")!);
                                }
                                break;
                            case XmlNodeType.Text:
                                transactionFields.Add(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                if(reader.Name=="SupportTransaction")
                                {
                                    string transactionDateTime  = transactionFields[0],
                                           transactionNarrative = transactionFields[1],
                                           transactionAmount    = transactionFields[2],
                                           transactionDebtor    = transactionFields[3],
                                           transactionCreditor  = transactionFields[4];
                                    try
                                    {
                                        AddNewBankTransaction(new Transaction(DateTime.FromOADate(Double.Parse(transactionDateTime)).ToString(), transactionDebtor, transactionCreditor, transactionNarrative, transactionAmount));
                                    }
                                    catch(FormatException e)
                                    {
                                        e = new FormatException();
                                        Console.Write("The following transaction was not accepted: ");
                                        Console.Write($"{transactionDateTime}, {transactionDebtor}, {transactionCreditor}, {transactionNarrative}, {transactionAmount}\n");
                                    }
                                    AddNewBankAccount(new Account(transactionDebtor  ));
                                    AddNewBankAccount(new Account(transactionCreditor));
                                    
                                    transactionFields.Clear();
                                }
                                break;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Error reading file");
                    break;
            }

        }

        public bool doesAccountExist(Account possibleBankAccount)
        {
            foreach(Account existingBankAccount in BankAccounts)
            {
                if(possibleBankAccount.AccountName == existingBankAccount.AccountName)
                {
                    return true;
                }    
            }
            return false;
        }

        public bool AddNewBankAccount(Account newBankAccount)
        {
            if(doesAccountExist(newBankAccount))
            {
                return false;
            }
            BankAccounts.Add(newBankAccount);
            return true;
        }

        public void AddNewBankTransaction(Transaction transaction)
        {
            BankTransactions.Add(transaction);
        }

        public void UpdateAccountBalanceFromTransactionList(Account thisBankAccount)
        {
            foreach(Transaction bankTransaction in BankTransactions)
            {
                decimal transactionAmount   = bankTransaction.TransactionAmount;
                string  thisBankAccountName = thisBankAccount.AccountName;

                string theCreditor = bankTransaction.TransactionCreditor;
                string theDebtor   = bankTransaction.TransactionDebtor;

                if (thisBankAccountName == theCreditor)
                {
                    thisBankAccount.UpdateBalance(+transactionAmount);
                }
                else if (thisBankAccountName == theDebtor)
                {
                    thisBankAccount.UpdateBalance(-transactionAmount);
                }
            }
        }

        public void UpdateAllBalances()
        {
            foreach(Account bankAccount in BankAccounts)
            {
                UpdateAccountBalanceFromTransactionList(bankAccount);
            }
        }

        public void PrintAllBalances()
        {
            Console.WriteLine("\nAccount Name     Balance");
            Console.WriteLine("------------    ---------");

            foreach(Account bankAccount in BankAccounts)
            {
                string  tabSpacing    = new string('\t', (16-bankAccount.AccountName.Length-1)/4);
                string  creditOrDebit = bankAccount.AccountBalance<0 ? "D" : "C";
                decimal balanceAmount = Math.Abs(bankAccount.AccountBalance);
                Console.WriteLine($"{bankAccount.AccountName}{tabSpacing}?? {balanceAmount.ToString("0.00").PadLeft(5)}\t{creditOrDebit}");
            }
        }

        public void PrintAccountNames()
        {
            foreach(Account bankAccount in BankAccounts)
            {
                Console.WriteLine($"{bankAccount.AccountName}");
            }
            Console.WriteLine("\n");
        }

        public void PrintAccountTransactions(string accountName)
        {   
            string  printoutCredits = "", printoutDebits = "";
            decimal totalCredits    = 0 , totalDebits    = 0 ;

            foreach (Transaction transaction in BankTransactions)
            {
                string transactionDateTime   = transaction.TransactionDateTime.ToString("dd/MM/yyyy");
                string transactionDebtor     = transaction.TransactionDebtor;
                string transactionCreditor   = transaction.TransactionCreditor;
                string transactionNarrative  = transaction.TransactionNarrative;
                decimal transactionAmount    = transaction.TransactionAmount;

                string tabSpacing = new string('\t', (16-transactionDebtor.Length-1)/4);

                if(accountName == transactionCreditor)
                {
                   printoutCredits += $"{transactionDateTime.PadRight(16)}{transactionDebtor.PadRight(12)}?? {transactionAmount.ToString("0.00").PadLeft(5)}\t{transactionNarrative}\n";
                   totalCredits    += transactionAmount;
                }   
                else if(accountName == transactionDebtor)
                {
                   printoutDebits += $"{transactionDateTime.PadRight(16)}{transactionCreditor.PadRight(12)}?? {transactionAmount.ToString("0.00").PadLeft(5)}\t{transactionNarrative}\n";
                   totalDebits    += transactionAmount;
                } 
            }

            Console.WriteLine($"\n=========== Credits for {accountName}: ===========\n");
            Console.WriteLine("Date:           Payer:      Amount:     Narrative:");
            Console.WriteLine("-----           ------      -------     ----------");
            Console.WriteLine(printoutCredits);
            Console.WriteLine($"Total Credits: ?? {totalCredits:F2}");
            Console.WriteLine($"\n=========== Debits for {accountName}: ===========\n");
            Console.WriteLine("Date:           Payee:      Amount:     Narrative:");
            Console.WriteLine("-----           ------      -------     ----------");
            Console.WriteLine(printoutDebits);
            Console.WriteLine($"Total Debits: ?? {totalDebits:F2}");
        }
    }
}
